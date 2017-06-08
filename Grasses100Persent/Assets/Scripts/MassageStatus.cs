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


    private void Start(){
        Sprite = GetComponent<SpriteRenderer>().sprite;
        Text = GetComponentInChildren<TextMesh>().text;
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
