using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour 
{
	

	// Use this for initialization
	void Start () 
	{
		SceneManager.LoadScene("WorldMaster");
		SceneManager.LoadScene("worldPt1", LoadSceneMode.Additive);
		//SceneManager.LoadSceneAsync("worldPt2");
		//SceneManager.LoadSceneAsync("worldPt3");
		//SceneManager.SetActiveScene(Scene, "WorldMaster");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log(SceneManager.sceneCount);
		//Debug.Log (

	
	}
}
