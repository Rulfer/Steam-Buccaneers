using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AimCursorPosition : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		this.GetComponent<RawImage>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Input.mousePosition;
	}
}
