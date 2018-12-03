using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNextButton : MonoBehaviour {

	public void OnClick(){
		//PlayerPrefs.SetInt("stage",PlayerPrefs.GetInt("stage") + 1);
		FadeManager.Instance.LoadScene("GameScene",0.5f);
	}
}
