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
    private TextMesh TM;

    //スプライト
    public Sprite Nomale;//こわれろー
    public Sprite Big;//おおきくなーれ
    public Sprite Smole;//ちいさくなーれ
    private Sprite Sprite;
    private SpriteRenderer SR;


    private void Awake(){
        SR = GetComponent<SpriteRenderer>();
        TM = GetComponentInChildren<TextMesh>();
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

    //private void Update(){
    //    //Janleによってテキスト・スプライトを変更
    //    switch (Janle) {
    //        case PMJanle.Berak:
    //            Sprite = Nomale;
    //            Text = MassageText.Break;
    //            break;
    //        case PMJanle.Smoler:
    //            Sprite = Smole;
    //            Text = MassageText.Smoler;
    //            break;
    //        case PMJanle.Bigger:
    //            Sprite = Big;
    //            Text = MassageText.Bigger;
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
