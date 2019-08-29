using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : BlockScript
{
    // Start is called before the first frame update
    void OnEnable()
    {
		if (FilterManager.IsHappy == true && FilterManager.IsAlmostDark != true)
		{
			PlayerScript.playerRef.transform.Find("Tank").GetComponent<Rigidbody>().velocity = new Vector3();

			PlayerScript.playerRef.transform.Find("Tank").GetComponent<Rigidbody>().MovePosition(transform.position);
			PlayerScript.playerRef.GetComponent<PlayerScript>().startPos = transform.position;
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}

		} else
		{
			// Spawn in tank
		}
    }
}
