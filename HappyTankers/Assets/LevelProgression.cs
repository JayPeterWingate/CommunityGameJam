using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelProgression : MonoBehaviour
{
	bool m_done = false;
	int m_cityCount = 0;
	public UnityEvent OnLevelComplete { private set; get;}
    // Start is called before the first frame update
    void Start()
    {
		OnLevelComplete = new UnityEvent();
    }

	public void RegisterCity()
	{
		m_cityCount += 1;
	}
	public void DestroyCity()
	{
		m_cityCount -= 1;
		if(m_cityCount == 0 && OnLevelComplete != null)
		{
            PlayerScript.playerRef.transform.Find("Tank").GetComponent<TankScript>().TeleportOut();
            StartCoroutine(DelayedLevelCompletion());
		}
	}

    private IEnumerator DelayedLevelCompletion()
    {
        yield return new WaitForSeconds(TankScript.TeleportWaitTime);
        OnLevelComplete.Invoke();
    }
}
