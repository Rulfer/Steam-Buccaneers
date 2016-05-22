using UnityEngine;
using System.Collections;

public class OnStartDeactivate : MonoBehaviour {
	public GameObject temp;

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive(false);
	}
}
