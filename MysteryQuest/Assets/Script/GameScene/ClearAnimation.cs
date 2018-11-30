using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ClearAnimation : MonoBehaviour {

	private GameObject containerUI;
	private GameObject screenDimmed;
	private GameObject titleDeco;
	private GameObject popup;
	private GameObject buttonBlue;
	private GameObject buttonBrown;
	private GameObject buttonGreen;
	private GameObject Title;
	private GameObject Star;
	private GameObject ScoreLabel;
	private GameObject Gold;
	private GameObject Clover;
	private GameObject Horner;


	/* アニメーションの時間 */
	private float timeFadeIn = 0.1f;
	private float timeShowPopUp = 0.5f;

	private void Start(){
		containerUI = transform.GetChild(0).gameObject;
		screenDimmed = containerUI.transform.GetChild(0).gameObject;
		titleDeco = containerUI.transform.GetChild(1).gameObject;
		popup = containerUI.transform.GetChild(2).gameObject;
		buttonBlue = popup.transform.GetChild(0).gameObject;
		buttonBrown = popup.transform.GetChild(1).gameObject;
		buttonGreen = popup.transform.GetChild(2).gameObject;
		Title = popup.transform.GetChild(4).gameObject;
		Star = popup.transform.GetChild(5).gameObject;
		ScoreLabel = popup.transform.GetChild(6).gameObject;
		Gold = popup.transform.GetChild(7).gameObject;
		Clover = popup.transform.GetChild(8).gameObject;
		Horner = popup.transform.GetChild(9).gameObject;
	}

	public void StartAnimation(){
		/*初期状態 */
		containerUI.SetActive(true);
		screenDimmed.SetActive(false);
		titleDeco.SetActive(false);
		popup.SetActive(false);

		ScreenFadeIn(); //screenDimmedをフェードイン
	}

	//screenDimmedのフェードイン
	private void ScreenFadeIn(){
		screenDimmed.SetActive(true);
		iTween.ValueTo(screenDimmed, iTween.Hash("from",0,"to",0.8,"time",timeFadeIn
			,"onupdate","UpdateAlfa","onupdatetarget",gameObject
			,"oncomplete","ShowPopUp","oncompletetarget",gameObject));
	}

	
	private void UpdateAlfa(float a){
		Color c = screenDimmed.GetComponent<Image>().color;
		screenDimmed.GetComponent<Image>().color = new Color(c.r,c.g,c.b,a);
	}

	//ポップアップの表示
	private void ShowPopUp(){
		buttonBlue.SetActive(false);
		buttonBrown.SetActive(false);
		buttonGreen.SetActive(false);
		Title.SetActive(false);
		Star.SetActive(false);
		ScoreLabel.SetActive(false);
		Gold.SetActive(false);
		Clover.SetActive(false);
		Horner.SetActive(false);
		popup.SetActive(true);

		float scaleFrom = 2f;
		//popup
		iTween.ValueTo(popup, iTween.Hash("from",0.2,"to",1,"time",timeShowPopUp
			,"onupdate","UpdateAlfaPopUp","onupdatetarget",gameObject));
		iTween.ScaleFrom(popup, iTween.Hash("x",scaleFrom,"y",scaleFrom,"time",timeShowPopUp));
		//title
		StartCoroutine(DelayMethod(0.2f,() => {
			Title.SetActive(true);
			iTween.ValueTo(Title, iTween.Hash("from",0.2,"to",1,"time",timeShowPopUp
			,"onupdate","UpdateAlfaTitle","onupdatetarget",gameObject));
			iTween.ScaleFrom(Title, iTween.Hash("x",scaleFrom,"y",scaleFrom,"time",timeShowPopUp));
		}));

		StartCoroutine(DelayMethod(0.2f + timeShowPopUp, () => {
			ShowStars();
		}));
	}

	//星の表示
	private void ShowStars(){
		GameObject star1 = Star.transform.GetChild(0).gameObject;
		GameObject star2 = Star.transform.GetChild(1).gameObject;
		GameObject star3 = Star.transform.GetChild(2).gameObject;
		star1.SetActive(false);
		star2.SetActive(false);
		star3.SetActive(false);
		Star.SetActive(true);

		float timeSpan = 0.3f;
		float timeScale = 0.4f;
		float scaleBig = 1.2f;
		StartCoroutine(DelayMethod(0, ()=> {
			Star.transform.GetChild(0).gameObject.SetActive(true);
			
			iTween.ScaleTo(Star.transform.GetChild(0).gameObject,iTween.Hash("x",scaleBig,"y",scaleBig
				,"time",timeScale));
			iTween.ScaleTo(Star.transform.GetChild(0).gameObject,iTween.Hash("x",1,"y",1
				,"time",timeScale,"delay",timeScale));
		}));
		StartCoroutine(DelayMethod(timeSpan, ()=> {
			Star.transform.GetChild(1).gameObject.SetActive(true);
			
			iTween.ScaleTo(Star.transform.GetChild(1).gameObject,iTween.Hash("x",scaleBig,"y",scaleBig
				,"time",timeScale));
			iTween.ScaleTo(Star.transform.GetChild(1).gameObject,iTween.Hash("x",1,"y",1
				,"time",timeScale,"delay",timeScale));
		}));
		StartCoroutine(DelayMethod(timeSpan*2, ()=> {
			Star.transform.GetChild(2).gameObject.SetActive(true);
			
			iTween.ScaleTo(Star.transform.GetChild(2).gameObject,iTween.Hash("x",scaleBig,"y",scaleBig
				,"time",timeScale));
			iTween.ScaleTo(Star.transform.GetChild(2).gameObject,iTween.Hash("x",1,"y",1
				,"time",timeScale,"delay",timeScale,"oncomplete","ShowOther","oncompletetarget",gameObject));
		}));
	}

	private void ShowOther(){
		AnimPopUp(ScoreLabel);
		StartCoroutine(DelayMethod(0.1f,() => {
			AnimPopUp(ScoreLabel);
		}));
		StartCoroutine(DelayMethod(0.2f,() => {
			AnimPopUp(Gold);
		}));
		StartCoroutine(DelayMethod(0.3f,() => {
			AnimPopUp(Clover);
		}));
		StartCoroutine(DelayMethod(0.4f,() => {
			AnimPopUp(Horner);
		}));
		StartCoroutine(DelayMethod(0.5f,() => {
			AnimPopUp(buttonBlue);
		}));
		StartCoroutine(DelayMethod(0.6f,() => {
			AnimPopUp(buttonBrown);
		}));
		StartCoroutine(DelayMethod(0.7f,() => {
			AnimPopUp(buttonGreen);
		}));
	}

	private void AnimPopUp(GameObject obj){
		obj.SetActive(true);

		iTween.ScaleFrom(obj, iTween.Hash("x",0.1f,"y",0.1f,"time",0.2f));
		iTween.ScaleTo(obj,iTween.Hash("x",1.2f,"y",1.2f,"time",0.1f,"delay",0.2f));
		iTween.ScaleTo(obj,iTween.Hash("x",1.2f,"y",1.2f,"time",0.1f,"delay",0.3f));
	}

	private void UpdateAlfaPopUp(float a){
		Color c = popup.GetComponent<Image>().color;
		popup.GetComponent<Image>().color = new Color(c.r,c.g,c.b,a);
	}

	private void UpdateAlfaTitle(float a){
		Color c = Title.GetComponent<Image>().color;
		Title.GetComponent<Image>().color = new Color(c.r,c.g,c.b,a);
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
