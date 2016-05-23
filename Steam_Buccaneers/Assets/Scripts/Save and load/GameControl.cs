using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	//Making a controller which is going to last as long as the game is running. Other scrips can access the data without problems
	public static GameControl control;
	public GameObject guiManager;

	//Here is the gamedata saved
	//Ship position this is the position where ship leaves shop.
	//So ship will always load outside last shop visisted
	public Vector3 shipPos;
	//Name of store to spawn outside
	public string storeName = "";
	//Stats
	public int health;
	public int money;
	public int[] canonUpgrades = new int[6];
	public int hullUpgrade;
	public int specialAmmo;
	public int thrusterUpgrade;
	//Saving first death so player dont need to talk to shopkeeper again
	public bool firstDeath = false;
	//Not a saved veriable, but makes it easy for many scripts to see if player is in battle
	public bool isFighting = false;
	//Not a saved varible. Checks if played have had dialog with boss
	public bool talkedWithBoss = false;
	//Saves if treasureplanets are visited, so players cant just load game and go to same planet
	public bool[] treasureplanetsfound = new bool[2];

	public GameObject loadingCanvas;

	private AsyncOperation async;

	void Awake () 
	{
		
		//Using awake() here since it happens before Start(). Since this has to do with loading new scene and keeping the data, it is important to have it as early as possible.
		//if controller doesnt exists it will be made.
		if (control == null) 
		{
			DontDestroyOnLoad (gameObject);
			control = this;
		} 
		//if there is another controller beeing launched from another scene, delete it. WE want to keep the controller from first scene to keep the data the same.
		else if (control != this) 
		{
			Destroy (gameObject);
		}

		//Sets start data
		if (health == 0 && money == 0)
		{
			health = 100;
			money = 20;
			hullUpgrade = 1;
			specialAmmo = 20;
			thrusterUpgrade = 1;

			for (int i = 0; i < canonUpgrades.Length; i ++)
			{
				
				canonUpgrades[i] = 1;
			}
		}
		Debug.Log("Health = " + health);
		Debug.Log("Money = " + money);
	}

	//Runs when scene is loaded
	void OnLevelWasLoaded(int level)
	{
		//Load game when entering worldmaster
		//This will happen from main menu and from shop scene
		if (SceneManager.GetActiveScene ().name == "WorldMaster")
		{
			Load ();
		}
	}

	public void Save(string storesName)
	{
		//This is magic that creates a file in binaryformat
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.ohhijohnny");
		//Initilizes class that can be written to file
		PlayerData data = new PlayerData ();
		storeName = storesName;
		data = saveData(data);
		//Writes to file here. File reference the file we made over and data is the class with the data we want to store.
		bf.Serialize (file, data);
		//Closing file after writing it.
		file.Close ();
	}

	private PlayerData saveData(PlayerData data)
	{
		//Updates controller with current data. Here posstions to player
		//First checks if it is a storesave. If it is not, it will save player position instead of store.
		if (storeName == "exit_store")
		{

		} 
		else if (storeName == "null")
		{
			GameControl.control.shipPos = new Vector3 (0, 0, 2980);
		} 
		else 
		{
			GameObject goP = GameObject.Find (storeName);
			GameControl.control.shipPos = goP.transform.position + (Vector3.forward * 100);
			GameControl.control.shipPos = new Vector3 (GameControl.control.shipPos.x, 0, GameControl.control.shipPos.z);
		}
		//Stores the data we are going to write to file here. All data that are goign to be written to file has to be stored in "data".
		//Writes data to file in GameControl.cs
		data.shipPos = Vector3toFloats(shipPos);
		data.storeName = storeName;
		data.money = money;
		data.health = health;
		data.canonUpgrades = canonUpgrades;
		data.hullUpgrade = hullUpgrade;
		data.specialAmmo = specialAmmo;
		data.thrusterUpgrade = thrusterUpgrade;
		data.firstDeath = firstDeath;
		data.treasureplanetsfound = treasureplanetsfound;

		return data;
	}

	public void Load()
	{
		//Have to check if file exists before attempting to read it
		if (File.Exists (Application.persistentDataPath + "/playerInfo.ohhijohnny") && SceneManager.GetActiveScene().name != "Tutorial")  
		{
			//Makes binaryformatter to be able to convert binary into data
			BinaryFormatter bf = new BinaryFormatter();
			//Opens file. Application.persistentDataPath is unity general savingplace for files. (Somewhere in appdata on windows)
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.ohhijohnny", FileMode.Open);
			//Deserializes the binaryfile to playerdata. 
			PlayerData data = (PlayerData)bf.Deserialize (file);
			//Close file after reading
			file.Close ();
			loadData(data);
		}
	}

	private void loadData(PlayerData data)
	{
		//sets lokal data posisions to what we read of.
		shipPos = FloatstoVector3(data.shipPos);
		//Update gameobjects with loaded data
		GameObject goP = GameObject.Find ("PlayerShip");
		goP.transform.root.position = FloatstoVector3(data.shipPos);
		goP.transform.rotation = Quaternion.Euler(0, 0,0);
		goP.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
		health = data.health;
		money = data.money;
		canonUpgrades = data.canonUpgrades;
		hullUpgrade = data.hullUpgrade;
		specialAmmo = data.specialAmmo;
		thrusterUpgrade = data.thrusterUpgrade;
		firstDeath = data.firstDeath;
		treasureplanetsfound = data.treasureplanetsfound;
		//Update gui
		GameObject.Find ("value_scraps_tab").GetComponent<Text> ().text = money.ToString();
		GameObject.Find ("value_ammo_tab").GetComponent<Text> ().text = specialAmmo.ToString();
	}

	public void newGame()
	{
		//delete savefile
		File.Delete (Application.persistentDataPath + "/playerInfo.ohhijohnny");
	}

	public void ChangeScene(string name)
	{
		//pause game before entering cogscreen for random error to not appear
		if (name == "cog_screen")
		{
			this.GetComponent<GameButtons> ().pause ();
		}
		//Sets loadingscreen active 
		if (loadingCanvas != null)
		{
			loadingCanvas.SetActive (true);
		}
		//hides minimap when loadingscreen is up
		if(GameObject.Find("_GUIManager"))  
			GameObject.Find("_GUIManager").SetActive(false);
		//Keeps loadingscreen going while game is loading
		StartCoroutine(LoadingScreen(name));
		//turns on minimap when loadingscreen is done
		if(GameObject.Find("_GUIManager"))  
			GameObject.Find("_GUIManager").SetActive(true);
		
	}

	IEnumerator LoadingScreen(string sceneName)
	{
		//Loads scene while loadingscreen is up
		async = SceneManager.LoadSceneAsync (sceneName);
		Debug.Log ("Load progress = " + async.progress);
		yield return async;
	}

	private float[] Vector3toFloats(Vector3 vec3)
	{
		//Unity cant write Vector3 to file so need to store the data in a float array
		float[] tempFloat = new float[3];
		tempFloat[0] = vec3.x;
		tempFloat [1] = vec3.y;
		tempFloat [2] = vec3.z;
		return tempFloat;
	}

	private Vector3 FloatstoVector3(float[] tempFloat)
	{
		//Converting the floats we got from stored file in a vector
		Vector3 vec3;
		vec3.x = tempFloat [0];
		vec3.y = tempFloat [1];
		vec3.z = tempFloat [2];
		return vec3;
	}

}

//Must have this over class to make it possible to write to file
[Serializable]
class PlayerData
{
	//Here we store all the data we want to write to file
	public float[] shipPos = new float[3];
	public float[] shipRot = new float[3];
	public string storeName;
	public int health;
	public int money;
	public int[] canonUpgrades = new int[6];
	public int hullUpgrade;
	public int specialAmmo;
	public int thrusterUpgrade;
	public bool firstDeath = false;
	public bool[] treasureplanetsfound = new bool[2];
}
