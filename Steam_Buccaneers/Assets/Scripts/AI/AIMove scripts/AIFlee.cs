using UnityEngine;
using System.Collections;

public class AIFlee : MonoBehaviour 
{

	public GameObject target;
	private GameObject player;
	private float fleeTimer;
	private float fleeDuration = 3;
	private bool first = true;
	private Vector3 relativePoint;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("PlayerShip");
	}

	private Vector3 fleeTargetPosition()
	{
		fleeTimer = 0;
		relativePoint = transform.InverseTransformPoint(player.transform.position);

		float posX = - relativePoint.x * 10;
		float posZ = - relativePoint.z * 10;

		Vector3 temp = transform.TransformPoint(new Vector3(posX, 0, posZ));

		return temp;
	}

	public void flee()
	{
		if(first)
		{
			target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			target.transform.name = "LOOK AT ME";
			target.transform.position = fleeTargetPosition();
			first = false;
		}
		fleeTimer += Time.deltaTime;
		if(fleeTimer >= fleeDuration)
			target.transform.position = fleeTargetPosition();

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
