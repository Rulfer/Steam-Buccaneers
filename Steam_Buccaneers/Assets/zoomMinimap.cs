using UnityEngine;
using System.Collections;

public class zoomMinimap : MonoBehaviour
{
	public GameObject Map;
	public static float enemyScale = 6.5f;
	private MapMarker[] mapScript;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void zoomOut()
	{
		if(Map.GetComponent<MapCanvasController>().radarDistance < 500)
		{
			enemyScale *= 0.9f;
			Map.GetComponent<MapCanvasController>().radarDistance += 50f;
			mapScript = Object.FindObjectsOfType<MapMarker>();
			for(int i = 0; i < mapScript.Length; i++)
			{
				mapScript[i].changeScale = true;
				mapScript[i].markerSize *= 0.9f;
				
			}

		}
	}

	public void zoomIn()
	{
		if(Map.GetComponent<MapCanvasController>().radarDistance > 100)
		{
			enemyScale *= 1.111f;
			Map.GetComponent<MapCanvasController>().radarDistance -= 50f;
			mapScript = Object.FindObjectsOfType<MapMarker>();
			for(int i = 0; i < mapScript.Length; i++)
			{
				mapScript[i].changeScale = true;
				mapScript[i].markerSize *= 1.111f;
			}

		}
	}
}
