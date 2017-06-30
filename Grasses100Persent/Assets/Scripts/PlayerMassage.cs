using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMassage : MonoBehaviour{

    //リスト
    public static List<PlayerMassage> List = new List<PlayerMassage>();//生成セリフ
    private static List<Vector2> Speed = new List<Vector2>();//セリフ速度

    public enum Janle { Break, Bigger, Smoler };//セリフ状態
    public Janle ThisJanle;//現状態

    //テキスト
    private const string Break = "こわれろー";
    private const string Bigger = "おおきくなれー";
    private const string Smoler = "ちいさくなれー";

    //消滅時計
    private float Timer;//時間計測
    public float DestroyTime;//破壊までの時間

    //コンポーネント
    private TextMesh TM;
    private Rigidbody2D RB2D;

    private static float Z_PosNum = -1;//表示位置調整用変数

    public static bool IsFreeze{
        set{

            for (int i = 0; i < List.Count; i++){

                if (value){//停止
                    Speed.Add(List[i].RB2D.velocity);//速度保存
                    List[i].RB2D.velocity = Vector2.zero;
                }
                else {//再生
                    if (List.Count >= 1 && Speed.Count >= 1){
                        List[i].RB2D.velocity = Speed[i];
                    }

                }

            }

            //リストクリア
            if (!value){
                Speed.Clear();
            }

        }
    }//フリーズ管理変数

    public static void ZAjaster(){
        for (int i = 0; i < List.Count; i++){
            //リスト番号に合わせてｚ値を変化
            Vector3 Pos = List[i].transform.position;
            Pos.z = i / 10.0f * Z_PosNum + Z_PosNum;
            List[i].transform.position = Pos;
        }
    }//表示位置調整メソッド

    public void TextChange(){
        string NewText;

        //SetJanleによって表示テキスト切替
        switch (ThisJanle){
            case Janle.Smoler:
                NewText = Smoler;
                break;
            case Janle.Break:
                NewText = Break;
                break;
            case Janle.Bigger:
                NewText = Bigger;
                break;
            default:
                NewText = "";
                break;

        }

        TM.text = NewText;//表示テキスト変更
    }//Text切替メソッド

    public static void GameEnd(){

        var AnotherList = List.ToArray();//配列化
        foreach (var PM in AnotherList){
            PM.Destroyer();
        }

    }//ゲーム終了処理実行メソッド

    public void Destroyer(){
        List.Remove(this);
        ZAjaster();
        Destroy(this.gameObject);
    }//ゲーム終了処理

    private void Awake(){

        //リスト追加
        List.Add(this);
        ZAjaster();

        //コンポーネント取得
        TM = GetComponentInChildren<TextMesh>();
        RB2D = GetComponent<Rigidbody2D>();

    }

    private void Update(){
        if (RB2D.velocity != Vector2.zero){
            if (Timer > DestroyTime){
                //一定時間経過で破棄
                Destroyer();
            }

            Timer += Time.deltaTime;//時間計測
        }

        //バグ回避
        if (!Main.ShotF && RB2D.velocity == Vector2.zero){
            Destroyer();
        }
    }

}
