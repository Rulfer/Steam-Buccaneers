using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatCodesScript : MonoBehaviour 
{
	private GameObject player;
	private GameObject bossSpawn;
	public GameObject shop1;
	public GameObject shop2;
	public GameObject shop3;
	//public GameObject shop4;

	public string stringToEdit = "";
	private Rect windowRect = new Rect(10, 50, 500, 20);
	private string cheatResult = "";

	private bool startTyping = false;
	private bool cheated = false;
	public static bool godMode = false;

	private float killTimer = 0;
	private float killDuration = 5;

	void OnGUI()
	{
		if(startTyping == true)
		{
			if(Event.current.Equals(Event.KeyboardEvent("return")))
			{
				startTyping = false;
				checkCheat();
			}
			stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25);
		}

		if(cheated == true)
		{
			if(killTimer < killDuration)
			{
				killTimer += Time.deltaTime;
				GUI.Label(windowRect, cheatResult);
				Debug.Log("we are displaying shit");
			}
			else
			{
				killTimer = 0;
				cheated = false;
			}
		}

	}

	void Start()
	{
		player = GameObject.Find("PlayerShip");
		bossSpawn = GameObject.Find("BossSpawn");
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(startTyping == true)
			{
				checkCheat();
			}
			else
				stringToEdit = "";
			startTyping = !startTyping;
		}

		if(startTyping == true)
			GUI.enabled = true;
		else
			GUI.enabled = false;
	}

	void checkCheat()
	{
		if(SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "Shop")
		{
			Debug.Log("Cheated)");
			cheated = true;
			switch(stringToEdit)
			{
			case "boss":
			case "Boss":
			case "BOSS":
				player.transform.position = new Vector3 (bossSpawn.transform.position.x, bossSpawn.transform.position.y, bossSpawn.transform.position.z - 100);
				cheatResult = "Cheat activated: Teleporting to Boss spawn location.";
				break;
			case "shop1":
			case "Shop1":
			case "SHOP1":
				player.transform.position = new Vector3 (shop1.transform.position.x, shop1.transform.position.y, shop1.transform.position.z - 100);
				cheatResult = "Cheat activated: Teleporting to Shop 1.";
				break;
			case "shop2":
			case "Shop2":
			case "SHOP2":
				player.transform.position = new Vector3 (shop2.transform.position.x, shop2.transform.position.y, shop2.transform.position.z - 100);
				cheatResult = "Cheat activated: Teleporting to Shop 2.";
				break;
			case "shop3":
			case "Shop3":
			case "SHOP3":
				player.transform.position = new Vector3 (shop3.transform.position.x, shop3.transform.position.y, shop3.transform.position.z - 100);
				cheatResult = "Cheat activated: Teleporting to Shop 3.";
				break;
			case "God":
			case "god":
			case "GOD":
				godMode = !godMode;
				cheatResult = "Cheat activated: No damage from bullets";
				break;
			case "help":
			case "Help":
			case "HELP":
				cheatResult = "Try 'boss', 'shop1', 'shop2', 'shop3' and 'god'";
			default:
				cheatResult = "Error: Incorrect cheat code.";
				break;
			}
		}
	}
}
