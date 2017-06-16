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
        public AudioClip[] Voice_RateDown;//評価ダウン
        public AudioClip[] Voice_RateUp;//評価アップ
        public AudioClip[] Voice_TTChange;//話題変更

    private AudioSource AS;

   

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
        index = Random.Range(0, Voice_TTChange.Length);//音声をランダムに決定
        AS.clip = Voice_TTChange[index];
        AS.Play();//音声再生
    }

    //評価メソッド
    public void Rated(int MassageNum){
        //表示中の話題と同じ話題なら評価ＵＰ
        if (MassageNum == TalkTitleNum){
            NowRated++;
            if(NowRated > MaxRated){
                NowRated = MaxRated;
            }

            int index = Random.Range(0, Voice_RateUp.Length);//音声をランダムに決定
            AS.clip = Voice_RateUp[index];
            //UpVoice1.PlayOneShot(UpVoice1.clip);
        }
        //違う話題なら評価ＤＯＷＮ
        else{
            NowRated--;
            if(NowRated < MinRated){
                NowRated = MinRated;
            }
            int index = Random.Range(0, Voice_RateDown.Length);//音声をランダムに決定
            AS.clip = Voice_RateDown[index];
            //DownVoice1.PlayOneShot(DownVoice1.clip);
        }
        AS.Play();//音声再生
    }

    private void Awake(){
        AS = GetComponent<AudioSource>();//AudioSource取得

        TalkTitleChange();
    }

  
  }
