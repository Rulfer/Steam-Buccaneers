using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ObjectiveButtons : MonoBehaviour {

	//Script which points compassneedle
	private PointTowards compassNeedle;
	//Textbox which holds questinfo
	private Text questInfo;
	float counter;

	void Start()
	{
		//Sets values for varibles
		compassNeedle = GameObject.Find ("compass_needle").GetComponent<PointTowards> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		counter = 0;
	}

	void Update()
	{
		counter += Time.deltaTime;
		//After 5 sec check if new target is available. This is to remove treasure that is already picked up
		if (counter > 5)
		{
			if (compassNeedle.goTarget != null)
			{
				if (compassNeedle.goTarget.gameObject.tag == "shop")
				{
					Shop ();
				} else if (compassNeedle.goTarget.gameObject.tag == "Treasure")
				{
					Treasure ();
				}
				counter = 0;
			} 
			else
			{
				Treasure ();
			}
		}
	}

	public void PirateLord () 
	{
		//Points to pirate lord and change text
		compassNeedle.goTarget = GameObject.Find ("BossSpawnCompass");
		questInfo.text = "Find ancient cog!";
	}

	public void Shop () 
	{
		//FInds all shops in distance and chooses the closest one for target for compass
		float distance;
		float temp = 1000000000;
		int tempI = 0;
		GameObject[] shops = GameObject.FindGameObjectsWithTag ("shop"); 

		if (shops.Length > 0)
		{
			for (int i = 0; i < shops.Length; i++)
			{
				distance = Vector3.Distance (shops [i].transform.position, GameObject.Find ("PlayerShip").transform.position);
				if (distance < temp)
				{
					temp = distance;
					tempI = i;
				}
			}
			compassNeedle.goTarget = shops [tempI];
			questInfo.text = "Go to shop!";
		} 
		else
		{
			questInfo.text = "No shop close";
		}

	}

	public void Treasure()
	{
		//Finding closest treasure and setting as target for compass
		float distance;
		float temp = 1000000000;
		int tempI = 0;
		GameObject[] treasure = GameObject.FindGameObjectsWithTag ("Treasure"); 

		if (treasure.Length > 0)
		{
			for (int i = 0; i < treasure.Length; i++)
			{
				distance = Vector3.Distance (treasure [i].transform.position, GameObject.Find ("PlayerShip").transform.position);
				if (distance < temp)
				{
					temp = distance;
					tempI = i;
				}
			}

			compassNeedle.goTarget = treasure [tempI];
			questInfo.text = "Find treasure!";
		} 
		else
		{
			questInfo.text = "No treasure close!";
		}
	}
}
