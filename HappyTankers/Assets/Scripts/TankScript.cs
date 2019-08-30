using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankController : MonoBehaviour
{
	public float leftDrive;
	public float rightDrive;
	public Vector3 turretTarget;

	public UnityEvent fireEvent = new UnityEvent();
    public UnityEvent strongFireEvent = new UnityEvent();
	public UnityEvent shieldEvent = new UnityEvent();
	public Color color;

	public virtual void SetCursor(bool happy, bool ready) { }

	public int lives;
}

public class TankScript : MonoBehaviour
{
	public static List<TankScript> TankList = new List<TankScript>();
	[SerializeField] Transform m_leftTread;
	[SerializeField] Transform m_rightTread;
 	[SerializeField] TankController m_controller;
	[SerializeField] float m_horsePower;
	[SerializeField] float m_rotatePower;
	[SerializeField] float m_turretRotateSpeed;
	[SerializeField] Transform m_turret;
	[SerializeField] Transform m_bulletSpawn;
	[SerializeField] GameObject m_bulletPrefab;
    [SerializeField] GameObject m_strongBulletPrefab;
	[SerializeField] List<GameObject> m_bulletList;
	[SerializeField] Transform m_shield;
	[SerializeField] Renderer[] m_renderers;
	[SerializeField] SpriteRenderer[] m_sprites;
	bool m_isFiring = false;
	bool m_isShielding = false;
	bool m_isTakingDamage = false;
	Vector3 m_currentTarget;
	Rigidbody m_body;
	public Color color;
    public bool paused = false;

