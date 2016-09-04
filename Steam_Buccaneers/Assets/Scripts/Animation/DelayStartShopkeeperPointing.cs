using UnityEngine;
using System.Collections;

//Class delays shop pointing animation so that it doesnt start immidiatly when scene is started.
public class DelayStartShopkeeperPointing : StateMachineBehaviour {

	//  OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		//Stoppping animation when it starts
		animator.speed = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		//After half a secound the animation starts
		if (Time.timeSinceLevelLoad > 0.5 && animator.speed != 1)
		{
			animator.speed = 1;
		}
	}
		
}
