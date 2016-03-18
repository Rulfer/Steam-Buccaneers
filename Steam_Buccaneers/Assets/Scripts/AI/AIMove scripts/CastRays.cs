using UnityEngine;
using System.Collections;

public class CastRays : MonoBehaviour {

	public Vector3 right;
	public Vector3 left;

	private int detectDistance = 30;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		right = this.transform.TransformDirection(Vector3.right);
		left = this.transform.TransformDirection(Vector3.left);

		Debug.DrawRay(this.transform.position, right * detectDistance, Color.green);
		Debug.DrawRay(this.transform.position, left * detectDistance, Color.blue);

		sensors();
	}

	private void sensors()
	{
		RaycastHit objectHit;
		if(Physics.Raycast(this.transform.position, right, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet" || objectHit.transform.tag == "aiShip" || objectHit.transform.tag == "shopWall") //The planet is in front of the AI
			{
				this.transform.root.GetComponent<AImove>().turnLeft = true;
				this.transform.root.GetComponent<AIavoid>().hitObject = true;
			}
			else
				this.transform.root.GetComponent<AImove>().turnLeft = false;
		}

		else if(Physics.Raycast(this.transform.position, left, out objectHit, detectDistance))
		{
			if(objectHit.transform.tag == "Planet" || objectHit.transform.tag == "aiShip" || objectHit.transform.tag == "shopWall") //The planet is in front of the AI
			{
				this.transform.root.GetComponent<AImove>().turnRight = true;
				this.transform.root.GetComponent<AIavoid>().hitObject = true;
			}
			else
				this.transform.root.GetComponent<AImove>().turnRight = false;
		}
	}
}
