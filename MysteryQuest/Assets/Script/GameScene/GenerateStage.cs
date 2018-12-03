using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStage : MonoBehaviour {

	public GameObject tilePrefab;
	public GameObject hero;
	public GameObject field;

	private GameObject map; //tileの親オブジェクtp
	private int fieldLen; //フィールドの幅
	private int xLen; //フィールドのx方向の長さ
	private int zLen; //フィールドのz方向の長さ
	private float tileY=1.6f; //タイルのy座標
	private float tileSize = 2; //タイルのサイズ
	private float heroY = 4.4f;

	private List<List<string>> stage = new List<List<string>>();

	private int tileCount = 0; //タイルの個数
	private int width = 3;
	private int height = 3;
	private int blankNum = 3; //空白の場所の個数の最大値

	private void Start(){
		for(int i=0;i<height;i++){
			List<string> row = new List<string>();
			for(int j=0;j<width;j++){
				row.Add("x");
			}
			stage.Add(row);
		}
		//Debug.Log(UnityEngine.Random.Range(0,1));
		Generate();
		
		// Debug.Log(stage[0][0]+ " " +"o"+ " " +stage[0][2]);
		// Debug.Log("o"+ " " +"o"+ " " +stage[1][2]);
		// Debug.Log("o"+ " " +"o"+ " " +"s");
	}

	private void Generate(){
		/* 配列の初期化 */
		for(int i=0;i<height;i++){
			for(int j=0;j<width;j++){
				stage[i][j] = "x";
			}
		}
		tileCount = 0;
		int startPos = UnityEngine.Random.Range(0,width*height);
		int q = startPos/width; //商
		int r = startPos%width; //余り
		stage[q][r] = "s";
		Move(q,r);
	}

	//与えられた座標から移動する
	private void Move(int h,int w){
		tileCount++;
		/* 0なら行けない 1なら行ける */
		int canMoveLeft = CanMove(h,w-1);
		int canMoveRight = CanMove(h,w+1);
		int canMoveUp = CanMove(h-1,w);
		int canMoveDown = CanMove(h+1,w);
		int[] canMoveArr = new int[]{canMoveLeft,canMoveRight,canMoveUp,canMoveDown}; //配列にした
		
		int canMoveCount = 0; //進むことができる方向の数
		for(int i=0;i<4;i++){
			if(canMoveArr[i] == 1)canMoveCount++;
		}

		if(canMoveCount == 0){
			Finish();
			return; //もし進む方向がなければ終了
		}
		if(width * height - tileCount <= blankNum){
			//残りのマス目がblankNum以下の時
			//一定確率で終了
			int pro = UnityEngine.Random.Range(1,width * height - tileCount);
			if(pro == 1){
				Finish();
				return;
			}
		}
		int moveDir = UnityEngine.Random.Range(0,canMoveCount) + 1; //進む方向
		int counter = 0;
		for(int i=0;i<4;i++){
			if(canMoveArr[i] == 1){
				counter++;
				if(counter == moveDir)moveDir = i; //moveDirに進む方向を入れる
			}
		}

		switch(moveDir){
		case 0:
			//左に進む
			stage[h][w-1] = "o";
			Move(h,w-1);
			break;
		case 1:
			//右に進む
			stage[h][w+1] = "o";
			Move(h,w+1);
			break;
		case 2:
			//上に進む
			stage[h-1][w] = "o";
			Move(h-1,w);
			break;
		case 3:
			//下に進む
			stage[h+1][w] = "o";
			Move(h+1,w);
			break;
		default:
			break;
		}


	}

	//与えられた座標がx(0)かo(1)か返す
	private int CanMove(int h,int w){
		/* 配列の範囲外に行かないように */
		if(h < 0)h = 0;
		if(h >= height)h = height-1;
		if(w < 0)w = 0;
		if(w >= width)w = width -1;
		/* 判定結果を返す */
		if(stage[h][w] == "o" || stage[h][w] == "s"){
			//そこには道がある
			return 0;
		}else{
			//道がない
			return 1;
		}
	}

	private void Finish(){
		//ShowArr();
		if(width * height - tileCount > blankNum){
			//空白が基準値より多いい場合もう一回
			Generate();
		}else{
			int h0 = 0;
			int hM = 0;
			int w0 = 0;
			int wM = 0;
			for(int i=0;i<width;i++){
				if(stage[0][i] == "x")h0++;
				if(stage[height-1][i] == "x")hM++;
			}
			for(int i=0;i<height;i++){
				if(stage[i][0] == "x")w0++;
				if(stage[i][width-1] == "x")wM++;
			}
			/* どれか１つでも全部blankだったらもう一回 */
			if(h0==height || hM==height || w0==width || wM==width){
				Generate();
			}else{
				GenerateField();
			}
		}
	}

	private void GenerateField(){
		ShowArr();
		zLen = height;
		xLen = width;
		map = field.transform.Find("Map").gameObject;
		
		Vector3 startPos = new Vector3(-1*(xLen-1),tileY,zLen-1);
		for(int i=0;i<zLen;i++){
			for(int j=0;j<xLen;j++){
				string state = stage[i][j];
				
				switch(state){
					case "x":
						break;
					case "o":
						//タイルを置く
						Vector3 pos = new Vector3(startPos.x+j*tileSize,tileY,startPos.z-i*tileSize);
						GameObject obj = (GameObject)Instantiate(tilePrefab);
						obj.transform.SetParent(map.transform, false);
						obj.transform.localPosition = pos;
						break;
					case "s":
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

	private void ShowArr(){
		Debug.Log("タイルの個数:" + tileCount);
		for(int i=0;i<height;i++){
			string str = "";
			for(int j=0;j<width;j++){
				str += stage[i][j].ToString() + " ";
			}
			Debug.Log(str);
		}
	}

}
