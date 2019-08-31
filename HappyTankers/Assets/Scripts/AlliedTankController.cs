using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlliedTankController : AITankController
{
	public CityScript m_target;
    // Start is called before the first frame update
    void Start()
    {
		Init();
		TargetReached.AddListener(TargetCity);
	}
	private CityScript GetRandomTarget()
	{
		IEnumerable<CityScript> livingCitiesInLevel = m_levelSpawned.GetComponentsInChildren<CityScript>().Where(city => !city.isDead);
		if(livingCitiesInLevel == null) { return null; }
		return livingCitiesInLevel.Skip(Random.Range(0, livingCitiesInLevel.Count() - 1)).First();
	}
	private void OnEnable()
	{
		if (m_levelSpawned == null) { gameObject.SetActive(false); return; }
		m_target = GetRandomTarget();
		SetDestinationNear(m_target.transform.position, 0f);
		StartCoroutine(Aim());
	}
	private IEnumerator Aim()
	{
		while (gameObject.activeInHierarchy)
		{
			yield return new WaitForSeconds(5);
			IEnumerable<CityScript> livingCitiesInLevel = m_levelSpawned.GetComponentsInChildren<CityScript>().Where(city => !city.isDead);
			foreach (CityScript target in livingCitiesInLevel)
			{
				Ray ray = new Ray(transform.position, m_target.transform.position - transform.position);
				Debug.DrawRay(transform.position, m_target.transform.position - transform.position);
				RaycastHit info;
				if(Physics.Raycast(ray,out info))
				{
					m_target = target;
					turretTarget = target.transform.position + new Vector3(0.5f, 0.5f, 0.5f);
					
					break;
				}
				
			}
			yield return new WaitForSeconds(1);
			StartCoroutine(FireRound());
		}
	}
	private void TargetCity()
	{
		if (m_target)
		{
			SetTarget(m_target.transform.position + new Vector3(0.5f, 0, 0.5f));
		}
		
	}
	// Update is called once per frame
	void Update()
    {
		if(m_target && m_target.isDead)
		{
			m_target = GetRandomTarget();
		}

		Tick();
    }
	private IEnumerator FireRound()
	{
		yield return new WaitForSeconds(0.5f);
		fireEvent.Invoke();
	}

	private void OnDestroy()
	{
		Destruct();
	}
}
