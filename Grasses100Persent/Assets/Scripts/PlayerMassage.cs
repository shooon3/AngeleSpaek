using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMassage : MonoBehaviour {
    
    //リスト
    public static List<PlayerMassage> List = new List<PlayerMassage>();//生成セリフ
    private static List<Vector2> Speed = new List<Vector2>();//セリフ速度

    public enum Janle { Break, Bigger, Smoler };//プレイヤーセリフ一覧
    public Janle SetJanle;//設定されるセリフ

    //テキスト
    private const string Break = "こわれろー";
    private const string Bigger = "おおきくなれー";
    private const string Smoler = "ちいさくなれー";

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
        switch (SetJanle){
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
        //各種コンポーネント取得
        TM = GetComponentInChildren<TextMesh>();
        RB2D = GetComponent<Rigidbody2D>();
    }

}
