using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Linq;
public class AITankController : TankController
{
	IEnumerable<TankScript> TankList;
	Animator m_fsm;
	[SerializeField]NavMeshAgent m_agent;

	bool m_hasPath;
	bool m_lookForEnemies = true;
	public UnityEvent TargetReached { get; private set; }
	// Start is called before the first frame update
	protected void Init()
    {
		print("HIT");
		m_fsm = GetComponent<Animator>();
		TargetReached = new UnityEvent();
		TankList =
		(from tank in TankScript.TankList
		 where tank.transform.parent == transform.parent && tank.color != color
		 select tank);
		StartCoroutine(CheckForEnemies());

	}

	IEnumerator CheckForEnemies()
	{
		while (m_lookForEnemies)
		{
			yield return new WaitForSeconds(1);
			bool canSee = false;
			foreach (TankScript tank in TankList)
			{
				Ray ray = new Ray(transform.position, tank.transform.position);
				RaycastHit info;
				if (Physics.Raycast(ray, out info))
				{
					if (info.collider.gameObject == tank.gameObject)
					{
						canSee = true;
						break;
					}
				}
			}
			m_fsm.SetBool("CanSeeEnemy", canSee);

		}
			
	}

    // Update is called once per frame
    public void Tick()
    {

		// Pathing update
		if (m_hasPath == true) {
			if (m_agent.remainingDistance < m_agent.stoppingDistance && TargetReached != null)
			{
				m_hasPath = false;
				TargetReached.Invoke();
			}
		}
		
		
    }
	public void Fire()
	{
		fireEvent.Invoke();
	}
	public void SetTarget(Vector3 target)
	{
		print("Got a path");
		m_hasPath = true;
		m_agent.SetDestination(target);
	}
}
