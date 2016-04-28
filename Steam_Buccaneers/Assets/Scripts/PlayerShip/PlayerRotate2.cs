using UnityEngine;
using System.Collections;

public class PlayerRotate2 : MonoBehaviour
{
	Vector3 rotationVec = new Vector3 (0f, 0f, 1f);
	Vector3 uprightRot = new Vector3(0f, 180f, 0f);
	private float minDeg = 20f;
	private float maxDeg = 340f;
	private float currentDeg = 0f;



	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		currentDeg = this.transform.localEulerAngles.z;

		//Debug.Log (currentDeg);

		if (PlayerMove2.turnLeft == true) 
		{
			if (currentDeg >= maxDeg || currentDeg <= minDeg)
			{
				this.transform.Rotate(-rotationVec, 10*Time.deltaTime);
			}
			
		}
			
		else if (PlayerMove2.turnRight == true)
		{
			if (currentDeg >= maxDeg || currentDeg <= minDeg)
			{
				this.transform.Rotate(rotationVec, 10*Time.deltaTime);
			}
		}

		else
		{
			if ( currentDeg >= maxDeg-20f)
			{
				this.transform.Rotate(rotationVec, 20*Time.deltaTime);
			}

			if ( currentDeg <= minDeg+20f)
			{
				this.transform.Rotate(-rotationVec, 20*Time.deltaTime);
			}
	
		}

		if ( PlayerMove2.turnLeft == false && PlayerMove2.turnRight == false)
		{
			if (currentDeg < 0.2f || currentDeg > 359.8f)
			{
				this.transform.localEulerAngles = uprightRot;

			}
		}
				

	
	}
}
