using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRandomLocation : StateMachineBehaviour
{
	AITankController m_controller;
	Vector3[] m_levelBounds;

    private void SetRandomDirection()
	{
		float x = Random.Range(m_levelBounds[0].x, m_levelBounds[1].x);
		float z = Random.Range(m_levelBounds[0].z, m_levelBounds[1].z);
	 	m_controller.SetTarget(new Vector3(x, 0, z));
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		Vector3 levelPosition = animator.transform.parent.position;
		m_levelBounds = new Vector3[2] { levelPosition + new Vector3(-12, 0, -7), levelPosition + new Vector3(12, 0, 7) }; 
		m_controller = animator.GetComponent<AITankController>();
		SetRandomDirection();
		m_controller.TargetReached.AddListener(SetRandomDirection);

		Debug.Log("Start Move");
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		m_controller.TargetReached.RemoveListener(SetRandomDirection);    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
