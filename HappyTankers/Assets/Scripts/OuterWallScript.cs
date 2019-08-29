using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OuterWallScript : BlockScript
{
    [SerializeField] SpriteRenderer m_happySprite;
    int m_health = 2;
    // Start is called before the first frame update
    void Start()
    {
        SetBreakWall(false);

    }

	IEnumerator TakeHit()
	{
		float redPercent = 1;
		while (redPercent > 0)
		{
			yield return new WaitForEndOfFrame();
			redPercent = Mathf.Max(0, redPercent - 2 * Time.deltaTime);
			m_happySprite.color = new Vector4(1, 1 - redPercent, 1 - redPercent, 1 - 0.5f * redPercent);
		}
	}

	override public void WasHit(int strength)
    {
        if (strength > 1)
        {
			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(TakeHit());
			}
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
		GetComponent<NavMeshObstacle>().carving = !broken;
	}
}
