﻿using UnityEngine;
using System.Collections;

public class AimCursorPosition : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Input.mousePosition;
	}
}
