using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrevPage : MonoBehaviour {

	public GameObject scrollView;

	public void OnClick(){
		scrollView.GetComponent<PageScrollRect>().MovePrevPage();
	}
}
