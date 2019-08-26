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
	public UnityEvent shieldEvent = new UnityEvent();
	public Color color;
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
	[SerializeField] Transform m_shield;
	[SerializeField] Renderer[] m_renderers;
	[SerializeField] SpriteRenderer[] m_sprites;
	bool m_isFiring = false;
	bool m_isShielding = false;

	Vector3 m_currentTarget;
	Rigidbody m_body;
	public Color color;

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
		if(m_controller.shieldEvent != null)
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

	}
	
    // Update is called once per frame
    void Update()
    {
		if (m_controller)
		{
			m_body.AddTorque(transform.up * m_rotatePower * (m_controller.leftDrive - m_controller.rightDrive));
			Debug.DrawRay(m_leftTread.position, m_leftTread.position + transform.TransformDirection(new Vector3(0, 0, m_controller.leftDrive * m_horsePower)));
			Debug.DrawRay(m_rightTread.position, m_rightTread.position + transform.rotation * (new Vector3(0, 0, m_controller.rightDrive * m_horsePower)));

			m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0, 0, m_controller.leftDrive * m_horsePower)), m_leftTread.position);
			m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0, 0, m_controller.rightDrive * m_horsePower)), m_rightTread.position);

			// [TODO] get a smoother looking rotation
			Vector3 targetPosition = new Vector3(m_controller.turretTarget.x, m_turret.position.y, m_controller.turretTarget.z);
			var dir = targetPosition - m_turret.position;
			dir.y = 0.0f;
			m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, Quaternion.LookRotation(dir), Time.time * m_turretRotateSpeed);

		}


	}

	private void FireMainBattleCannon()
	{
		if(!m_isFiring && !m_isShielding)
		{
			StartCoroutine(FireBullets(10, 0.05f, 1.0f));
		}
		
	}
	private IEnumerator FireBullets(int bulletCount, float spawnGap, float cooldown)
	{
		m_isFiring = true;
		for(int i = 0; i < bulletCount; i++)
		{
            BouncyBullet bullet = Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_turret.rotation).GetComponent<BouncyBullet>();
            bullet.InitialSetup(bullet.transform.forward);
            yield return new WaitForSeconds(spawnGap);
		}
		yield return new WaitForSeconds(cooldown);
		m_isFiring = false;
	}
	private void ActiveShield()
	{
		if (!m_isFiring && !m_isShielding)
		{
			StartCoroutine(StartShield(0.1f, 0.25f, 0.1f, new Vector3(2.5f, 2.5f, 2.5f)));
		}
	}
	private IEnumerator StartShield(float rise, float stay, float close, Vector3 size)
	{
		m_isShielding = true;
		float current = 0;
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
		
		m_isShielding = false;
	}
}
