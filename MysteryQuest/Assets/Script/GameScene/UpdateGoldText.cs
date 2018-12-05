using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGoldText : MonoBehaviour {

	public GameObject textGold;

	// Use this for initialization
	void Start () {
		UpdateGold();
	}
	
	public void UpdateGold(){
		textGold.GetComponent<Text>().text = PlayerPrefs.GetInt("gold",0).ToString();
	}
}
