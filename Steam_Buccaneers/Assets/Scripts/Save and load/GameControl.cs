using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	//Making a controller which is going to last as long as the game is running. Other scrips can access the data without problems
	public static GameControl control;

	//Here is the gamedata saved
	public Vector3 shipPos;
	public string storeName = "";
	public int health;
	public int money;
	public int[] canonUpgrades = new int[6];
	public int hullUpgrade;
	public int specialAmmo;
	public int thrusterUpgrade;

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
		Debug.Log ("Level loaded");

		if (SceneManager.GetActiveScene ().name == "WorldMaster")
		{
			Debug.Log ("Load WorldMaster");
			Load ();
		}
	}

	//Makes Gui on launch of program
	void OnGUI()
	{
		//Only for debug purposes to see the saved posistions 
//		GUI.Label (new Rect (10, 220, 160, 30), "ShipPos: " + shipPos);
//		GUI.Label (new Rect (10, 240, 160, 30), "Last Store: " + storeName);
//		GUI.Label (new Rect (10, 260, 160, 30), "Health: " + health);
//		GUI.Label (new Rect (10, 280, 160, 39), "Money: " + money);
//		GUI.Label (new Rect (10, 300, 160, 30), "SpessAmmo: " + specialAmmo);
//		GUI.Label (new Rect (10, 320, 260, 39), "Canon upgrade lvl: " + canonUpgrades[0] + ", " + canonUpgrades[1] + ", " + canonUpgrades[2] + ", " + canonUpgrades[3] + ", " + canonUpgrades[4] + ", " + canonUpgrades[5] + ", " );
//		GUI.Label (new Rect (10, 340, 160, 30), "Hullupgrade: " + hullUpgrade);
//		GUI.Label (new Rect (10, 360, 160, 30), "thrusterUpgrade: " + thrusterUpgrade);
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
		Debug.Log("Store = " + storeName);
		if (storeName == "exit_store")
		{

		} 
		else if (storeName == "null")
		{
			GameControl.control.shipPos = new Vector3 (0, 0, 2963);
		} 
		else 
		{
			Debug.Log (GameObject.Find (storeName));
			GameObject goP = GameObject.Find (storeName);
			Debug.Log ("Saving store: " + goP);
			GameControl.control.shipPos = goP.transform.position + (Vector3.forward * 70);
			Debug.Log ("Saving pos: " + GameControl.control.shipPos);
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

		return data;
	}

	public void Load()
	{
		//Have to check if file exists before attempting to read it
		if (File.Exists (Application.persistentDataPath + "/playerInfo.ohhijohnny")) 
		{
			//Makes binaryformatter to be able to convert binary into data
			BinaryFormatter bf = new BinaryFormatter();
			//Opens file. Application.persistentDataPath is unity general savingplace for files. (Somewhere in appdata on windows)
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.ohhijohnny", FileMode.Open);
			Debug.Log ("Load savefile");
			//Deserializes the binaryfile to playerdata. 
			PlayerData data = (PlayerData)bf.Deserialize (file);
			//Close file after reading
			file.Close ();
			Debug.Log ("Write data");
			loadData(data);
		}
	}

	private void loadData(PlayerData data)
	{
		//sets lokal data posisions to what we read of.
		shipPos = FloatstoVector3(data.shipPos);
		Debug.Log ("Set shippos = " + shipPos);
		//Update gameobjects with loaded data
		GameObject goP = GameObject.FindGameObjectWithTag ("Player");
		Debug.Log ("Player ship = " + goP);
		goP.transform.root.position = FloatstoVector3(data.shipPos);
		Debug.Log ("Player ship position = " + goP.transform.root.position);
		goP.transform.rotation = Quaternion.Euler(0, 0,0);
		Debug.Log ("Set shiprotation = " + goP.transform.rotation);
		goP.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
		Debug.Log ("Set shipvelocity = " + goP.GetComponentInParent<Rigidbody>().velocity);
		health = data.health;
		money = data.money;
		Debug.Log ("Player health and money = " + health + ", " + money);
		canonUpgrades = data.canonUpgrades;
		Debug.Log ("Canonupgrades = " + canonUpgrades[0] + ", " + canonUpgrades[1] + ", " + canonUpgrades[2] + ", " + canonUpgrades[3]+ ", " + canonUpgrades[4]+ ", " + canonUpgrades[5]);
		hullUpgrade = data.hullUpgrade;
		Debug.Log ("HullUpgrade = " + hullUpgrade);
		specialAmmo = data.specialAmmo;
		Debug.Log ("Spess ammo = " + specialAmmo);
		thrusterUpgrade = data.thrusterUpgrade;
		Debug.Log ("Thruster = " + thrusterUpgrade);
	}

	public void newGame()
	{
		File.Delete (Application.persistentDataPath + "/playerInfo.ohhijohnny");
	}

	public void ChangeScene(string name)
	{
		//Changes scene to parameter
		loadingCanvas.SetActive(true);
		StartCoroutine(LoadingScreen(name));
	}

	IEnumerator LoadingScreen(string sceneName)
	{
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
}
