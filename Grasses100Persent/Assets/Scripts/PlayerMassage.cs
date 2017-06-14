using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMassage : MonoBehaviour {

    public enum PMJanle{Berak,Bigger,Smoler};//プレイヤーの発射するセリフの種類

    public PMJanle Janle;//設定された種類

    //テキスト
    private class MassageText{
        public const string Break  = "こわれろー";
        public const string Bigger = "おおきくなーれ";
        public const string Smoler = "ちいさくなーれ";
    }
    private string Text;
    private TextMesh TM;

    //スプライト
    public Sprite Nomale;//こわれろー
    public Sprite Big;//おおきくなーれ
    public Sprite Smole;//ちいさくなーれ
    private Sprite Sprite;
    private SpriteRenderer SR;

    //リスト
    public static List<PlayerMassage> InstanceMassage = new List<PlayerMassage>();//生成済みセリフ
    private static List<Vector2> IMSpeed = new List<Vector2>();//セリフスピード

    private Rigidbody2D RB2D;

    public static bool IsFreeze{
        set{
            for (int i = 0; i < InstanceMassage.Count; i++){
                //フリーズさせる
                if (value){
                    IMSpeed.Add(InstanceMassage[i].RB2D.velocity);//速度保存
                    InstanceMassage[i].RB2D.velocity = Vector2.zero;//フリーズ
                }
                //動き出す
                else {
                    InstanceMassage[i].RB2D.velocity = IMSpeed[i];
                }
            }

            //リストクリア
            if (!value){
                IMSpeed.Clear();
            }
        }
    }

    private void Awake(){
        SR = GetComponent<SpriteRenderer>();
        TM = GetComponentInChildren<TextMesh>();
        RB2D = GetComponent<Rigidbody2D>();
    }

    public void Changer(){
        //切替準備
        switch (Janle){
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
                Text = MassageText.Break;
                break;
        }

        //表示切替
        //SR.sprite = Sprite;
        TM.text = Text;
    }

}
