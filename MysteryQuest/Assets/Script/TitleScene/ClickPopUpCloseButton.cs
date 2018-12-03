using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPopUpCloseButton : MonoBehaviour {

	public void OnClick(){
		this.transform.parent.gameObject.SetActive(false);
	}
}
