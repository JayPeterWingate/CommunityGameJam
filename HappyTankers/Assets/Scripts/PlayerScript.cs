using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerScript : TankController
{
	public static GameObject playerRef;
	public MasterControl controls;

	private void Awake()
	{
		playerRef = gameObject;
		print("THIS OCCURED");
		color = new Color(1, 1, 1);
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
		controls.Player.Fire.performed += crt => fireEvent.Invoke();
        controls.Player.FireStrong.performed += crt => strongFireEvent.Invoke();
        controls.Player.Shield.performed += crt => shieldEvent.Invoke();
	}
}
