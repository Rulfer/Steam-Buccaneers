using UnityEngine;
using System.Collections;

public class DoNotDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//used for objects you just want to keep and never loose. Like GameControl
		DontDestroyOnLoad (gameObject);
	
	}
}
