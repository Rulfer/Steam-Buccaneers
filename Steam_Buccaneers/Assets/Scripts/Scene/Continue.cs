using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour {

	// Use this for initialization
	public void startWorldMaster () 
	{
		GameControl.control.ChangeScene ("WorldMaster");
	}
}
