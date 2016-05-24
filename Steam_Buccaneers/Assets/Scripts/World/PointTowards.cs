using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PointTowards : MonoBehaviour 
{
	//Compass points towards this
	public GameObject goTarget;

	void Start()
	{
		//Sets target after which scene is active
		//If it is tutorial, shop is set as target while waiting for other to appear
		if(SceneManager.GetActiveScene().name == "Tutorial")
			goTarget = GameObject.FindGameObjectWithTag ("shop");
		//If it is not tutorial it starts by selecting the main quest as target
		else if(SceneManager.GetActiveScene().name != "Shop" && SceneManager.GetActiveScene().name != "Tutorial")
			goTarget = GameObject.Find ("BossSpawnCompass");
	}

	void Update () 
	{
		//If there is no target anymore
		if (goTarget == null)
		{
			//And it is  tutorial
			if(GameObject.Find("TutorialControl") != null)
			{
				//And it is dialog 23 which means that target is scrap after marine
				if (GameObject.Find("TutorialControl").GetComponent<Tutorial>().dialogNumber == 23)
				{
					int i = 0;
					while (goTarget == null)
					{
						if (GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().scrapHolder [i] != null)
						{
							goTarget = GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().scrapHolder [i];
						}
						i++;
						Debug.Log ("Scrapnummer: " + i);
					}
				}
				else
				{
					//Set gotarget to shop if goTarget is null and it is tutorial
					goTarget = GameObject.FindGameObjectWithTag ("shop");
				}
			}
		}
		else
			PositionArrow();        
	}

	//Function that turns arrow towards target
	void PositionArrow()
	{
		Vector3 v3Pos = Camera.main.WorldToViewportPoint(goTarget.transform.position);

		v3Pos.x -= 0.5f;  // Translate to use center of viewport
		v3Pos.y -= 0.5f; 
		v3Pos.z = 0;      // I think I can do this rather than do a 
		//   a full projection onto the plane
		float fAngle = Mathf.Atan2 (v3Pos.x, v3Pos.y);
		this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (-fAngle * Mathf.Rad2Deg));
	}
}
