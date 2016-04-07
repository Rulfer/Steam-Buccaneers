using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public RenderTexture miniMapTexture;
	public Material miniMapMaterial;

	private float offset;

	// Use this for initialization
	void Awake () {
		offset = 10;
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		if(Event.current.type == EventType.Repaint)
			//Graphics.DrawTexture(new Rect(0,0, 128, 128), miniMapTexture, miniMapMaterial);
			Graphics.DrawTexture(new Rect(Screen.width / 1.13f, Screen.height / 9.7f, Screen.width / 9.5f, Screen.width / 9.5f), miniMapTexture, miniMapMaterial);
	}
}
