using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class testChangingcolor : MonoBehaviour {

	private bool goingDown = true;
	private Button blinkingButtons;
	private ColorBlock cb;

	void Start()
	{
		blinkingButtons = this.GetComponent<Button> ();

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Start Blinking!");
			Debug.Log ("Mengde rødfarge: " + blinkingButtons.colors.normalColor.r);
		Debug.Log (goingDown);
			cb = blinkingButtons.colors;
			if (blinkingButtons.colors.normalColor.g >= 1)
			{
				goingDown = true;
			} 
			else if (blinkingButtons.colors.normalColor.g <= 0)
			{
				goingDown = false;
			}

			if (goingDown == true)
			{
			cb.normalColor = new Color (cb.normalColor.r, cb.normalColor.g- 0.01f, cb.normalColor.b- 0.01f);
			} 
			else
			{
			cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g+0.01f, cb.normalColor.b+0.01f);
			}

			blinkingButtons.colors = cb;
		}
}
