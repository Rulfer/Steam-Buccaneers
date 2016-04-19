using UnityEngine;
using System.Collections;

public class ChangeBool : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	 //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{


			if (animator.name == "Portrett")
			{
				if (stateInfo.normalizedTime > 1 && !animator.IsInTransition (layerIndex))
				{
					if (animator.GetBool ("isHappyMainCharacter") == true)
					{
						animator.SetBool ("isHappyMainCharacter", false);//Length 2 sec
					}

					if (animator.GetBool ("isAngryMainCharacter") == true)
					{
						animator.SetBool ("isAngryMainCharacter", false);//Length 2.45 sec
					}
				}
			}

			else if (animator.name == "Portrett2_boss")
			{
				if (stateInfo.normalizedTime > 2 && !animator.IsInTransition (layerIndex))
				{
					if (animator.GetBool ("isHappyBoss") == true)
					{
						animator.SetBool ("isHappyBoss", false);//Length 1 sec
					}

					if (animator.GetBool ("isAngryBoss") == true)
					{
						animator.SetBool ("isAngryBoss", false);//Length 1.2 sec
					}
				}
			}

			else if (animator.name == "Portrett2_marine")
			{
				if (stateInfo.normalizedTime > 2 && !animator.IsInTransition (layerIndex))
				{
					if (animator.GetBool ("isHappyMarine") == true)
					{
						animator.SetBool ("isHappyMarine", false);//Length 1.2 sec
					}

					if (animator.GetBool ("isAngryMarine") == true)
					{
						animator.SetBool ("isAngryMarine", false);//Length 1.2 sec
					}
				}
			}


	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
