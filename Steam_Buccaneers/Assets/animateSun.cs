using UnityEngine;
using System.Collections;

public class animateSun : MonoBehaviour {

	public Material[] sunTextures = new Material[71];
	private float temp;
	
	// Update is called once per frame
	void Update () 
	{
		temp += Time.deltaTime*24;
		this.gameObject.GetComponent<MeshRenderer> ().material = new Material(sunTextures[Mathf.RoundToInt(temp)]);
		Debug.Log (this.gameObject.GetComponent<MeshRenderer> ().material);
		Debug.Log (temp);

		if (temp >= 71)
			temp = 0;
	}
}
