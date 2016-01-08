using UnityEngine;
using System.Collections;
using System.IO;

public class Cursor : MonoBehaviour {

	public Texture2D cursorImage;

	// Use this for initialization
	void Start () {
		UnityEngine.Cursor.visible = false;
	}

	void OnGUI()
	{
		Vector3 mousePos = Input.mousePosition;
		GUI.Label (new Rect (mousePos.x, Screen.height - mousePos.y, cursorImage.width, cursorImage.height), cursorImage);
	}
}
