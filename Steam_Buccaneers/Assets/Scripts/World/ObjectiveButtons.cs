using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ObjectiveButtons : MonoBehaviour {

	private PointTowards compassNeedle;
	private Text questInfo;

	float counter;

	void Start()
	{
		compassNeedle = GameObject.Find ("compass_needle").GetComponent<PointTowards> ();
		questInfo = GameObject.Find ("quest_info_text").GetComponent<Text> ();
		counter = 0;
	}

	void Update()
	{
		counter += Time.deltaTime;

		if (counter > 10)
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
		compassNeedle.goTarget = GameObject.Find ("BossSpawnCompass");
		questInfo.text = "Find ancient cog!";
	}

	public void Shop () 
	{
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
		//Wait for treasures to be done
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
