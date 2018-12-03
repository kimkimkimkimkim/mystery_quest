using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopupManager : MonoBehaviour {

	public GameObject textPlaceholder;
	public GameObject textName;
	public GameObject textScore;
	public GameObject toggleVolume;
	public GameObject toggleVibe;
	public GameObject ranking;

	// Use this for initialization
	void Start () {
		/* 初期値代入 */
		textPlaceholder.GetComponent<Text>().text = PlayerPrefs.GetString("username");
		textScore.GetComponent<Text>().text = PlayerPrefs.GetInt("stage").ToString();
		
	}

	void OnEnable(){
		textPlaceholder.GetComponent<Text>().text = PlayerPrefs.GetString("username");
		textScore.GetComponent<Text>().text = PlayerPrefs.GetInt("stage").ToString();
	}

	public void EndEdit(){
		//Debug.Log(textName.GetComponent<Text>().text);
		//username更新
		PlayerPrefs.SetString("username",textName.GetComponent<Text>().text);
		ranking.GetComponent<CreateRanking>().WriteRanking();
	}

	public void VolumeToggleValueChange(bool b){
		bool isOn = toggleVolume.GetComponent<Toggle>().isOn;
		if(isOn){
			//右に移動
			iTween.MoveTo(toggleVolume, iTween.Hash("x",48.7,"time",0.5f,"isLocal",true));
		}else{
			//左に移動
			iTween.MoveTo(toggleVolume, iTween.Hash("x",-1 * 48.7,"time",0.5f,"isLocal",true));
		}
	}

	public void VibeToggleValueChange(bool b){
		bool isOn = toggleVibe.GetComponent<Toggle>().isOn;
		if(isOn){
			//右に移動
			iTween.MoveTo(toggleVibe, iTween.Hash("x",48.7,"time",0.5f,"isLocal",true));
		}else{
			//左に移動
			iTween.MoveTo(toggleVibe, iTween.Hash("x",-1 * 48.7,"time",0.5f,"isLocal",true));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
