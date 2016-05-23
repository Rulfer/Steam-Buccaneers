using UnityEngine;
using System.Collections;

public class ChangeBool : StateMachineBehaviour {

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{

		//This is where combatanimation gets turned of after played wanted amount of time.
		//Portrett is player animation
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
		//This is boss animation
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
		//This is enemy animation
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

}
