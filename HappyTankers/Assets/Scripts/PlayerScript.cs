using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



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
		playerRef = gameObject;
		color = new Color(1, 1, 1);
		controls = new MasterControl();
		controls.Enable();
		controls.Player.MovementAxis.performed += ctr =>
		{
			Vector2 input = ctr.ReadValue<Vector2>();
			targetDirection = input;
		};
		controls.Player.MovementAxis.canceled += ctr => { targetDirection = new Vector2(); };
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

	public override void SetCursor(bool happy, bool ready) =>	Cursor.SetCursor(happy? ready ? m_happyReadyCursor : m_happyCursor : ready ? m_darkReadyCursor : m_darkCursor, new Vector2(32, 32), CursorMode.Auto);
}