	// Start is called before the first frame update
    void Start()
	{
		TankList.Add(this);
		m_currentTarget = new Vector3();
		m_body = GetComponent<Rigidbody>();
		if (m_controller.fireEvent != null)
		{
			m_controller.fireEvent.AddListener(FireMainBattleCannon);
		}
        if (m_controller.strongFireEvent != null)
        {
            m_controller.strongFireEvent.AddListener(FireSecondaryStrongBattleCannon);
        }
        if (m_controller.shieldEvent != null)
		{
			m_controller.shieldEvent.AddListener(ActiveShield);
		}
		
		for(int i = 0; i < m_renderers.Length; i++)
		{
			m_renderers[i].material.color = m_controller.color;
		}
		for(int i = 0; i < m_sprites.Length; i++)
		{
			m_sprites[i].color = m_controller.color;
		}
		color = m_controller.color;
		m_bulletList = new List<GameObject>();

		m_shield.gameObject.SetActive(false);

		SetVisuals(true);
		FilterManager.OnChange.AddListener(SetVisuals);
	}
	void SetVisuals(bool isHappy)
	{
		/*for(int i = 0; i < m_renderers.Length; i++)
		{
			bool shouldBeActive = isHappy ?
				m_renderers[i].gameObject.layer == 9
				:
				m_renderers[i].gameObject.layer == 10||
				m_renderers[i].gameObject.layer == 12;
			m_renderers[i].enabled = shouldBeActive;
		}*/
		m_controller.SetCursor(isHappy, m_isFiring);
	}
    // Update is called once per frame
    void FixedUpdate()
    {
		if (m_controller && !paused)
		{
			m_body.AddTorque(transform.up * m_rotatePower * (m_controller.leftDrive - m_controller.rightDrive));
			Debug.DrawRay(m_leftTread.position, m_leftTread.position + transform.TransformDirection(new Vector3(0, 0, m_controller.leftDrive * m_horsePower)));
			Debug.DrawRay(m_rightTread.position, m_rightTread.position + transform.rotation * (new Vector3(0, 0, m_controller.rightDrive * m_horsePower)));

			m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0, 0, m_controller.leftDrive * m_horsePower)), m_leftTread.position);
			m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0, 0, m_controller.rightDrive * m_horsePower )), m_rightTread.position);

			// [TODO] get a smoother looking rotation
			Vector3 targetPosition = new Vector3(m_controller.turretTarget.x, m_turret.position.y, m_controller.turretTarget.z);
			var dir = targetPosition - m_turret.position;
			dir.y = 0.0f;
			m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, Quaternion.LookRotation(dir), Time.time * m_turretRotateSpeed);

		}


	}

	private void FireMainBattleCannon()
	{
		if(!m_isFiring && !m_isShielding && !m_isTakingDamage)
		{
			StartCoroutine(FireBullets(10, 0.05f, 1.0f, false));
		}
		
	}
    private void FireSecondaryStrongBattleCannon()
    {
        if (!m_isFiring && !m_isShielding && !m_isTakingDamage)
        {
            StartCoroutine(FireBullets(1, 0.05f, 1.0f, true));
        }

    }
    private IEnumerator FireBullets(int bulletCount, float spawnGap, float cooldown, bool strong)
	{
		m_isFiring = true;
		m_controller.SetCursor(FilterManager.IsHappy, false);
		for (int i = 0; i < bulletCount; i++)
		{
            if (strong)
            {
                BouncyBulletStrong bullet = Instantiate(m_strongBulletPrefab, m_bulletSpawn.position, m_turret.rotation).GetComponent<BouncyBulletStrong>();
                bullet.InitialSetup(bullet.transform.forward);
				m_bulletList.Add(bullet.gameObject);
            }
            else
            {
                BouncyBullet bullet = Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_turret.rotation).GetComponent<BouncyBullet>();
                bullet.InitialSetup(bullet.transform.forward);
				m_bulletList.Add(bullet.gameObject);
			}
            yield return new WaitForSeconds(spawnGap);
		}
		yield return new WaitForSeconds(cooldown);
		m_controller.SetCursor(FilterManager.IsHappy, true);
		m_isFiring = false;
	}
	private void ActiveShield()
	{
		if (!m_isFiring && !m_isShielding && !m_isTakingDamage)
		{
			StartCoroutine(StartShield(0.1f, 1f, 0.1f, new Vector3(5f, 5f, 5f)));
		}
	}
	private IEnumerator StartShield(float rise, float stay, float close, Vector3 size)
	{
		m_isShielding = true;
		float current = 0;
		m_shield.gameObject.SetActive(true);
		while(current < rise)
		{
			m_shield.transform.localScale = Vector3.Lerp(new Vector3(), size, current / rise);
			current += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(stay);
		current = 0;
		while (current < close)
		{
			current += Time.deltaTime;
			m_shield.transform.localScale = Vector3.Lerp(size, new Vector3(), current / close);
			yield return new WaitForEndOfFrame();
		}

		m_shield.gameObject.SetActive(false);
		m_isShielding = false;
	}
	public void DestroyBullets()
	{
		m_bulletList.ForEach((GameObject obj) => Destroy(obj));
	}

	public void Hit(GameObject bullet)
	{
		if (!m_isTakingDamage)
		{
			if (m_controller.lives > 0)
			{
				Destroy(bullet);
				m_controller.lives -= 1;
			}
			else
			{
				try
				{
					PlayerScript player = (PlayerScript)m_controller;
					if (FilterManager.IsHappy)
					{
						DestroyBullets();
						transform.position = player.startPos;
						player.lives = 3;
					} else
					{
						FilterManager.TriggerDeath();
					}
					
				}
				catch
				{
					// Controller is not a player
					TankList.Remove(this);
					Destroy(gameObject);
				}
			}
			print("IM HIT");
			m_isTakingDamage = true;
			Destroy(bullet);
			StartCoroutine(ResetDamage(1));
		}
	}
	IEnumerator ResetDamage(float resetTime)
	{
		bool onSwitch = true;
		for(int counter = 0; counter < 6; counter++)
		{
			yield return new WaitForSeconds(resetTime / 6);
			onSwitch = !onSwitch;
			for(int i = 0; i < m_sprites.Length; i++)
			{
				m_sprites[i].enabled = onSwitch;
			}
		}
		m_isTakingDamage = false;
	}

	private void OnDestroy()
	{
		TankList.Remove(this);
	}
}