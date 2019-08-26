﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysUpright : MonoBehaviour
{
	public float speed;
    // Update is called once per frame
    void Update()
    {
		Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
	}
}