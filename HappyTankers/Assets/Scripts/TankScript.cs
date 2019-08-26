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

	public UnityEvent fireEvent;
	public UnityEvent shieldEvent;
}

public class TankScript : MonoBehaviour
{
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

	bool m_isFiring = false;
	bool m_isShielding = false;

	Vector3 m_currentTarget;
	Rigidbody m_body;


	// Start is called before the first frame update
    void Start()
	{
		m_currentTarget = new Vector3();
		m_body = GetComponent<Rigidbody>();
		m_controller.fireEvent.AddListener(FireMainBattleCannon);
		m_controller.shieldEvent.AddListener(ActiveShield);
    }
	
    // Update is called once per frame
    void Update()
    {
		m_body.AddTorque( transform.up * m_rotatePower * ( m_controller.leftDrive - m_controller.rightDrive));
		Debug.DrawRay(m_leftTread.position, m_leftTread.position + transform.TransformDirection(new Vector3(0, 0, m_controller.leftDrive * m_horsePower)));
		Debug.DrawRay(m_rightTread.position, m_rightTread.position + transform.rotation * (new Vector3(0, 0, m_controller.rightDrive * m_horsePower)));

		m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0,0, m_controller.leftDrive * m_horsePower)), m_leftTread.position);
		m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0, 0, m_controller.rightDrive * m_horsePower)), m_rightTread.position);

		// [TODO] get a smoother looking rotation
		Vector3 targetPosition = new Vector3(m_controller.turretTarget.x, m_turret.position.y, m_controller.turretTarget.z);
		var dir = targetPosition - m_turret.position;
		dir.y = 0.0f;
		m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, Quaternion.LookRotation(dir), Time.time * m_turretRotateSpeed);


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
			GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_turret.rotation);
			yield return new WaitForSeconds(spawnGap);
		}
		yield return new WaitForSeconds(cooldown);
		m_isFiring = false;
	}
	private void ActiveShield()
	{
		if (!m_isFiring && !m_isShielding)
		{
			StartCoroutine(StartShield(0.1f, 0.25f, 0.1f));
		}
	}
	private IEnumerator StartShield(float rise, float stay, float close)
	{
		m_isShielding = true;
		float current = 0;
		while(current < rise)
		{
			m_shield.transform.localScale = Vector3.Lerp(new Vector3(), new Vector3(2, 2, 2), current / rise);
			current += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(stay);
		current = 0;
		while (current < close)
		{
			current += Time.deltaTime;
			m_shield.transform.localScale = Vector3.Lerp(new Vector3(2.1f, 2.1f, 2.1f), new Vector3(), current / close);
			yield return new WaitForEndOfFrame();
		}
		
		m_isShielding = false;
	}
}
