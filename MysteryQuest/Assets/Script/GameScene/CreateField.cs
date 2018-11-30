using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateField : MonoBehaviour {

	public GameObject tilePrefab;
	public GameObject hero;

	private GameObject map; //tileの親オブジェクtp
	private int fieldLen; //フィールドの幅
	private int xLen; //フィールドのx方向の長さ
	private int zLen; //フィールドのz方向の長さ
	private float tileY=1.6f; //タイルのy座標
	private float tileSize = 2; //タイルのサイズ
	private float heroY = 4.4f;

	private void Start(){
		map = transform.Find("Map").gameObject;
		int stageNum = PlayerPrefs.GetInt("nowStage");
		StageParameter stagePara = StageParameterList.Instance.stageParameterList[stageNum-1];
		//Vector3 heroPos = stagePara.heroPos;
		List<TilePosition> tilePosition = stagePara.tileposition;
		fieldLen = tilePosition.Count;
		zLen = tilePosition.Count;
		xLen = tilePosition[0].tilestate.Count;
		
		Vector3 startPos = new Vector3(-1*(xLen-1),tileY,zLen-1);
		for(int i=0;i<zLen;i++){
			List<TileState> tile = tilePosition[i].tilestate;
			for(int j=0;j<xLen;j++){
				TileState state = tile[j]; //この位置にタイルを置くかどうか
				
				switch(state){
					case TileState.Blank:
						break;
					case TileState.Root:
						//タイルを置く
						Vector3 pos = new Vector3(startPos.x+j*tileSize,tileY,startPos.z-i*tileSize);
						GameObject obj = (GameObject)Instantiate(tilePrefab);
						obj.transform.SetParent(map.transform, false);
						obj.transform.localPosition = pos;
						break;
					case TileState.Start:
						//タイルを置く
						Vector3 pos2 = new Vector3(startPos.x+j*tileSize,tileY,startPos.z-i*tileSize);
						GameObject obj2 = (GameObject)Instantiate(tilePrefab);
						obj2.transform.SetParent(map.transform, false);
						obj2.transform.localPosition = pos2;
						hero.transform.position = new Vector3(pos2.x,heroY,pos2.z);
						break;
				}
			}
		}

		
	}
}
