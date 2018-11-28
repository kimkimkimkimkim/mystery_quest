using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHomeButton : MonoBehaviour {

	public void OnClick(){
		SceneManager.LoadScene ("TitleScene");
	}
}
