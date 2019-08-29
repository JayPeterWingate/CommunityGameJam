﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerScript : TankController
{
	public static GameObject playerRef;
	public MasterControl controls;
	public Vector3 startPos;

	private void Awake()
	{
		playerRef = gameObject;
		color = new Color(1, 1, 1);
		controls = new MasterControl();
		controls.Enable();
		controls.Player.MovementAxis.performed += ctr =>
		{
			Vector2 input = ctr.ReadValue<Vector2>();
			rightDrive = Vector2.Dot(input, new Vector2(-1, 1));
			leftDrive = Vector2.Dot(input, new Vector2(1 , 1));
		};
		controls.Player.MovementAxis.canceled += ctr => { leftDrive = 0; rightDrive = 0; };
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
