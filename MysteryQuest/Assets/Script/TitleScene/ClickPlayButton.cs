using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickPlayButton : MonoBehaviour {

	public Sprite[] sprite = new Sprite[2]; //PlayButtonの画像 0→通常 1→Press

	public void OnClick(){
		SceneManager.LoadScene ("StageSelectScene");
	}

	public void PointerDown(){
		GetComponent<Image>().sprite = sprite[1];
	}

	public void PointerUp(){
		GetComponent<Image>().sprite = sprite[0];
	}
}
