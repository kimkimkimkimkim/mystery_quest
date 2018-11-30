using UnityEngine;
using System.Collections.Generic;

public enum TileState{
    Root,
    Blank,
    Start,
}

[CreateAssetMenu( menuName = "MyGame/Create StageParameterList", fileName = "StageParameterList" )]
public class StageParameterList : ScriptableObject
{
    private static readonly string RESOURCE_PATH  = "StageParameterList";

    private static  StageParameterList  s_instance    = null;
    public static   StageParameterList  Instance
    {
        get
        {
            if( s_instance == null )
            {
                var asset   = Resources.Load( RESOURCE_PATH ) as StageParameterList; 
                if( asset == null )
                {
                    // アセットが指定のパスに無い。
                    // 誰かが勝手に移動させたか、消しやがったな！
                    Debug.AssertFormat( false, "Missing StageParameterList! path={0}", RESOURCE_PATH );
                    asset   = CreateInstance<StageParameterList>();
                }

                s_instance  = asset;
            }
            
            return s_instance;
        }
    }


    //ListステータスのList
    public List<StageParameter> stageParameterList = new List<StageParameter>();

} // class StageParameterList

//System.Serializableを設定しないと、データを保持できない(シリアライズできない)ので注意
[System.Serializable]
public class StageParameter{

    //設定したいデータの変数
    //public Vector3 heroPos; //プレイヤーの位置

    public List<TilePosition> tileposition = new List<TilePosition>();

}

[System.Serializable]
public class TilePosition{
    //public List<bool> tile = new List<bool>();
    public List<TileState> tilestate = new List<TileState>();
}
