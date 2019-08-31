using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Linq;
public class AITankController : TankController
{
	IEnumerable<TankScript> TankList;
	[SerializeField] protected NavMeshAgent m_agent;

	bool m_hasPath;
	bool m_lookForEnemies = true;
	public GameObject m_levelSpawned;
	public UnityEvent TargetReached { get; private set; }

	// Start is called before the first frame update
	protected void Init()
    {
		print("HIT");
		TargetReached = new UnityEvent();
		isAI = true;
		/*TankList =
		(from tank in TankScript.TankList
		 where tank.transform.parent == transform.parent && tank.color != color
		 select tank);*/
	}
	
	protected void SetDestinationNear(Vector3 pos, float range)
	{
		Vector3 randomPoint = Random.onUnitSphere * range;
		SetTarget( new Vector3(pos.x + randomPoint.x, pos.y, pos.z + randomPoint.z));
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
			try
			{
				Vector3 dir = m_agent.steeringTarget - transform.Find("Tank").position;
				targetDirection = new Vector2(dir.x, dir.z);
			}
			catch
			{

			}
			
			
			
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
		if (m_agent.isOnNavMesh)
		{
			m_agent.SetDestination(target);
		}
		
	}
}
