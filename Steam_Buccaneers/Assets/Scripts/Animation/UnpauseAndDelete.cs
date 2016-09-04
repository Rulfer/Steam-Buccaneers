using UnityEngine;
using System.Collections;

public class UnpauseAndDelete : StateMachineBehaviour {

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		//Script deletes animation after it is played
		if (stateInfo.normalizedTime >= 1)
		{
			
			Destroy (animator.gameObject);
			Debug.Log ("Destroy animation");
		}
	}
}
