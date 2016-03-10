using UnityEngine;
using System.Collections;

public class PointTowards : MonoBehaviour 
{

	private GameObject goTarget;

	void Start()
	{
		goTarget = GameObject.FindGameObjectWithTag ("shop");
	}

	void Update () 
	{
		PositionArrow();        
	}

	void PositionArrow()
	{
		Vector3 v3Pos = Camera.main.WorldToViewportPoint(goTarget.transform.position);

		v3Pos.x -= 0.5f;  // Translate to use center of viewport
		v3Pos.y -= 0.5f; 
		v3Pos.z = 0;      // I think I can do this rather than do a 
		//   a full projection onto the plane

		float fAngle = Mathf.Atan2 (v3Pos.x, v3Pos.y);
		this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (-fAngle * Mathf.Rad2Deg)+180.0f);
	}
}
