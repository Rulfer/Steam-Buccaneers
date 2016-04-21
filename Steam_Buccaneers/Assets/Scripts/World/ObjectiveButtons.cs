using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ObjectiveButtons : MonoBehaviour {

	private PointTowards compassNeedle;
	private Text questInfo;

	void Start()
	{
		compassNeedle = GameObject.Find ("compass_needle").GetComponent<PointTowards> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
	}

	public void PirateLord () 
	{
		compassNeedle.goTarget = GameObject.Find ("BossSpawn");
		questInfo.text = "Find ancient cog!";
	}

	public void Shop () 
	{
		float distance;
		float temp = 1000000000;
		int tempI = 0;
		GameObject[] shops = GameObject.FindGameObjectsWithTag ("shop"); 
		for (int i = 0; i < shops.Length; i++)
		{
			distance = Vector3.Distance(shops[i].transform.position, GameObject.Find("PlayerShip").transform.position);
			if (distance < temp)
			{
				temp = distance;
				tempI = i;
			}
		}
		compassNeedle.goTarget = shops [tempI];
		questInfo.text = "Go to shop!";
	}

	public void Treasure()
	{
		//Wait for treasures to be done
		float distance;
		float temp = 1000000000;
		int tempI = 0;
		GameObject[] treasure = GameObject.FindGameObjectsWithTag ("TreasurePlanet"); 
		for (int i = 0; i < treasure.Length; i++)
		{
			distance = Vector3.Distance(treasure[i].transform.position, GameObject.Find("PlayerShip").transform.position);
			if (distance < temp)
			{
				temp = distance;
				tempI = i;
			}
		}
		compassNeedle.goTarget = treasure [tempI];
		questInfo.text = "Find treasure planet!";
	}
}
