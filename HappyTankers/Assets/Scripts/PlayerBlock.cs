using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : BlockScript
{
	static public PlayerBlock PlayerStarter; 
	// Start is called before the first frame update
	bool spawned = false;
	public AlliedTankController AITank;

	private void Start()
	{
		AITank.m_levelSpawned = m_level;
	}

	void OnEnable()
	{
		PlayerStarter = this;
    }
	public void ActivateEnemy()
	{
		
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(true);
		}
		transform.DetachChildren();
	}
	public void TeleportPlayer()
	{
		PlayerScript.playerRef.transform.Find("Tank").GetComponent<Rigidbody>().velocity = new Vector3();

		PlayerScript.playerRef.transform.Find("Tank").GetComponent<Rigidbody>().MovePosition(transform.position);
		PlayerScript.playerRef.GetComponent<PlayerScript>().startPos = transform.position;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}
}
