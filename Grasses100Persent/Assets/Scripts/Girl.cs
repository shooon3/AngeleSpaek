using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour {

    //評価パラメータ
    public static int NowRated;//現在の評価
    public static int MaxRated = 6;//評価最大値
    public static int MinRated = 0;//評価最低値
    public static int FormatRated = 3;//評価初期値
    public int NowRatedView;

    //話題切り替えタイマー
    private float Timer;//経過時間
    private float Interval;//間隔

    //話題提供吹き出し
    private int TalkTitleNum;
    private string TalkTitle;//話題

    //花
    public GameObject Flower;
    public Sprite[] FlowerSprites;
    public Vector3 FlowerPos;

    public Sprite QuestionSprite;

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

        if(Timer >= Interval - 1){
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b,Interval - Timer );
        }

        if (Timer > Interval){
            TalkTitleChange();
            Timer = 0;
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
        }
    }

    //話題変更メソッド
    public void TalkTitleChange() { 
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
    public void Rated(int MassageNum, EnemyMassage.Janle Janle){

        //声量・話題が適切なら評価アップ
        if (MassageNum == TalkTitleNum && Janle == EnemyMassage.Janle.Normal){
            GameObject InstanceFlower = (GameObject)Instantiate(Flower, FlowerPos, Quaternion.identity);//花で評価アップを表現
            int index = Random.Range(0, FlowerSprites.Length);//花の色をランダムに決定
            InstanceFlower.GetComponent<SpriteRenderer>().sprite = FlowerSprites[index];

            NowRated++;
            if (NowRated > MaxRated){
                NowRated = MaxRated;
            }

            //音声をランダムに決定
            index = Random.Range(0, Voice_RateUp.Length);
            AS.clip = Voice_RateUp[index];
        }
        //適切でないので評価ダウン
        else{
            GameObject InstanceFlower = (GameObject)Instantiate(Flower, FlowerPos, Quaternion.identity);//花で評価アップを表現
            InstanceFlower.GetComponent<SpriteRenderer>().sprite = QuestionSprite;

            NowRated--;
            if (NowRated < MinRated){
                NowRated = MinRated;
            }
            
            //音声をランダムに決定
            int index = Random.Range(0, Voice_RateDown.Length);
            AS.clip = Voice_RateDown[index];
        }

        AS.Play();//音声再生
    }

    private void Awake(){
        AS = GetComponent<AudioSource>();//AudioSource取得
        Text = GameObject.Find("TalkTitle").GetComponent<TextMesh>();

        //各種数値リセット
        Interval = MassageList.ChangeInterval;
        NowRated = FormatRated;
        Timer = 0; 
        //TalkTitleChange();
    }

  }
