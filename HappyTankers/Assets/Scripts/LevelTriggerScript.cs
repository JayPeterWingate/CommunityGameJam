using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTriggerScript : MonoBehaviour
{
    public UnityAction m_action;

    void OnTriggerEnter()
    {
        m_action();
    }
}
