using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopText : MonoBehaviour {

	private GameObject header;
	private GameObject infoText;
	private GameObject cost;
	private int canonNumber;
	public int[] weaponCost = new int[3]; 

	// Use this for initialization
	void Start () {

		header = GameObject.Find("header");
		infoText = GameObject.Find("description");
		cost = GameObject.Find("value_cost");
		Debug.Log("Start text");
	}


	public void updateText(string buttonName)
	{
		Debug.Log("Change text");
		switch (buttonName)
		{
		case "hull":

			Debug.Log("hull text");

			header.GetComponent<Text>().text = "Hull armor Level I";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "100,-";
			break;

		case "special":
			
			header.GetComponent<Text>().text = "Special ammo";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "20,-";

			break;
		
		case "cannonT1":
			canonNumber = 0;
			header.GetComponent<Text>().text = "Right top cannon upgrade";

			break;

		case "cannonT2":
			canonNumber = 1;
			header.GetComponent<Text>().text = "Right middle cannon upgrade I";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "100,-";

			break;

		case "cannonT3":
			canonNumber = 2;
			header.GetComponent<Text>().text = "Right bottom cannon upgrade I";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "100,-";

			break;

		case "cannonB1":
			canonNumber = 3;
			header.GetComponent<Text>().text = "Left top cannon upgrade I";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "100,-";

			break;

		case "cannonB2":
			canonNumber = 4;
			header.GetComponent<Text>().text = "Left midle cannon upgrade I";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "100,-";

			break;

		case "cannonB3":
			canonNumber = 5;
			header.GetComponent<Text>().text = "Left bottom cannon upgrade I";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "100,-";

			break;

		case "thruster":

			header.GetComponent<Text>().text = "Thruster upgrade I";
			infoText.GetComponent<Text>().text = "orem ipsum dolor sit amet, consectetur adipiscing elit. " +
				"Aliquam lorem nisi, suscipit sit amet fringilla facilisis, hendrerit nec arcu. " +
				"Curabitur viverra condimentum sapien, et porta eros faucibus quis. Etiam semper massa metus, " +
				"pretium tempus augue aliquet eget. Nunc quis scelerisque felis. Praesent aliquam augue hendrerit lectus laoreet, vitae blandit enim rhoncus. " +
				"Quisque rhoncus tristique lectus, eu lacinia ex viverra eu. Praesent iaculis et purus id aliquam. Nullam blandit leo sed sagittis accumsan. " +
				"Curabitur hendrerit turpis sed nisi viverra gravida.";
			cost.GetComponent<Text>().text = "100,-";

			break;

		default:
			
			break;
		}
		int canonLvl = GameControl.control.canonUpgrades[canonNumber];
		header.GetComponent<Text>().text += canonLvl;

		switch(canonLvl)
		{
		case 1:
			infoText.GetComponent<Text>().text = "Info for weapon upgrade 1.";
			cost.GetComponent<Text>().text = "100,-";
			break;
		case 2:
			infoText.GetComponent<Text>().text = "Info for weapon upgrade 2.";
			cost.GetComponent<Text>().text = "150,-";
			break;
		case 3:
			infoText.GetComponent<Text>().text = "Info for weapon upgrade 3.";
			cost.GetComponent<Text>().text = "200,-";
			break;
		default:
			break;
		}
	}
		
}
