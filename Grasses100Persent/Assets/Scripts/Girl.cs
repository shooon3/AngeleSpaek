using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour {

    //評価パラメータ
    public int NowRated;//現在の評価
    private int MaxRated = 5;//評価最大値
    private int MinRated = -5;//評価最低値

    //タイマー
    private float Timer;//経過時間
    public float Interval;//間隔

    //セリフ
    private int TalkTitleNum;
    private string TalkTitle;//話題

    //ステータス
    public TextMesh Text;//テキスト

    public void IsTalkTitleChange(){
        Timer += Time.deltaTime;
        if(Timer > Interval){
            TalkTitleChange();
            Timer = 0;
        }
    }

    //話題変更メソッド
    private void TalkTitleChange() { 
        //次の話題を決定
        int index = new int();
        while (true){
            index = Random.Range(0, MassageList.UseMassage.Length);
            //０番は使用しない
            if (MassageList.UseMassage[index] != 0) {
                //同じ話題にしないこと
                break;
            }
        }
        TalkTitle = MassageList.Massage[MassageList.UseMassage[index]];
        Text.text = TalkTitle;
    }

    //評価メソッド
    public void Rated(string Massage){
        //表示中の話題と同じ話題なら評価ＵＰ
        if (Massage == TalkTitle){
            NowRated++;
            if(NowRated > MaxRated){
                NowRated = MaxRated;
            }
        }
        //違う話題なら評価ＤＯＷＮ
        else{
            NowRated--;
            if(NowRated < MinRated){
                NowRated = MinRated;
            }
        }
    }

    private void Awake(){
        TalkTitleChange();
    }
}
