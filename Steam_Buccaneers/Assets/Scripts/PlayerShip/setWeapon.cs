﻿using UnityEngine;
using System.Collections;

public class setWeapon : MonoBehaviour {

	private GameObject[] canons;
	public Mesh[] canonMesh = new Mesh[3];

	void Start () 
	{
		canons = new GameObject[6];
		canons[0] = GameObject.Find("cannon_v1_1");
		canons[1] = GameObject.Find("cannon_v1_2");
		canons[2] = GameObject.Find("cannon_v1_3");
		canons[3] = GameObject.Find("cannon_v1_4");
		canons[4] = GameObject.Find("cannon_v1_5");
		canons[5] = GameObject.Find("cannon_v1_6");

		for(int i = 0; i < 6; i++)
		{
			if(GameControl.control.canonUpgrades[i] == 1)
			{
				canons[i].GetComponent<MeshFilter>().mesh = canonMesh[0];
			}
			else if(GameControl.control.canonUpgrades[i] == 2)
			{
				canons[i].GetComponent<MeshFilter>().mesh = canonMesh[1];
			}
			else if(GameControl.control.canonUpgrades[i] == 3)
			{
				canons[i].GetComponent<MeshFilter>().mesh = canonMesh[2];
			}
		}
	}
}
