using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNextButton : MonoBehaviour {

	public void OnClick(){
		PlayerPrefs.SetInt("nowStage",PlayerPrefs.GetInt("nowStage") + 1);
		FadeManager.Instance.LoadScene("GameScene",0.5f);
	}
}
