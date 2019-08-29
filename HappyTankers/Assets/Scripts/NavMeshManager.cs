﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshManager : MonoBehaviour
{
	public static void BuildTheMesh()
	{
		print("I am SAD");
		m_navMesh.UpdateNavMesh(m_navMesh.navMeshData);
	}
	static NavMeshSurface m_navMesh;
    // Start is called before the first frame update
    void Start()
    {
		m_navMesh = GetComponent<NavMeshSurface>();
		StartCoroutine(LateStart());	
	}

	IEnumerator LateStart()
	{
		yield return new WaitForEndOfFrame();
		m_navMesh.BuildNavMesh();
		print("I AM SO SAD");
	}

    // Update is called once per frame
    void Update()
    {
    }
}
