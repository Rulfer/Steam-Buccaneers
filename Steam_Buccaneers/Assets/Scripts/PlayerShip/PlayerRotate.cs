using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour 
{
	public Vector3 uprightRotation = new Vector3 (90f,180f,0f);
	Vector3 rotateShip = new Vector3 (0.0f, 0.2f, 0.0f);
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
			if (rotateThis.transform.localEulerAngles.y < uprightRotation.y+20f)
			{
				//Debug.Log(rotateThis.transform.localEulerAngles.y + " this is rotation1");
				rotateThis.transform.Rotate(transform.up, 5 * Time.deltaTime);
				rotateThis.transform.localEulerAngles = new Vector3 (90f, 
					rotateThis.transform.localEulerAngles.y, uprightRotation.z);
			}
		}

			else
		{
			if (rotateThis.transform.localEulerAngles.y > uprightRotation.y)
			{
				//Debug.Log(rotateThis.transform.localEulerAngles.y + " this is rotation2");
				Vector3 rotateBack = rotateThis.transform.localEulerAngles - rotateShip;
				rotateThis.transform.localEulerAngles = rotateBack;
				//Debug.Log(rotateThis.transform.localEulerAngles.y + " this is rotation4");
				//Debug.Log ("i hate you");
				//
				if (rotateThis.transform.localEulerAngles.y <= uprightRotation.y)
				{
					//Debug.Log(rotateThis.transform.localEulerAngles.y + " this is rotation3");
					rotateThis.transform.localEulerAngles = new Vector3 (90f, 180f, 0f);
				}
			}
		}

		if (PlayerMove2.turnRight == true) 
		{
			if (rotateThis.transform.localEulerAngles.y > uprightRotation.y-20f)
			{
				rotateThis.transform.Rotate(-transform.up, 5 * Time.deltaTime);
				rotateThis.transform.localEulerAngles = new Vector3 (90f, 
					rotateThis.transform.localEulerAngles.y, uprightRotation.z);
			}
		}

		else
		{
			if (rotateThis.transform.localEulerAngles.y < uprightRotation.y)
			{
				Vector3 rotateBack = rotateThis.transform.localEulerAngles + rotateShip;
				rotateThis.transform.localEulerAngles = rotateBack;
				//rotateThis.transform.Rotate(transform.up,2f*Time.deltaTime);
				//Debug.Log ("i hate you");
				if (rotateThis.transform.localEulerAngles.y >= uprightRotation.y)
				{
					Debug.Log ("i hate you");
					rotateThis.transform.localEulerAngles = new Vector3 (90f, 180f, 0f);
				}
			}
		}
	}
}
