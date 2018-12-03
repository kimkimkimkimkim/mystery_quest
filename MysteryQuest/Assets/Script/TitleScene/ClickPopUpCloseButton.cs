using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPopUpCloseButton : MonoBehaviour {

	public GameObject ranking;
	public GameObject setting;

	public void OnClick(){
		//this.transform.parent.gameObject.SetActive(false);
		ranking.SetActive(false);
		setting.SetActive(false);
	}
}
