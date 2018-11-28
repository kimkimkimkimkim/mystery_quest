using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveManager : MonoBehaviour {

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;

	private bool canMove = true; //移動できるかどうか
	private float timeMove = 1.5f; //移動時間
	private float disMove = 2f; //移動距離

	private void Update(){
		if(canMove)Flick();
	}

	void Flick(){
	if (Input.GetKeyDown(KeyCode.Mouse0)){
		touchStartPos = new Vector3(Input.mousePosition.x,
									Input.mousePosition.y,
									Input.mousePosition.z);
		}

		if (Input.GetKeyUp(KeyCode.Mouse0)){
			touchEndPos = new Vector3(Input.mousePosition.x,
									Input.mousePosition.y,
									Input.mousePosition.z);
			GetDirection();
		}
	}

	void GetDirection(){
		float directionX = touchEndPos.x - touchStartPos.x;
		float directionY = touchEndPos.y - touchStartPos.y;
		string Direction = "";
		Vector3 targetPos = new Vector3(0,0,0);

     	if (Mathf.Abs(directionY) < Mathf.Abs(directionX)){
       		if (30 < directionX){
                //右向きにフリック
                Direction = "right";
           	}else if (-30 > directionX){
                //左向きにフリック
                Direction = "left";
            }
    	}else if (Mathf.Abs(directionX)<Mathf.Abs(directionY)){
            if (30 < directionY){
                //上向きにフリック
                Direction = "up";
            }else if (-30 > directionY){
                //下向きのフリック
                Direction = "down";
            }
    	}else{
			//タッチを検出
			Direction = "touch";
        }

		switch (Direction){
		case "up":
			//上フリックされた時の処理
			targetPos = new Vector3(transform.position.x,transform.position.y, transform.position.z + disMove);
			Move(targetPos);
		　　 break;
		case "down":
			//下フリックされた時の処理
			targetPos = new Vector3(transform.position.x,transform.position.y, transform.position.z - disMove);
			Move(targetPos);
		　　 break;
		case "right":
	　　　　　//右フリックされた時の処理
			targetPos = new Vector3(transform.position.x + disMove,transform.position.y, transform.position.z);
			Move(targetPos);
			break;
		case "left":
	　　　　　//左フリックされた時の処理
			targetPos = new Vector3(transform.position.x - disMove,transform.position.y, transform.position.z);
			Move(targetPos);
			break;
		case "touch":
	　　　　　 //タッチされた時の処理
			break;
		}
    }

	private void Move(Vector3 targetPos){
		canMove = false;
		GetComponent<Animator> ().SetBool ("isWalk",true);

		//回転
		Vector3 relativePos = targetPos - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;

		iTween.MoveTo(gameObject, iTween.Hash("x",targetPos.x,"z",targetPos.z,"time",timeMove,
			"EaseType",iTween.EaseType.linear,"oncomplete","Stop","oncompletetarget",gameObject));
	}

	private void Stop(){
		canMove = true;
		GetComponent<Animator>().SetBool("isWalk",false);
	}
	

}
