using System.Collections;
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
    }

    public void RemoveFilter()
    {
        m_animator.SetBool("RemoveFilter",true);
    }

    public void LerpMoveFocus(Vector3 newPos)
    {
        m_targetPos = newPos;
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

        if ((m_targetPos - transform.position).magnitude < 0.1f)
        {
            transform.position += (m_targetPos - transform.position) * 1 * Time.deltaTime;
        }
        else
        {
            m_targetPos = transform.position;
        }
    }
}
