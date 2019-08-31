using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : MonoBehaviour
{
    [SerializeField] private Vector3 m_direction;
    [SerializeField] private float m_speed;
    [SerializeField] private AudioSource m_happySound;
    [SerializeField] private AudioSource m_darkSound;

    private int m_bounceCount = 0;


    public void InitialSetup(Vector3 d, float s = 5)
    {
        DeleteIfOutOfView(); //Hacky fix to stop AI shooting offscreen
        m_direction = d.normalized + new Vector3(Random.Range(-0.25f,0.25f),0, Random.Range(-0.25f, 0.25f));
        m_speed = s;

        if (FilterManager.IsHappy)
        {
            m_happySound.clip = SoundController.Instance.chirpSmallFire;
            m_happySound.Play();
        }
        else
        {
            m_darkSound.clip = SoundController.Instance.darkSmallFire;
            m_darkSound.Play();
        }

    }

    private void DeleteIfOutOfView()
    {
        Vector3 b = transform.position;
        Vector3 c = CameraScript.Instance.transform.position;
        float hOff = (LevelManager.m_levelW / 2) + 6;
        float vOff = (LevelManager.m_levelH / 2) + 6;
        if (b.x < c.x - hOff || b.x > c.x + hOff || b.z < c.z - vOff || b.z > c.z + vOff)
        {
            Destroy(gameObject);
        }
        hOff = (LevelManager.m_levelW / 2);
        vOff = (LevelManager.m_levelH / 2);
        bool camOutsideRoom = (c.x < -hOff || c.x > hOff || c.z < -vOff || c.z > vOff);
        bool bulletOutsideRoom = (b.x < -hOff || b.x > hOff || b.z < -vOff || b.z > vOff);
        if (FilterManager.IsHappy && camOutsideRoom != bulletOutsideRoom)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_direction = new Vector3(m_direction.x, 0, m_direction.z);
        DeleteIfOutOfView();
        RaycastHit hit;
        Physics.Linecast(transform.position, transform.position + m_direction * m_speed * Time.deltaTime, out hit, ~(1 << 14), QueryTriggerInteraction.Collide);
        if (hit.collider != null)
        {
            CollisionFound(hit);
        }
        
        transform.position += m_direction * m_speed * Time.deltaTime;
    }

    void CollisionFound(RaycastHit hit)
    {
		if(hit.collider.transform.parent)
		{
			BlockScript blockScript = hit.collider.transform.parent.GetComponent<BlockScript>();
			if (blockScript)
			{
				blockScript.WasHit(1);
			}
		} 
		
        if (hit.collider.gameObject.tag == "shield")
        {
            Destroy(gameObject);
            m_direction = Vector3.Reflect(m_direction, hit.normal);
        }
		else if(hit.collider.gameObject.tag == "bullet")
		{
			Destroy(gameObject);
		}
        else
        {
            if (hit.rigidbody)
            {
				TankScript tank = hit.rigidbody.GetComponent<TankScript>();
				if (tank)
				{
					tank.Hit(gameObject);
                }
			}
            Bounce(hit.normal);
        }
    }

    private void Bounce(Vector3 normal)
    {
        if (Mathf.Abs(normal.x) > 0.1f)
        {
            m_direction = new Vector3(-m_direction.x, m_direction.y, m_direction.z);
        }
        else
        {
            m_direction = new Vector3(m_direction.x, m_direction.y, -m_direction.z);
        }
    }
}
