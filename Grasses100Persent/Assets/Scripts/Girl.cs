using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour {

    //評価パラメータ
    public int NowRated;//現在の評価
    private int MaxRated = 5;//評価最大値
    private int MinRated = -5;//評価最低値

    //話題切り替えタイマー
    private float Timer;//経過時間
    public float Interval;//間隔

    //話題提供吹き出し
    private int TalkTitleNum;
    private string TalkTitle;//話題

    //ステータス
    public TextMesh Text;//テキスト

    //彼女のボイス集
    public AudioClip DownVoice1;
    public AudioClip DownVoice2;
    public AudioClip DownVoice3;
    public AudioClip UpVoice1;
    public AudioClip UpVoice2;
    public AudioClip ChangeVoice1;
    public AudioClip ChangeVoice2;


    //話題変更タイマー
    public void TalkTitleTimer(){
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
            //同じ話題回避・０番は使用しない
            if (MassageList.UseMassage[index] != 0 && TalkTitleNum != MassageList.UseMassage[index]) {
                TalkTitleNum = MassageList.UseMassage[index];
                break;
            }
        }
        TalkTitle = MassageList.Massage[TalkTitleNum];
        Text.text = TalkTitle;//テキスト変更
    }

    //評価メソッド
    public void Rated(int MassageNum){
        //表示中の話題と同じ話題なら評価ＵＰ
        if (MassageNum == TalkTitleNum){
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
