using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : BlockScript
{
    // Start is called before the first frame update
    void Start()
    {
		if (LevelManager.IsHappy)
		{
			PlayerScript.playerRef.transform.position = transform.position;
			for(int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}

		} else
		{
			// Spawn in tank
		}
    }
}
