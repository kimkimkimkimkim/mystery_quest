using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetObject : MonoBehaviour {

	public Camera camera_object; //カメラを取得
	public GameObject startSphere;
	public GameObject goalSphere;
	public GameObject linePrefab;
	public GameObject hero;
    private RaycastHit hit; //レイキャストが当たったものを取得する入れ物
	
	private float nextDis = 2;

	private List<Vector3>  lineArr = new List<Vector3>();
	private GameObject prevSphere;
	private GameObject lineObject;
	private GameObject field;
	private bool canDraw = true;
	private bool isGoal = false;
	private bool canTouch = true;
	private Vector3 startPos;

	private void Start(){
		camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();
		lineObject = GameObject.Find("LineRendererObject");
		field = GameObject.Find("Field");
		//startPos = startSphere.transform.position;
	}

    void Update () {
		if(!canTouch)return;
        if (Input.GetMouseButton(0)) //マウスがクリックされたら
        {
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入
            if(Physics.Raycast(ray,out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
            	string objectName = hit.collider.gameObject.name; //オブジェクト名を取得して変数に入れる

				if(hit.collider.gameObject.CompareTag("Tile")){
					//hitしたオブジェクトがTileだった場合
					GameObject sphere = hit.collider.gameObject.transform.GetChild(1).gameObject;
					if(lineArr.Count == 0){
						//最初はヒーローのいる位置じゃないとだめ
						if(!(sphere.transform.position.x == hero.transform.position.x &&
							sphere.transform.position.z == hero.transform.position.z))return;
					}
					/* Arrにもともと入ってたらそのまま新しくて隣なら追加 */
					CheckArr(hit.collider.gameObject);
				}
				
            }
        }
    }

	private void CheckArr(GameObject tile){
		Vector3 spherePos = tile.transform.GetChild(1).gameObject.transform.position;
		GameObject sphere = tile.transform.GetChild(1).gameObject;
		if(!IsExist(spherePos)){
			//Arrにない
			if(lineArr.Count==0){
				//最初なら追加
				lineArr.Add(spherePos);
				prevSphere = sphere;
				sphere.SetActive(true);
				CreateLine();
			}else{
				/* 隣かどうか */
				float dis = (spherePos - lineArr[lineArr.Count-1]).sqrMagnitude;
				if(dis == nextDis*nextDis){
					//隣なら追加
					lineArr.Add(spherePos);
					prevSphere = sphere;
					sphere.SetActive(true);
					CreateLine();
				}
			}
		}else {
			if(lineArr.Count >= 2){
				if(lineArr[lineArr.Count - 2] == spherePos){
					//Arrにあったけど１個前なので最新のやつを消す
					prevSphere.SetActive(false);
					prevSphere = sphere;
					lineArr.RemoveAt(lineArr.Count-1);
					CreateLine();
				}
			}
		}
	}


	private bool IsExist(Vector3 pos){
		for(int i=0;i<lineArr.Count;i++){
			if(lineArr[i] == pos)return true;
		}
		return false;
	}

	private void CreateLine(){
		//LineRenderer line = GameObject.Find("LineRendererObject").GetComponent<LineRenderer> ();
		for(int i=0;i<lineObject.transform.childCount;i++){
			Destroy(lineObject.transform.GetChild(i).gameObject);
		}
		for(int i=0;i<lineArr.Count-1;i++){
			//GameObject newLine = new GameObject ("Line");
			GameObject newLine = (GameObject)Instantiate(linePrefab);
			newLine.name = "Line";
			newLine.transform.SetParent(lineObject.transform,false);
			//LineRenderer line = newLine.AddComponent<LineRenderer> ();
			LineRenderer line = newLine.GetComponent<LineRenderer> ();
			line.SetPosition(0,lineArr[i]);
			line.SetPosition(1,lineArr[i+1]);
			line.startWidth = 0.5f;
			line.endWidth = 0.5f;
		}

		//クリアしたか判定
		//lineArrの要素数が全タイルの要素数と等しいとクリア
		if(lineArr.Count == field.transform.Find("Map").transform.childCount)MoveHero();
	}

	private void DeleteLine(){
		for(int i=0;i<lineObject.transform.childCount;i++){
			Destroy(lineObject.transform.GetChild(i).gameObject);
		}
		GameObject map = field.transform.Find("Map").gameObject;
		for(int i=0;i<map.transform.childCount;i++){
			map.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
		}
		lineArr.Clear();
		canDraw=true;
	}

	private void MoveHero(){
		hero.GetComponent<Animator>().SetBool("isWalk",true);
		canTouch = false;
		for(int i=1;i<lineArr.Count;i++){
			NextMove(i);
		}
	}

	private void NextMove(int i){
		float timeMove = 0.3f;
		StartCoroutine(DelayMethod(timeMove * (i-1), () => {
			//回転
			Vector3 relativePos = lineArr[i] - lineArr[i-1];
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			hero.transform.rotation = rotation;
			iTween.MoveTo(hero, iTween.Hash("position",lineArr[i],"time",timeMove,"EaseType",iTween.EaseType.linear));
			if(i==lineArr.Count-1){
				Invoke("ClearGame",timeMove);
			}
		}));
	}

	private void ClearGame(){
		hero.GetComponent<Animator>().SetBool("isWalk",false);
		GameObject.Find("Canvas").GetComponent<ClearAnimation>().StartAnimation();
		DeleteLine();

	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}


}
