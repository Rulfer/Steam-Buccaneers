using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	//Lager control som skal vare hele tiden. Selv om scene blir byttet. Det skal også være mulig å få tilgang til uten å referere.
	//Making a controller which is going to last as long as the game is running. 
	public static GameControl control;

	//Her lages spillerdata
	public Vector3 shipPos;
	public Vector3 meteorPos;

	void Awake () 
	{
		//Bruker awake her siden det skjer før Start()
		if (control == null) 
		{
			DontDestroyOnLoad (gameObject);
			control = this;
		} 
		else if (control != this) 
		{
			Destroy (gameObject);
		}
	}
	
	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 30), "ShipPos: " + shipPos);
		GUI.Label (new Rect (10, 40, 150, 30), "MeteorPos: " + meteorPos);
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.john");

		PlayerData data = new PlayerData ();
		data.shipPos = Vector3toFloats(shipPos);
		data.meteorPos = Vector3toFloats(meteorPos);

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.john")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.john", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			shipPos = FloatstoVector3(data.shipPos);
			meteorPos = FloatstoVector3(data.meteorPos);
		}
	}

	private float[] Vector3toFloats(Vector3 vec3)
	{
		float[] tempFloat = new float[3];
		tempFloat[0] = vec3.x;
		tempFloat [1] = vec3.y;
		tempFloat [2] = vec3.z;
		return tempFloat;
	}

	private Vector3 FloatstoVector3(float[] tempFloat)
	{
		Vector3 vec3;
		vec3.x = tempFloat [0];
		vec3.y = tempFloat [1];
		vec3.z = tempFloat [2];
		return vec3;
	}

}

[Serializable]
class PlayerData
{
	public float[] shipPos = new float[3];
	public float[] meteorPos = new float[3];
}
