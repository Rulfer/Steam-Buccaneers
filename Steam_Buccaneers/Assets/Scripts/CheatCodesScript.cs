using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatCodesScript : MonoBehaviour 
{
	private GameObject player; //Reference to the player object
	private GameObject bossSpawn; //Reference to the position of the boss's spawn
	public GameObject shop1; //Reference to the first shop in the game
	public GameObject shop2; //Reference to the second shop in the game
	public GameObject shop3; //Reference to the third shop in the game

	public string stringToEdit = ""; //String the player can edit
	private Rect windowRect = new Rect(10, 50, 500, 20); //Window where the text will appear
	private string cheatResult = ""; //Debug for the player

	private bool startTyping = false; //Used to check if a viable cheat was typed
	private bool cheated = false; //Used to show the cheatResult
	public static bool godMode = false; //Global bool that certain scripts need to access

	//These two floats remove the debug string
	private float killTimer = 0;
	private float killDuration = 5;

	void OnGUI()
	{
		if(startTyping == true) //Player is writing a cheat
		{
			if(Event.current.Equals(Event.KeyboardEvent("return"))) //Player pressed the Enter button
			{
				startTyping = false; //No longer writing
				checkCheat(); //Check what the player wrote
			}
			stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25); //Unity needs to update a TextField every frame
		}

		if(cheated == true) //Player tried to cheat
		{
			if(killTimer < killDuration) //Still show the result of the cheat
			{
				killTimer += Time.deltaTime; //Increase timer
				GUI.Label(windowRect, cheatResult); //Display results
			}
			else //Remove the debug
			{
				killTimer = 0; //Reset timer
				cheated = false; //Reset bool
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
		if(Input.GetKeyDown(KeyCode.Return)) //Player pressed enter
		{
			if(startTyping == true) //A cheat has ben written, check it
			{
				checkCheat();
			}
			else //Open the cheat field so the player can write
				stringToEdit = ""; //Reset he string
			startTyping = !startTyping; //Change the bool
		}

		if(startTyping == true) //The player is going to write something
			GUI.enabled = true; //Show the textfield
		else //The player is done writing
			GUI.enabled = false; //Remove the textfield
	}

	void checkCheat()
	{
		if(SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "Shop") //Makes sure we are in WorldMaster
		{
			cheated = true; //Tells the code to display the results of the cheat
			switch(stringToEdit)
			{
			case "boss": //These three will teleport the player to the boss
			case "Boss":
			case "BOSS":
				player.transform.position = new Vector3 (bossSpawn.transform.position.x, bossSpawn.transform.position.y, bossSpawn.transform.position.z - 100); //Move the player
				cheatResult = "Cheat activated: Teleporting to Boss spawn location."; //Result text
				break;
			case "shop1": //These three will teleport the player to the first shop in the game
			case "Shop1":
			case "SHOP1":
				player.transform.position = new Vector3 (shop1.transform.position.x, shop1.transform.position.y, shop1.transform.position.z - 100); //Move the player
				cheatResult = "Cheat activated: Teleporting to Shop 1.";
				break;
			case "shop2": //These three will teleport the player to the second shop in the game
			case "Shop2":
			case "SHOP2":
				player.transform.position = new Vector3 (shop2.transform.position.x, shop2.transform.position.y, shop2.transform.position.z - 100); //Move the player
				cheatResult = "Cheat activated: Teleporting to Shop 2.";
				break;
			case "shop3": //These three will teleport the player to the third shop in the game
			case "Shop3":
			case "SHOP3":
				player.transform.position = new Vector3 (shop3.transform.position.x, shop3.transform.position.y, shop3.transform.position.z - 100); //Move the player
				cheatResult = "Cheat activated: Teleporting to Shop 3.";
				break;
			case "God": //These three will make cannonballs not trigger on the player, making it easier to test out the enemies in combat
			case "god":
			case "GOD":
				godMode = !godMode;
				cheatResult = "Cheat activated: No damage from bullets";
				break;
			case "help": //These three displays all valid chets
			case "Help":
			case "HELP":
				cheatResult = "Try 'boss', 'shop1', 'shop2', 'shop3', 'god' and 'money'";
				break;
			case "money": //These three will give the player money
			case "Money":
			case "MONEY":
				GameControl.control.money += 1000; //Give the player 1000 money.
				cheatResult = "Gained 1000 scraps."; 
				GameObject.Find("value_scraps_tab").GetComponent<Text>().text = GameControl.control.money.ToString(); // updates players total scrap
				break;
			default: //Invalid cheat
				cheatResult = "Error: Incorrect cheat code."; 
				break;
			}
		}
	}
}
