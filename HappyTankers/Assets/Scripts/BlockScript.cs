using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    [SerializeField] protected GameObject m_happy;
    [SerializeField] protected GameObject m_dark;

    public void SetFilterModes(bool happy, bool dark)
    {
        if (m_happy != null && m_dark != null)
        {
            m_happy.SetActive(happy);
            m_dark.SetActive(dark);
        }
    }

    virtual public void WasHit(int strength)
    { }
}
