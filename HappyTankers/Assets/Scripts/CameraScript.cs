﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Animator m_animator;
    float m_timer = 0;
    Vector3 m_targetPos;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_targetPos = transform.position;

		FilterManager.OnChange.AddListener(RemoveFilter);
    }

    public void RemoveFilter(bool isHappy)
    {
        m_animator.SetBool("RemoveFilter",!isHappy);
    }

    public void LerpMoveFocus(Vector3 newPos)
    {
        m_targetPos = newPos;
    }

    // Update is called once per frame
    void Update()
    {

        if ((m_targetPos - transform.position).magnitude > 0.1f)
        {
            transform.position += (m_targetPos - transform.position) * 1 * Time.deltaTime;
        }
        else
        {
            m_targetPos = transform.position;
        }
    }
}
