using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlliedTankController : AITankController
{
	CityScript m_target;
    // Start is called before the first frame update
    void Start()
    {
		Init();
		TargetReached.AddListener(TargetCity);
	}
	private CityScript GetTarget()
	{
		IEnumerable<CityScript> livingCitiesInLevel = m_levelSpawned.GetComponentsInChildren<CityScript>().Where(city => !city.isDead);
		return livingCitiesInLevel.Skip(Random.Range(0, livingCitiesInLevel.Count() - 1)).First();
	}
	private void OnEnable()
	{
		if (m_levelSpawned == null) { gameObject.SetActive(false); return; }
		m_target = GetTarget();
		SetDestinationNear(m_target.transform.position, 2f);
	}
	private void TargetCity()
	{
		SetDestinationNear(m_target.transform.position, 2f);
	}
	// Update is called once per frame
	void Update()
    {
		if(m_target.isDead)
		{
			m_target = GetTarget();
		}

		Tick();
    }

	private void OnDestroy()
	{
		Destruct();
	}
}
