using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public RenderTexture miniMapTexture; //Renders the camera on this texture
	public Material miniMapMaterial; //Holds both the map and the texture
	
	// Update is called once per frame
	void OnGUI () 
	{
		if(MinimapCamera.miniCam.isMinimap) //Minimap is visible
		{
			if(Event.current.type == EventType.Repaint) //Draw the minimap
				Graphics.DrawTexture(new Rect(Screen.width / 1.13f, Screen.height / 9.7f, Screen.width / 9.5f, Screen.width / 9.5f), miniMapTexture, miniMapMaterial); 
		}
	}
}
