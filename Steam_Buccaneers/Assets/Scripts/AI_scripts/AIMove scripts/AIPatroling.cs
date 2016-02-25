using UnityEngine;
using System.Collections;


public class AIPatroling : MonoBehaviour {

	public GameObject target;
	private int destPoint = 0;
	private Vector3 patrolPoint;
	private float distanceToObjective;


//	private Script AIPatroling;

	void Start () 
	{
		patrolPoint = spawnAI.patrolPoint;
		target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		target.transform.position = spawnAI.patrolPoint;
		target.GetComponent<MeshRenderer>().enabled = false;
	}

	void Update () 
	{
		// Choose the next destination point when the AI gets
		// close to the current one.
		distanceToObjective = Vector3.Distance (this.transform.position, target.transform.position); //distance between AI and player
		if(GetComponent<AIavoid>().hitObject == false)
			goToPoint();
		if (distanceToObjective < 10f)
		{
			this.GetComponent<AIMaster>().deaktivatePatroling();
		}

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