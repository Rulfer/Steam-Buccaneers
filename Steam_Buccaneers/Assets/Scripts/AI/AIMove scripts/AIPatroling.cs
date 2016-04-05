using UnityEngine;
using System.Collections;


public class AIPatroling : MonoBehaviour {

	public GameObject target;

	void Start () 
	{
		target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		target.transform.position = SpawnAI.patrolPoint;
	}

	void Update () 
	{
		if(this.GetComponent<AIavoid>().hitFront == false && this.GetComponent<AIavoid>().hitSide == false)
			goToPoint();
	}


	void goToPoint()
	{
		Vector3 relativePoint = transform.InverseTransformPoint(target.transform.position);
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