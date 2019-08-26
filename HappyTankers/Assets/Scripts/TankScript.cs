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
	Vector3 m_currentTarget;
	Rigidbody m_body;


	// Start is called before the first frame update
    void Start()
	{
		m_currentTarget = new Vector3();
		m_body = GetComponent<Rigidbody>();
		m_controller.fireEvent.AddListener(FireMainBattleCannon);
    }
	
    // Update is called once per frame
    void Update()
    {
		m_body.AddTorque( transform.up * m_rotatePower * ( m_controller.leftDrive - m_controller.rightDrive));
		m_body.AddForceAtPosition(m_leftTread.TransformDirection(new Vector3(0,0, m_controller.leftDrive * m_horsePower)), m_leftTread.position);
		m_body.AddForceAtPosition(m_rightTread.TransformDirection(new Vector3(0, 0, m_controller.rightDrive * m_horsePower)), m_rightTread.position);

		// [TODO] get a smoother looking rotation
		Vector3 targetPosition = new Vector3(m_controller.turretTarget.x, m_turret.position.y, m_controller.turretTarget.z);
		var dir = targetPosition - m_turret.position;
		dir.y = 0.0f;
		m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, Quaternion.LookRotation(dir), Time.time * m_turretRotateSpeed);


	}

	private void FireMainBattleCannon()
	{
		StartCoroutine(FireBullets(10, 0.05f));
	}
	private IEnumerator FireBullets(int bulletCount, float spawnGap)
	{
		for(int i = 0; i < bulletCount; i++)
		{
			GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_turret.rotation);
			yield return new WaitForSeconds(spawnGap);
		}
	}
	private void ActiveShield()
	{

	}
}
