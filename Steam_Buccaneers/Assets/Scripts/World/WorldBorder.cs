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
			Debug.Log("killDuration = " + killDuration);
			if(killTimer > killDuration)
			{
				GameControl.control.health = -1;
			}
			theText.text = "Return to the playable area!" + "\r\n";
			theText.text += "You will die in         " + "seconds.";
			numberText.text = (Mathf.Round(killDuration * 100f) / 100f).ToString();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.root.name == "PlayerShip")
		{
			isTrespassing = true;
			if(GameObject.Find("SpawnsAI"))
				SpawnAI.spawn.trespassingWorldBorder = true;
			Debug.Log("wat hit me " + other.transform.root.name);

		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform.root.name == "PlayerShip")
		{
			if(GameObject.Find("SpawnsAI"))
				SpawnAI.spawn.trespassingWorldBorder = false;

			killDuration = 20;
			theText.text = "";
			numberText.text = "";
			isTrespassing = false;
		}
	}
}
