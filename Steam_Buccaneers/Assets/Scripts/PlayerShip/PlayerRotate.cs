using UnityEngine;
using System.Collections;

public class PlayerRotate : MonoBehaviour 
{
	public Vector3 uprightRotation = new Vector3 (0f,180f,0f);
	//Vector3 rotateShip = new Vector3 (0.0f, 0.0f, 0.2f);
	Quaternion fuckShit = new Quaternion (0f, 0f, .2f, 0f);
	Vector3 shitFuck = new Vector3 (0f, 0f, 1f);
	public GameObject rotateThis;
	// Use this for initialization
	void Start () 
	{
		rotateThis = GameObject.Find ("Player");
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if (PlayerMove2.turnLeft == true) 
		{
			//rotateThis.transform.rotation =+ Quaternion.Euler (0f, 0f, .2f);
			//rotateThis.transform.localEulerAngles =+ fuckShit;
			//rotateThis.transform.Rotate(transform.right, .5f);
			//rotateThis.transform.localEulerAngles.z += rotateShip.z;
			//rotateThis.transform.Rotate(transform.right, 5 * Time.deltaTime);
			/*rotateThis.transform.localEulerAngles = new Vector3 (0f, uprightRotation.y,
				rotateThis.transform.localEulerAngles.z);*/
			//Debug.Log ("are you doing this?");
			/*if (rotateThis.transform.localEulerAngles.z > uprightRotation.z-20f || rotateThis.transform.localEulerAngles.z > 340f ) //uprightRotation.z+20f)
			{
				Debug.Log ("are you doing this?");*/

				/*//Debug.Log(rotateThis.transform.localEulerAngles.z + " this is rotation1");
				rotateThis.transform.Rotate(transform.right, 5 * Time.deltaTime);
				rotateThis.transform.localEulerAngles = new Vector3 (0f, uprightRotation.y,
					rotateThis.transform.localEulerAngles.z);*/
			//}
		}
		/*
			else
		{
			
			if (rotateThis.transform.localEulerAngles.z < uprightRotation.z)
			{
				Debug.Log ("are you doing this? 2");
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
		}*/

		if (PlayerMove2.turnRight == true) 
		{
			if (rotateThis.transform.localEulerAngles.z < 1f || rotateThis.transform.localEulerAngles.z > 340f)//uprightRotation.z-20f)
			{
				Debug.Log ("are you doing this?4");
				rotateThis.transform.Rotate(-shitFuck, 5*Time.deltaTime);
				//rotateThis.transform.Rotate(-transform.right, 5 * Time.deltaTime);
				/*rotateThis.transform.localEulerAngles = new Vector3 (0f, uprightRotation.y,
					rotateThis.transform.localEulerAngles.z);*/
			}
		}

		else 
		{
			if (rotateThis.transform.localEulerAngles.z < 360)
			{
				rotateThis.transform.Rotate(shitFuck, 5*Time.deltaTime);

				if (rotateThis.transform.localEulerAngles.z <= uprightRotation.z)
				{
					rotateThis.transform.localEulerAngles = new Vector3 (0f, 180f, 0f);
				}
			}
		}

/*	else
		{
			if (rotateThis.transform.localEulerAngles.z > uprightRotation.z )
			{
				Debug.Log ("are you doing this? 3");
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
		}*/
	}
}
