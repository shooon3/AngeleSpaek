using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour {

    //評価パラメータ
    private int NowRated;//現在の評価
    private int MaxRated;//評価最大値
    private int MinRated;//評価最低値

    private int Open;

    //セリフ
    private enum MassageRated{Good,Normal,Bad}//評価段階
    private MassageRated[] WordRated = new MassageRated[10];//評価一覧
    private string[] Words = new string[10];//開示セリフ

    //スプライト
    public Sprite GoodImageMassage;
    public Sprite BadImageMassage;

    //ステータス
    public GameObject Massage;//吹き出し
    private SpriteRenderer MassageSprite;//吹き出しイメージ
    private TextMesh Text;//テキスト


    private void Start(){
        MassageSprite = Massage.GetComponent<SpriteRenderer>();//SpriteRenderer取得
        Text = Massage.transform.GetChild(0).GetComponent<TextMesh>();//TextMesh取得
    }

    //セリフ開示メソッド
    public void OpenMassage(){
        int Index = Random.Range(0, Words.Length);//ランダムに１個抽出

        //吹き出し変更
        Sprite ChangeSprite = new Sprite();
        switch (WordRated[Index]){
            case MassageRated.Good://高評価Spriteに変更
                ChangeSprite = GoodImageMassage;
                break;
            case MassageRated.Bad://悪評価Spriteに変更
                ChangeSprite = BadImageMassage;
                break;
            case MassageRated.Normal://Normalは何もしない
            default:
                break;
        }
        MassageSprite.sprite = ChangeSprite;//スプライト割当

        //テキスト変更
        Text.text = Words[Index];
    }

    //評価
    public void Rated(string Massage){
        //当たったセリフを開示セリフに照らし合わせる
        for (int i = 0; i < Words.Length; i++){
            if (Massage == Words[i]){
                //評価判定
                switch (WordRated[i]){
                    case MassageRated.Good://高評価
                        NowRated++;//評価上げ
                        if(NowRated > MaxRated){
                            NowRated = MaxRated;
                        }
                        break;
                    case MassageRated.Normal://普通
                        break;
                    case MassageRated.Bad://悪評価
                        NowRated--;//評価下げ
                        if(NowRated < MinRated){
                            NowRated = MinRated;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }

}
