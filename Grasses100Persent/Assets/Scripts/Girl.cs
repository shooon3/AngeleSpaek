using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour {

    //評価パラメータ
    public static int NowRated;//現在の評価
    public static int MaxRated;//評価最大値
    public static int MinRated = 0;//評価最低値

    //話題切り替えタイマー
    private float Timer;//経過時間
    private float Interval;//間隔
    public float FadeTime;//透過時間

    //話題提供吹き出し
    private int TalkTitleNum;//話題単語番号
    private string TalkTitle;//話題
    public TextMesh Text;//テキスト

    //評価アイコン
    public GameObject RatedIcon;
    public Vector3 RatedIconPos;

    //ボイス集
    public AudioClip[] Voice_RateDown;//評価ダウン
    public AudioClip[] Voice_RateUp;//評価アップ
    public AudioClip[] Voice_TTChange;//話題変更

    //コンポーネント
    private AudioSource AS;

    public void TalkTitleTimer(){
        Timer += Time.deltaTime;//時間計測

        if(Timer >= Interval - FadeTime){//一定時間前から透明化
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b,Interval - Timer );
        }

        if (Timer > Interval){//話題変更
            TalkTitleChange();
            Timer = 0;//タイマーリセット
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1);//透過リセット
        }
    }//話題変更タイマー

    public void TalkTitleChange() { 
        int index = new int();

        while (true){
            index = Random.Range(0, MassageList.UseMassage.Length);
            //同じ話題回避・０番は使用しない
            if (MassageList.UseMassage[index] != 0 && TalkTitleNum != MassageList.UseMassage[index]) {
                TalkTitleNum = MassageList.UseMassage[index];
                break;
            }
        }

        TalkTitle = MassageList.Massage[TalkTitleNum];//話題決定
        Text.text = TalkTitle;//テキスト変更
        //音声をランダムに決定
        index = Random.Range(0, Voice_TTChange.Length);
        AS.clip = Voice_TTChange[index];
        AS.Play();//音声再生
    }//話題変更メソッド

    public void Rated(int MassageNum, EnemyMassage.Janle Janle){

        GameObject RatedIconObj = (GameObject)Instantiate(RatedIcon, RatedIconPos, Quaternion.identity);//評価アイコン生成
        RatedIcon RI = RatedIconObj.GetComponent<RatedIcon>();//Script取得

        //声量・話題が適切なら評価アップ
        if (MassageNum == TalkTitleNum && Janle == EnemyMassage.Janle.Normal){

            NowRated++;

            RI.NowRated = global::RatedIcon.Rated.Up;//Sprite決定

            //音声をランダムに決定
            int index = Random.Range(0, Voice_RateUp.Length);
            AS.clip = Voice_RateUp[index];
        }
        //適切でないので評価ダウン
        else{
            NowRated--;

            RI.NowRated = global::RatedIcon.Rated.Down;

            //音声をランダムに決定
            int index = Random.Range(0, Voice_RateDown.Length);
            AS.clip = Voice_RateDown[index];
        }

        AS.Play();//音声再生
    }//評価メソッド

    private void Awake(){
        //コンポーネント取得
        AS = GetComponent<AudioSource>();
        Text = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMesh>();

        //各種数値リセット
        Interval = MassageList.ChangeInterval;
        MaxRated = MassageList.MaxRated;
        NowRated = MaxRated / 2;//初期値は最大値の半分
        Timer = 0; 
    }

  }
