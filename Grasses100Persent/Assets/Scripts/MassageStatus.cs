using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassageStatus : MonoBehaviour {

    public enum PMJanle{Berak,Bigger,Smoler};//プレイヤーの発射するセリフの種類

    public PMJanle Janle;//設定された種類

    //テキスト
    private class MassageText{
        public const string Break  = "こわれろー";
        public const string Bigger = "おおきくなーれ";
        public const string Smoler = "ちいさくなーれ";
    }
    private string Text;

    //スプライト
    public Sprite Nomale;//こわれろー
    public Sprite Big;//おおきくなーれ
    public Sprite Smole;//ちいさくなーれ
    private Sprite Sprite;

    private Rigidbody2D RB2D;

    //リスト
    private static List<MassageStatus> InstanceMassage = new List<MassageStatus>();//生成済みセリフ
    private static List<Vector2> IMSpeed = new List<Vector2>();//セリフスピード

    public static bool IsFreeze{
        set{
            for (int i = 0; i < InstanceMassage.Count; i++){
                if (value){
                    IMSpeed.Add(InstanceMassage[i].RB2D.velocity);//速度保存
                    InstanceMassage[i].RB2D.velocity = Vector2.zero;//停止
                }
                else{
                    Debug.Log("");
                    InstanceMassage[i].RB2D.velocity = IMSpeed[i];//速度登録
                }
            }

            //リストリセット
            if (!value){
                IMSpeed.Clear();
            }
        }
    }

    private void Start(){
        RB2D = GetComponent<Rigidbody2D>();

        Sprite = GetComponent<SpriteRenderer>().sprite;
        Text = GetComponentInChildren<TextMesh>().text;

        //リストに追加
        InstanceMassage.Add(this);
    }

    private void Update(){
        //Janleによってテキスト・スプライトを変更
        switch (Janle) {
            case PMJanle.Berak:
                Sprite = Nomale;
                Text = MassageText.Break;
                break;
            case PMJanle.Smoler:
                Sprite = Smole;
                Text = MassageText.Smoler;
                break;
            case PMJanle.Bigger:
                Sprite = Big;
                Text = MassageText.Bigger;
                break;
            default:
                break;
        }
    }
}
