using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStageSelectButton : MonoBehaviour {

	public int stageNum;

	public void OnClick(){
		PlayerPrefs.SetInt("nowStage",stageNum);
		Debug.Log("nowStage" + PlayerPrefs.GetInt("nowStage"));
		FadeManager.Instance.LoadScene("GameScene",0.5f);
	}
}
