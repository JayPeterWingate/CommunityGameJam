using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBulletStrong : MonoBehaviour
{
    private Vector3 m_direction;
    private float m_speed;
    private int m_bounceCount = 0;

    public void InitialSetup(Vector3 d, float s = 5)
    {
        m_direction = d;
        m_speed = s;
    }


    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Physics.Linecast(transform.position, transform.position + m_direction * m_speed * Time.deltaTime, out hit, ~(1 << 14), QueryTriggerInteraction.UseGlobal);
        if (hit.collider != null)
        {
            CollisionFound(hit);
        }
        
        transform.position += m_direction * m_speed * Time.deltaTime;
    }

    void CollisionFound(RaycastHit hit)
    {
		if (transform.parent)
		{
			BlockScript blockScript = hit.collider.transform.parent.GetComponent<BlockScript>();
			if (blockScript)
			{
				blockScript.WasHit(2);
			}
		}
        
        if (hit.collider.tag == "shield")
        {
            m_direction = Vector3.Reflect(m_direction, hit.normal);
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
