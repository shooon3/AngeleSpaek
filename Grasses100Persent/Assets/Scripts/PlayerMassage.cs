using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMassage : MonoBehaviour {
    
    //リスト
    public static List<PlayerMassage> List = new List<PlayerMassage>();//生成セリフ
    private static List<Vector2> Speed = new List<Vector2>();//セリフ速度

    public enum Janle { Break, Bigger, Smoler };//プレイヤーセリフ一覧
    public Janle ThisJanle;//設定されるセリフ

    //テキスト
    private const string Break = "こわれろー";
    private const string Bigger = "おおきくなれー";
    private const string Smoler = "ちいさくなれー";

    //消滅時計
    private float Timer;//時間計測
    public float DestroyTime;//破壊までの時間

    //ステータス
    private TextMesh TM;
    private Rigidbody2D RB2D;

    //フリーズ
    public static bool IsFreeze{
        set{
            for (int i = 0; i < List.Count; i++){
                //フリーズさせる
                if (value){
                    Speed.Add(List[i].RB2D.velocity);//速度保存
                    List[i].RB2D.velocity = Vector2.zero;//フリーズ
                }
                //動き出す
                else {
                    List[i].RB2D.velocity = Speed[i];
                }
            }

            //リストクリア
            if (!value){
                Speed.Clear();
            }
        }
    }
    
    //Text切替メソッド
    public void TextChange(){
        string Text;

        //SetJanleによって表示テキスト切替
        switch (ThisJanle){
            case Janle.Smoler:
                Text = Smoler;
                break;
            case Janle.Break:
                Text = Break;
                break;
            case Janle.Bigger:
                Text = Bigger;
                break;
            default:
                Text = "";
                break;

        }

        TM.text = Text;//表示テキスト変更
    }

    private void Awake(){
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);//表示位置調整
        
        //各種コンポーネント取得
        TM = GetComponentInChildren<TextMesh>();
        RB2D = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        if (RB2D.velocity != Vector2.zero){
            if (Timer > DestroyTime){
                List.Remove(this);//リストから削除
                Destroy(this.gameObject);
            }

            Timer += Time.deltaTime;//時間計測
        }
    }

}
