using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterChange : MonoBehaviour
{
    [SerializeField] GameObject m_happyRender;

    public void ScreenFade()
    {
        m_happyRender.GetComponent<Animation>().Play("FilterTransition");
    }
}
