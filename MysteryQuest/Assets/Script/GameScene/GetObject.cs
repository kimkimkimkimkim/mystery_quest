using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject : MonoBehaviour {

	public Camera camera_object; //カメラを取得
	public GameObject startSphere;
    private RaycastHit hit; //レイキャストが当たったものを取得する入れ物
	
	private float nextDis = 2;

	private List<Vector3>  lineArr = new List<Vector3>();
	private GameObject prevSphere;
	private GameObject lineObject;
	private GameObject field;
	private bool canDraw = true;
	private Vector3 startPos;

	private void Start(){
		camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();
		lineObject = GameObject.Find("LineRendererObject");
		field = GameObject.Find("Field");
		startPos = startSphere.transform.position;
	}

    void Update () {
        if (Input.GetMouseButton(0)) //マウスがクリックされたら
        {
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入

            if(Physics.Raycast(ray,out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
            	string objectName = hit.collider.gameObject.name; //オブジェクト名を取得して変数に入れる
            	Debug.Log(objectName); //オブジェクト名をコンソールに表示

				if(hit.collider.gameObject.CompareTag("Tile")){
					//hitしたオブジェクトがTileだった場合
					GameObject sphere = hit.collider.gameObject.transform.GetChild(1).gameObject;
					if(lineArr.Count != 0){
						Vector3 angle = sphere.transform.position - lineArr[lineArr.Count - 1];
						Debug.Log("A:"+sphere.transform.position + " B:" + prevSphere.transform.position + " angle:" + angle/angle.magnitude);
						//Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
						Ray rayObstacle= new Ray (prevSphere.transform.position, angle/angle.magnitude);

						//Rayが当たったオブジェクトの情報を入れる箱
						RaycastHit hit;

						//Rayの飛ばせる距離
						int distance = (int)nextDis;
						//distance = 10;

						//Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
						Debug.DrawLine (rayObstacle.origin, rayObstacle.direction * distance, Color.red);

						//もしRayにオブジェクトが衝突したら
						//                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
						if (Physics.Raycast(rayObstacle,out hit,distance))
						{
							//Rayが当たったオブジェクトのtagがPlayerだったら
								Debug.Log ("Rayが"+hit.collider.gameObject.name+"に当たった");
						}
					}
					if(!canDraw)return; //canDrawじゃないとリターン
					if(lineArr.Count == 0 && sphere != startSphere)return; //最初の位置じゃなければリターン
					//隣じゃなければリターン
					if(lineArr.Count != 0){
						float dis = (sphere.transform.position - prevSphere.transform.position).sqrMagnitude;
						if(dis != nextDis*nextDis)return;
					}
					sphere.SetActive(true);
					AddArr(sphere.transform.position);
					prevSphere = sphere;
				}
				
            }
        }

		if  (Input.GetMouseButtonUp(0)){
			DeleteLine();
		}
    }

	private void AddArr(Vector3 pos){
		if(IsExist(pos)){
			//linArrになければ追加
			lineArr.Add(pos);
		}else if(lineArr[lineArr.Count-2] == pos){
			//一個前だったら
			prevSphere.SetActive(false);
			lineArr.RemoveAt(lineArr.Count-1);
		}

		if(lineArr.Count >= 2)CreateLine();
	}


	private bool IsExist(Vector3 pos){
		for(int i=0;i<lineArr.Count;i++){
			if(lineArr[i] == pos)return false;
		}
		return true;
	}

	private void CreateLine(){
		//LineRenderer line = GameObject.Find("LineRendererObject").GetComponent<LineRenderer> ();
		for(int i=0;i<lineObject.transform.childCount;i++){
			Destroy(lineObject.transform.GetChild(i).gameObject);
		}
		for(int i=0;i<lineArr.Count-1;i++){
			GameObject newLine = new GameObject ("Line");
			newLine.transform.SetParent(lineObject.transform,false);
        	LineRenderer line = newLine.AddComponent<LineRenderer> ();
			line.SetPosition(0,lineArr[i]);
			line.SetPosition(1,lineArr[i+1]);
			line.startWidth = 0.5f;
			line.endWidth = 0.5f;
			line.startColor = Color.red;
		}
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


}
