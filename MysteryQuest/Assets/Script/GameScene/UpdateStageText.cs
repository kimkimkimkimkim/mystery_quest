using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStageText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Text>().text = "Stage " + PlayerPrefs.GetInt("stage",1).ToString();
	}
}
