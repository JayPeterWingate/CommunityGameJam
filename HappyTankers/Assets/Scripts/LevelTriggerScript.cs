using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTriggerScript : MonoBehaviour
{
    public UnityAction m_action;

    void OnTriggerEnter(Collider collider)
    {
        if (PlayerScript.playerRef == collider.attachedRigidbody.transform.parent.gameObject)
        {
            //Debug.Log("HitOutterWall");
            m_action();
        }
    }
}
