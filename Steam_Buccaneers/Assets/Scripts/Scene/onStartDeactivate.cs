using UnityEngine;
using System.Collections;

public class OnStartDeactivate : MonoBehaviour {
	public GameObject temp;

	//Script used for all objects we dont want to see at first.
	//Made because of laziness
	void Start () 
	{
		this.gameObject.SetActive(false);
	}
}
