using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Animator m_animator;
    float m_timer = 0;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public void RemoveFilter()
    {
        m_animator.SetBool("RemoveFilter",true);
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer>1)
        {
            RemoveFilter();
            m_timer = -1000f;
        }
    }
}
