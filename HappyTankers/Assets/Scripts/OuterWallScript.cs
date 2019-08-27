using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterWallScript : BlockScript
{
    [SerializeField] SpriteRenderer m_happySprite;
    int m_health = 2;
    float m_redPerc = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetBreakWall(false);
    }

    void Update()
    {
        if (m_redPerc > 0)
        {
            m_redPerc = Mathf.Max(0, m_redPerc - 2 * Time.deltaTime);
            m_happySprite.color = new Vector4(1,1-m_redPerc,1-m_redPerc,1-0.5f*m_redPerc);
        }
    }

    override public void WasHit(int strength)
    {
        if (strength > 1)
        {
            m_redPerc = 1;
            m_health--;
            if (m_health == 0)
            {
                SetBreakWall(true);
            }
        }
        
    }

    private void SetBreakWall(bool broken)
    {
        m_happy.SetActive(!broken);
        m_dark.SetActive(!broken);
    }
}
