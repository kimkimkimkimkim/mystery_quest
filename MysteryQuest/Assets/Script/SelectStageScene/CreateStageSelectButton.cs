using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStageSelectButton : MonoBehaviour {

	public Sprite[] spriteButton = new Sprite[3]; // 0:clear 1:unlock 2:lock

	private GameObject content; //スクロールビューのコンテンツ
	private int reachStage = 0;
	private int maxStage = 0;
	// Use this for initialization
	void Start () {
		reachStage = PlayerPrefs.GetInt("reachStage",0);
		maxStage = PlayerPrefs.GetInt("maxStage",72);
		Debug.Log("reach:" + reachStage + " max:" + maxStage);
		content = transform.GetChild(0).GetChild(0).gameObject;
		CreateButton();
	}

	//Buttonの画像を設定
	private void CreateButton(){
		//Clear
		for(int i=0;i<reachStage;i++){
			GameObject btn = GetButton(i+1);
			btn.GetComponent<ClickStageSelectButton>().stageNum = i + 1;
			btn.GetComponent<Image>().sprite = spriteButton[0];
			btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (i+1).ToString();
			btn.transform.GetChild(1).gameObject.SetActive(true);
		}

		//Unlocked
		for(int i=reachStage;i<reachStage+1;i++){
			GameObject btn = GetButton(i+1);
			btn.GetComponent<ClickStageSelectButton>().stageNum = i + 1;
			btn.GetComponent<Image>().sprite = spriteButton[1];
			btn.transform.GetChild(0).gameObject.GetComponent<Text>().text = (i+1).ToString();
			btn.transform.GetChild(0).gameObject.GetComponent<Text>().transform.localPosition = new Vector3(0,0,0);
			btn.transform.GetChild(1).gameObject.SetActive(false);
		}
		
		//Locked
		for(int i=reachStage+1;i<maxStage;i++){
			GameObject btn = GetButton(i+1);
			btn.GetComponent<Button>().interactable = false;
			btn.GetComponent<Image>().sprite = spriteButton[2];
			btn.transform.GetChild(0).gameObject.SetActive(false);
			btn.transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	//入力されたステージ番号に対応したボタンを返す
	private GameObject GetButton(int stageNum){
		int numInPage = content.transform.GetChild(0).childCount; //１ページ内のボタンの数
		int page = stageNum/numInPage; //何ページ目か
		int count = stageNum%numInPage; //何番目のボタンか
		if(count == 0){
			page--;
			count = numInPage;
		}
		//Debug.Log(stageNum + "ステージは" + (page+1) + "ページ目の" + count + "番目");
		GameObject btn = content.transform.GetChild(page).GetChild(count - 1).gameObject;
		return btn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
