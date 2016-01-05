using UnityEngine;
using System.Collections;

public class AIDetect : MonoBehaviour {

	public static bool rightSide;
	public static bool leftSide;
	public static bool almostRightSide;
	public static bool almostLeftSide;
	public static bool frontSide;
	public float traceDistance = 40;
	public static Vector3 relativePoint;
	public Transform player;

	// Use this for initialization
	void Start () {
		rightSide = false;
		leftSide = false;
		almostLeftSide = false;
		almostRightSide = false;
		frontSide = false;
		Debug.Log (traceDistance);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		driveNextToPlayer ();
	}

	void driveNextToPlayer()
	{
		relativePoint = transform.InverseTransformPoint (player.position);

		if (relativePoint.z >= 0.5) {
			almostLeftSide = true;
			almostRightSide = false;
			frontSide = false;
		}

		if (relativePoint.z <= 0.1) {
			almostRightSide = true;
			almostRightSide = false;
			frontSide = false;
		}

		if(relativePoint.z >= 0.2 && relativePoint.z <= 0.4) {
			almostLeftSide = false;
			almostRightSide = false;
			frontSide = true;
		}

		Debug.Log (relativePoint);

	}
}
