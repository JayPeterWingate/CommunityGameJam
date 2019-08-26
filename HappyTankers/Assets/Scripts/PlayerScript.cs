using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
	public float leftDrive;
	public float rightDrive;
	public Vector3 turretTarget;


}

public class PlayerScript : TankController
{
	public MasterControl controls;

	private void Awake()
	{

		print("THIS OCCURED");
		controls = new MasterControl();
		controls.Enable();
		controls.Player.LeftTrack.performed += ctr => leftDrive = ctr.ReadValue<float>();
		controls.Player.RightTrack.performed += ctr => rightDrive = ctr.ReadValue<float>();
		controls.Player.LeftTrack.canceled += ctr => { leftDrive = 0; };
		controls.Player.RightTrack.canceled += ctr => { rightDrive = 0; };
		controls.Player.RotateTurret.performed += ctr =>
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// Save the info
			RaycastHit hit;
			// You successfully hi
			if (Physics.Raycast(ray, out hit))
			{
				turretTarget = hit.point;
				
			}
		};
	}
}
