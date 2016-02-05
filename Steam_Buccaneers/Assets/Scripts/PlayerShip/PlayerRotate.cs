using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour 
{
	public Vector3 uprightRotation = new Vector3 (0f,180f,0f);
	Vector3 rotateShip = new Vector3 (0.0f, 0.0f, 0.2f);
	public GameObject rotateThis;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if (PlayerMove2.turnLeft == true) 
		{
			if (rotateThis.transform.localEulerAngles.z < uprightRotation.z+20f)
			{
				//Debug.Log(rotateThis.transform.localEulerAngles.z + " this is rotation1");
				rotateThis.transform.Rotate(transform.up, 5 * Time.deltaTime);
				rotateThis.transform.localEulerAngles = new Vector3 (0f, 
					rotateThis.transform.localEulerAngles.z, uprightRotation.z);
			}
		}

			else
		{
			if (rotateThis.transform.localEulerAngles.z > uprightRotation.z)
			{
				//Debug.Log(rotateThis.transform.localEulerAngles.z + " this is rotation2");
				Vector3 rotateBack = rotateThis.transform.localEulerAngles - rotateShip;
				rotateThis.transform.localEulerAngles = rotateBack;
				//Debug.Log(rotateThis.transform.localEulerAngles.z + " this is rotation4");
				//Debug.Log ("i hate you");
				//
				if (rotateThis.transform.localEulerAngles.z <= uprightRotation.z)
				{
					//Debug.Log(rotateThis.transform.localEulerAngles.z + " this is rotation3");
					rotateThis.transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
				}
			}
		}

		if (PlayerMove2.turnRight == true) 
		{
			if (rotateThis.transform.localEulerAngles.z > uprightRotation.z-20f)
			{
				rotateThis.transform.Rotate(-transform.up, 5 * Time.deltaTime);
				rotateThis.transform.localEulerAngles = new Vector3 (0f, 
					rotateThis.transform.localEulerAngles.z, uprightRotation.z);
			}
		}

		else
		{
			if (rotateThis.transform.localEulerAngles.z < uprightRotation.z)
			{
				Vector3 rotateBack = rotateThis.transform.localEulerAngles + rotateShip;
				rotateThis.transform.localEulerAngles = rotateBack;
				//rotateThis.transform.Rotate(transform.up,2f*Time.deltaTime);
				//Debug.Log ("i hate you");
				if (rotateThis.transform.localEulerAngles.z >= uprightRotation.z)
				{
					Debug.Log ("i hate you");
					rotateThis.transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
				}
			}
		}
	}
}
