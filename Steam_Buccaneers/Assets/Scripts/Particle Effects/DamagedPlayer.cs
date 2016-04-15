﻿using UnityEngine;
using System.Collections;

public class DamagedPlayer : MonoBehaviour 
{
	public static DamagedPlayer dmPlayer;
	public GameObject[] smokeParticles;
	public GameObject[] fireParticles;
	// Use this for initialization
	void Start () {
		dmPlayer = this;
		foreach(GameObject go in smokeParticles)
			go.SetActive(false);
	}
}
