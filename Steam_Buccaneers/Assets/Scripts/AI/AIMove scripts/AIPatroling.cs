using UnityEngine;
using System.Collections;


public class AIPatroling : MonoBehaviour {

	public GameObject target;
	private Vector3 relativePoint;

	void Update () 
	{
		if(this.GetComponent<AIavoid>().hitFront == false && this.GetComponent<AIavoid>().hitSide == false)
		{
			if(target != null)
				goToPoint();
		}
	}


	void goToPoint()
	{
		relativePoint = transform.InverseTransformPoint(target.transform.position);
		if(relativePoint.x >-0.1 && relativePoint.x < 0.1)
		{
			if(relativePoint.z >= 0)
			{
				this.GetComponent<AImove>().turnLeft = false;
				this.GetComponent<AImove>().turnRight = false;
			}
			else
			{
				this.GetComponent<AImove>().turnLeft = true;
				this.GetComponent<AImove>().turnRight = false;
			}
		}

		else if(relativePoint.x >= 0)
		{
			this.GetComponent<AImove>().turnLeft = false;
			this.GetComponent<AImove>().turnRight = true;
		}

		else if(relativePoint.z <= 0)
		{
			this.GetComponent<AImove>().turnLeft = true;
			this.GetComponent<AImove>().turnRight = false;
		}
	}
}