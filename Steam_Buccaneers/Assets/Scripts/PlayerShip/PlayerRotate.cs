using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour 
{
	public Vector3 rotateShip = new Vector3 (90f,180f,0f);
	public GameObject rotateThis;
	// Use this for initialization
	void Start () 
	{
		rotateThis = GameObject.Find("Small_Ship");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if (PlayerMove2.turnLeft == true) 
		{
			if (rotateThis.transform.localEulerAngles.y < rotateShip.y+20f)
			{
				rotateThis.transform.Rotate(transform.up, 5 * Time.deltaTime);
				rotateThis.transform.localEulerAngles = new Vector3 (90f, 
					rotateThis.transform.localEulerAngles.y, rotateShip.z);
				//schlonger.transform.localEulerAngles.x == stopRotatingShitface.x;
				//schlonger.transform.localEulerAngles.z == stopRotatingShitface.z;
			}
		}

			else
		{
			if (rotateThis.transform.localEulerAngles.y > rotateShip.y)
			{

				rotateThis.transform.Rotate(-transform.up, 2*Time.deltaTime);
				//Debug.Log ("i hate you");
				if (rotateThis.transform.localEulerAngles.y <= rotateShip.y)
				{
					//rotateThis.transform.localEulerAngles = new Vector3 (90f, 180f, 0f);
				}
			}
		}

		if (PlayerMove2.turnRight == true) 
		{
			if (rotateThis.transform.localEulerAngles.y > rotateShip.y-20f)
			{
				rotateThis.transform.Rotate(-transform.up, 5 * Time.deltaTime);
				rotateThis.transform.localEulerAngles = new Vector3 (90f, 
					rotateThis.transform.localEulerAngles.y, rotateShip.z);
			}
		}

		else
		{
			if (rotateThis.transform.localEulerAngles.y < rotateShip.y)
			{
				rotateThis.transform.Rotate(transform.up,2f*Time.deltaTime);
				//Debug.Log ("i hate you");
				if (rotateThis.transform.localEulerAngles.y <= rotateShip.y)
				{
					rotateThis.transform.localEulerAngles = new Vector3 (90f, 180f, 0f);
				}
			}
		}
	}
}
