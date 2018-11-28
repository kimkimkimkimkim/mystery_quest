using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNextPage : MonoBehaviour {

	public GameObject scrollView;

	public void OnClick(){
		scrollView.GetComponent<PageScrollRect>().MoveNextPage();
	}
}
