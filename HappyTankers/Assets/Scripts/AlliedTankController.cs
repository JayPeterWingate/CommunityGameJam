﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedTankController : AITankController
{
	
    // Start is called before the first frame update
    void Start()
    {
		Init();
    }

    // Update is called once per frame
    void Update()
    {
		Tick();
    }

	private void OnDestroy()
	{
		Destruct();
	}
}
