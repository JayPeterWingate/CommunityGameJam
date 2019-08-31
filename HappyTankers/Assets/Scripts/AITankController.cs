using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Linq;
public class AITankController : TankController
{
	IEnumerable<TankScript> TankList;
	[SerializeField]NavMeshAgent m_agent;

	bool m_hasPath;
	bool m_lookForEnemies = true;
	public GameObject m_levelSpawned;
	public UnityEvent TargetReached { get; private set; }

	// Start is called before the first frame update
	protected void Init()
    {
		print("HIT");
		TargetReached = new UnityEvent();
		/*TankList =
		(from tank in TankScript.TankList
		 where tank.transform.parent == transform.parent && tank.color != color
		 select tank);*/
	}
	
	protected void SetDestinationNear(Vector3 pos, float range)
	{
		float x = Random.Range(-range, range);
		float z = Random.Range(-range, range);
		SetTarget( pos + new Vector3(x, 0, z));
	}
	protected void Destruct()
	{
		if (TargetReached != null)
		{
			TargetReached.RemoveAllListeners();
		}
		
	}
	

    // Update is called once per frame
    public void Tick()
    {

		// Pathing update
		if (m_hasPath == true) {
			m_agent.velocity = Vector3.zero;
			targetDirection = m_agent.steeringTarget;
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
		//print("Got a path");
		m_hasPath = true;
		if(m_agent.isOnNavMesh)
			m_agent.SetDestination(target);
	}
}
