using UnityEngine;
using System.Collections;

public class randomScaleMove : MonoBehaviour {

	private bool goingDown = false;
	public float upperScaleLimit;
	public float lowerScaleLimit;

	private float tempScaleUpperLimit;
	private float tempScaleLowerLimit;

	public float scalingSpeed;
	public float percentAwayFromOriginalPoint;

	private float distanceBetweenScaling;

	// Use this for initialization
	void Start () 
	{
		distanceBetweenScaling = (upperScaleLimit / 100)*percentAwayFromOriginalPoint;
		setNewLowerScale ();
		setNewUpperScale ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.gameObject.transform.localScale.x >= tempScaleUpperLimit)
			{
				goingDown = true;
				setNewLowerScale ();
			} 
		else if (this.gameObject.transform.localScale.x <= tempScaleLowerLimit)
			{
				goingDown = false;
				setNewUpperScale ();
			}

			if (goingDown == true)
			{
				this.gameObject.transform.localScale -= new Vector3 (scalingSpeed, scalingSpeed, scalingSpeed);
			} 
			else
			{
				this.gameObject.transform.localScale += new Vector3 (scalingSpeed, scalingSpeed, scalingSpeed);
			}
	}

	private void setNewLowerScale()
	{
		tempScaleLowerLimit = Random.Range (lowerScaleLimit - distanceBetweenScaling, lowerScaleLimit + distanceBetweenScaling);
		Debug.Log("New lower limit: " + tempScaleLowerLimit);
	}

	private void setNewUpperScale()
	{
		tempScaleUpperLimit = Random.Range (upperScaleLimit - distanceBetweenScaling, upperScaleLimit + distanceBetweenScaling);
		Debug.Log("New lower limit: " + tempScaleUpperLimit);
	}
}
