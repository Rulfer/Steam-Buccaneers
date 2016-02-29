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
			health = 20;
			money = 120;
			hullUpgrade = 1;
			specialAmmo = 1;
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
		if (level == 0) 
		{
			Save (storeName);
		}
	}

	//Makes Gui on launch of program
	void OnGUI()
	{
		//Only for debug purposes to see the saved posistions 
		GUI.Label (new Rect (10, 80, 160, 30), "ShipPos: " + shipPos);
		GUI.Label (new Rect (10, 100, 160, 30), "Last Store: " + storeName);
		GUI.Label (new Rect (10, 120, 160, 30), "Health: " + health);
		GUI.Label (new Rect (10, 140, 160, 39), "Money: " + money);
		GUI.Label (new Rect (10, 160, 160, 30), "SpessAmmo: " + specialAmmo);
		GUI.Label (new Rect (10, 180, 260, 39), "Canon upgrade lvl: " + canonUpgrades[0] + ", " + canonUpgrades[1] + ", " + canonUpgrades[2] + ", " + canonUpgrades[3] + ", " + canonUpgrades[4] + ", " + canonUpgrades[5] + ", " );
		GUI.Label (new Rect (10, 200, 160, 30), "Hullupgrade: " + hullUpgrade);
		GUI.Label (new Rect (10, 220, 160, 30), "thrusterUpgrade: " + thrusterUpgrade);
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
		if (storeName != "null") 
		{
			GameObject goP = GameObject.Find (storeName);
			GameControl.control.shipPos = goP.transform.position+(Vector3.forward*70);
		} 
		else 
		{
			GameObject goP = GameObject.FindGameObjectWithTag ("Player");
			GameControl.control.shipPos = goP.transform.position;
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
			BinaryFormatter bf = new BinaryFormatter ();
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
		GameObject goP = GameObject.FindGameObjectWithTag ("Player");
		goP.transform.root.position = FloatstoVector3(data.shipPos);
		goP.transform.rotation = Quaternion.Euler(0, 0,0);
		goP.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
		health = data.health;
		money = data.money;
		canonUpgrades = data.canonUpgrades;
		hullUpgrade = data.hullUpgrade;
		specialAmmo = data.specialAmmo;
		thrusterUpgrade = data.thrusterUpgrade;
	}

	public void ChangeScene(string name)
	{
		//Changes scene to parameter
		SceneManager.LoadScene (name);
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
