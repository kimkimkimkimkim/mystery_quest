using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour {

	public GameObject ranking;

	// Use this for initialization
	void Start () {
		//初回起動時useridを設定
		if(PlayerPrefs.GetString("userid") == ""){
			System.Guid guid = System.Guid.NewGuid ();
			string uuid = guid.ToString ();
			PlayerPrefs.SetString("userid",uuid);
		}else{
			//Debug.Log(PlayerPrefs.GetString(PPKey.userid.ToString()));
		}
		PlayerPrefs.SetInt("maxStage",72);
		//PlayerPrefs.SetInt("reachStage",0);
		/* ランキングに保存 */
		ranking.GetComponent<CreateRanking>().WriteRanking();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
