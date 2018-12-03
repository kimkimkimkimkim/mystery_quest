using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPopUpRanking : MonoBehaviour {

	private GameObject canvasUI;
	private GameObject popup;
	private GameObject ranking;

	private void Start(){
		canvasUI = GameObject.Find("CanvasUI");
		popup = canvasUI.transform.GetChild(1).gameObject;
		ranking = popup.transform.GetChild(1).gameObject;
	}


	public void OnClick(){
		popup.SetActive(true);
		ranking.SetActive(true);
	}
}
