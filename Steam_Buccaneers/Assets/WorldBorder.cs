using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldBorder : MonoBehaviour 
{
	private bool isTrespassing = false;
	private float killTimer = 0;
	private float killDuration = 20;
	public Text theText;
	public Text numberText;

	// Update is called once per frame
	void Update () {
		if(isTrespassing == true)
		{
			killDuration -= Time.deltaTime;
			if(killTimer > killDuration)
			{
				GameControl.control.health = -1;
			}
			theText.text = "Return to the playable area!" + "\r\n";
			theText.text += "You will die in         " + "seconds.";
			numberText.text = (Mathf.Round(killDuration * 100f) / 100f).ToString();
		}
		else
		{
			killDuration = 20;
			theText.text = "";
			numberText.text = "";
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			isTrespassing = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			isTrespassing = false;
		}
	}
}
