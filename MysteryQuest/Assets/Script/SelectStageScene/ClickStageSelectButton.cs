using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStageSelectButton : MonoBehaviour {

	public void OnClick(){
		FadeManager.Instance.LoadScene("GameScene",1.5f);
	}
}
