﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("maxStage",72);
		//PlayerPrefs.SetInt("reachStage",0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
