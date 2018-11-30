using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickRestartButton : MonoBehaviour {

	public void OnClick(){
		// 現在のScene名を取得する
		Scene loadScene = SceneManager.GetActiveScene();
		// Sceneの読み直し
		SceneManager.LoadScene(loadScene.name);
	}
}
