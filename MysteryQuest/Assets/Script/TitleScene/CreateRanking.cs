using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class CreateRanking : MonoBehaviour {

	public GameObject numPrefab;
	public GameObject scorePrefab;
	public GameObject namePrefab;
	public GameObject myRank;
	public GameObject myScore;
	public GameObject myName;
	public Sprite[] spriteNum = new Sprite[3];

	private GameObject content;
	private GameObject contNum;
	private GameObject contScore;
	private GameObject contUsername;

	DatabaseReference reference;

	private void Start(){
		/* Firebase関連 */
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mysteryquest-7d361.firebaseio.com/");
		

		// Get the root reference location of the database.
		reference = FirebaseDatabase.DefaultInstance.RootReference;

		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
  		var dependencyStatus = task.Result;
  		if (dependencyStatus == Firebase.DependencyStatus.Available) {
		    // Create and hold a reference to your FirebaseApp, i.e.
		    //   app = Firebase.FirebaseApp.DefaultInstance;
		    // where app is a Firebase.FirebaseApp property of your application class.

		    // Set a flag here indicating that Firebase is ready to use by your
		    // application.
		  } else {
		    UnityEngine.Debug.LogError(System.String.Format(
		      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
		    // Firebase Unity SDK is not safe to use here.
		  }
		});

		

		
	}

	private void OnEnable(){
		content = this.transform.GetChild(0).GetChild(0).gameObject;
		contNum = content.transform.Find("Num").gameObject;
		contScore = content.transform.Find("Score").gameObject;
		contUsername = content.transform.Find("Name").gameObject;
		WriteRanking();
		GetRanking();
	}

	public void WriteRanking(){
		// Get the root reference location of the database.
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		//User user = new User(name,score);
		string json = "{\"username\":\"" + PlayerPrefs.GetString("username") + "\",\"score\":" + (-1*PlayerPrefs.GetInt("stage")) +"}";
		//Debug.Log("reference:"+reference);
		reference.Child("ranking").Child(PlayerPrefs.GetString("userid")).SetRawJsonValueAsync(json);
	}

	private void GetRanking(){
		/* containerの中身を空に */
		for(int i=0;i<contNum.transform.childCount;i++){
			Debug.Log("入った");
			Destroy(contNum.transform.GetChild(i).gameObject);
			Destroy(contScore.transform.GetChild(i).gameObject);
			Destroy(contUsername.transform.GetChild(i).gameObject);
		}
		// Set up the Editor before calling into the realtime database.

		// Get the root reference location of the database.
		reference = FirebaseDatabase.DefaultInstance.RootReference;
		//RealtileDataBaseから現在のランキングを取得
		//bossNoのノードからtimeで昇順ソートして最大10件を取る（非同期)
		reference.Child("ranking").OrderByChild("score").GetValueAsync().ContinueWith(task =>{
  		if(task.IsFaulted){ //取得失敗
    	//Handle the Error
			Debug.Log("error");
	  	}else if(task.IsCompleted){ //取得成功
			Debug.Log("取得成功");
		    DataSnapshot snapshot = task.Result; //結果取得
				//Debug.Log(snapshot.ToString());
		    IEnumerator<DataSnapshot> en = snapshot.Children.GetEnumerator(); //結果リストをenumeratorで処理
		    int rank = 1;
			bool flag = false;
			string userid = PlayerPrefs.GetString("userid");
			//Debug.Log("snapshot:" + snapshot);
		    while(en.MoveNext()){ //１件ずつ処理
				DataSnapshot data = en.Current; //データを取る
				string name = data.Child("username").GetValue(true).ToString(); //名前を取得
				string score = data.Child("score").GetValue(true).ToString().Substring(1); //スコアを取る
				if(score=="")score = "0"; //scoreが0の時
				ReflectText(rank,score,name);

				string id = data.Key.ToString(); //useridを取得
				if(id == PlayerPrefs.GetString("userid")){
					//自分のだった時
					AddMyRank(rank, score);
				}
				
		      	rank++;
		    }

			ResizeContent(rank);

		}
		});
	}

	//自分のスコアは別で表示
	private void AddMyRank(int rank,string score_str){
		/* numの生成*/
		rank--;
		myRank.transform.GetChild(0).gameObject.GetComponent<Text>().text = (rank+1).ToString();
		if(rank<3){
			myRank.GetComponent<Image>().sprite = spriteNum[rank];
			myRank.transform.GetChild(0).gameObject.SetActive(false);
		}

		/* score */
		myScore.GetComponent<Text>().text = score_str;

		/* name */
		myName.GetComponent<Text>().text = PlayerPrefs.GetString("username");
	}

	private void ResizeContent(int rank){
		rank--;
		contNum.GetComponent<RectTransform>().sizeDelta = new Vector2(100,(float)(rank * 116 + (rank-1) * 47.5));
		contScore.GetComponent<RectTransform>().sizeDelta = new Vector2(200,(float)(rank * 116 + (rank-1) * 47.5));
		contUsername.GetComponent<RectTransform>().sizeDelta = new Vector2(300,(float)(rank * 116 + (rank-1) * 47.5));
		Debug.Log("rank: " + rank);
	}

	//Textに反映させる
	private void ReflectText(int rank,string score_str,string name_str){
		Debug.Log("username:" + name_str + " score:" + score_str);
		/* numの生成*/
		rank--;
		GameObject num = (GameObject)Instantiate(numPrefab);
		num.transform.GetChild(0).gameObject.GetComponent<Text>().text = (rank+1).ToString();
		if(rank<3){
			num.GetComponent<Image>().sprite = spriteNum[rank];
			num.transform.GetChild(0).gameObject.SetActive(false);
		}
		num.transform.SetParent(contNum.transform,false);

		/* scoreの生成 */
		GameObject score = (GameObject)Instantiate(scorePrefab);
		score.transform.SetParent(contScore.transform,false);
		score.GetComponent<Text>().text = score_str;

		/* nameの生成 */
		GameObject username = (GameObject)Instantiate(namePrefab);
		username.transform.SetParent(contUsername.transform,false);
		username.GetComponent<Text>().text = name_str;
	}	
}
