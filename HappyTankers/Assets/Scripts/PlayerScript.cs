﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : TankController
{
	public static GameObject playerRef;
	public MasterControl controls;
	public Vector3 startPos;

	public Texture2D m_darkCursor;
	public Texture2D m_happyCursor;

	public Texture2D m_darkReadyCursor;
	public Texture2D m_happyReadyCursor;
	
	private void Awake()
	{
		isAI = false;
		playerRef = gameObject;
		color = new Color(1, 1, 1);
		controls = new MasterControl();
		controls.Enable();
		controls.Player.MovementAxis.performed += ctr =>
		{
			Vector2 input = ctr.ReadValue<Vector2>();
			targetDirection = input;
		};
		controls.Player.MovementAxis.canceled += ctr => { targetDirection = new Vector2();};
		controls.Player.RotateTurret.performed += ctr =>
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// Save the info
			RaycastHit hit;
			// You successfully hi
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 14), QueryTriggerInteraction.Collide))
			{
				turretTarget = hit.point;
				
			}
		};
		controls.Player.XBoxRotateTurret.performed += crt =>
		{
			Vector2 read = crt.ReadValue<Vector2>();
			Vector3 target = new Vector3(read.x, 0, read.y) * 10 + transform.Find("Tank").position;
			
			turretTarget = target;
		};
		controls.Player.Fire.performed += crt => fireEvent.Invoke();
        controls.Player.FireStrong.performed += crt => strongFireEvent.Invoke();
        controls.Player.Shield.performed += crt => shieldEvent.Invoke();
		controls.Player.Escape.performed += crt => SceneManager.LoadScene(0);
	}

	public override void SetCursor(bool happy, bool ready) =>	Cursor.SetCursor(happy? ready ? m_happyReadyCursor : m_happyCursor : ready ? m_darkReadyCursor : m_darkCursor, new Vector2(32, 32), CursorMode.Auto);
}
