using UnityEngine;
using System.Collections;

public class onStartDeactivate : MonoBehaviour {
	public GameObject temp;

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive(false);
	}
}
