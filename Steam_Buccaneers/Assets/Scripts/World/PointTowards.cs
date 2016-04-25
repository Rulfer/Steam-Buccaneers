using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PointTowards : MonoBehaviour 
{

	public GameObject goTarget;

	void Start()
	{
		if(SceneManager.GetActiveScene().name == "Tutorial")
			goTarget = GameObject.FindGameObjectWithTag ("shop");
		else if(SceneManager.GetActiveScene().name != "Shop" && SceneManager.GetActiveScene().name != "Tutorial")
			goTarget = GameObject.Find ("BossSpawnCompass");
	}

	void Update () 
	{
		if (goTarget == null)
		{
			if(GameObject.Find("TutorialControl") != null)
			{
				Debug.Log ("DialogNummer = " + GameObject.Find ("TutorialControl").GetComponent<Tutorial> ().dialogNumber);
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
					goTarget = GameObject.FindGameObjectWithTag ("shop");
				}
			}
		}
		else
			PositionArrow();        
	}

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
