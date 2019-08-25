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
	Rigidbody m_body;


	// Start is called before the first frame update
    void Start()
    {
		m_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		m_body.AddTorque( transform.up * m_rotatePower * ( m_controller.leftDrive - m_controller.rightDrive));
		m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0,0, m_controller.leftDrive * m_horsePower)), m_leftTread.position);
		m_body.AddForceAtPosition(transform.TransformDirection(new Vector3(0, 0, m_controller.rightDrive * m_horsePower)), m_rightTread.position);
	}
}
