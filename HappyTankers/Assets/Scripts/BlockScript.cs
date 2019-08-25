using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] protected GameObject m_happy;
    [SerializeField] protected GameObject m_dark;

    public void SetFilterModes(bool happy, bool dark)
    {
        m_happy.SetActive(happy);
        m_dark.SetActive(dark);
    }
}
