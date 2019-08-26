using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour
{
	[SerializeField] Transform m_leftTread;
	[SerializeField] Transform m_rightTread;
 	[SerializeField] TankController m_controller;
	[SerializeField] float m_horsePower;
	[SerializeField] float m_rotatePower;
	[SerializeField] float m_turretRotateSpeed;
	[SerializeField] Transform m_turret;

	Vector3 m_currentTarget;
	Rigidbody m_body;


	// Start is called before the first frame update
    void Start()
	{
		m_currentTarget = new Vector3();
		m_body = GetComponent<Rigidbody>();
		StartCoroutine(LateStart());
    }
	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(1);
		m_body.constraints = RigidbodyConstraints.FreezePositionY;
	}
    // Update is called once per frame
    void Update()
    {
		m_body.AddTorque( transform.up * m_rotatePower * ( m_controller.leftDrive - m_controller.rightDrive));
		m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0,0, m_controller.leftDrive * m_horsePower)), m_leftTread.position);
		m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0, 0, m_controller.rightDrive * m_horsePower)), m_rightTread.position);

		// [TODO] get a smoother looking rotation
		Vector3 targetPosition = new Vector3(m_controller.turretTarget.x, m_turret.position.y, m_controller.turretTarget.z);
		var dir = targetPosition - m_turret.position;
		dir.y = 0.0f;
		m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, Quaternion.LookRotation(dir), Time.time * m_turretRotateSpeed);


	}
}
