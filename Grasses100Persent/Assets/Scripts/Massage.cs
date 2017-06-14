﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massage : MonoBehaviour {
    //エネミーの発射するセリフ

    //タグ一覧
    private class TagName{
        public const string Massage   = "Massage";
        public const string Girl      = "Girl";
    }

    //速度一覧
    private class Speed{
        public const float Slow  = 0.0f;
        public const float Nomal = 0.1f;
        public const float Fast  = 0.0f;
    }

    //コライダー大きさ一覧
    private class ColliderSize{
        public const float Smole = 0.0f;
        public const float Nomal = 0.0f;
        public const float Big = 0.0f;
    }

    private static List<Massage> InstanceMassage = new List<Massage>();
    private static List<Vector2> IMSpeed = new List<Vector2>();

    //セリフ一覧
    public string[] TalkMassage = new string[] {
        "遊園地",
        "動物園",
        "",
    };

    //停止用フラグ
    public static bool IsFreeze {
        set{
            for (int i = 0; i < InstanceMassage.Count; i++){
                if (value){
                    IMSpeed.Add(InstanceMassage[i].RB.velocity);//速度保存
                    InstanceMassage[i].RB.velocity = Vector2.zero;//停止
                }
                else{
                    InstanceMassage[i].RB.velocity = IMSpeed[i];//速度登録
                }
            }
            
            //リストリセット
            if (!value){
                IMSpeed.Clear();
            }
            //IsFreeze = value;//フリーズ判定
        }
    }

    //スプライト
    public Sprite[] MassageSprite = new Sprite[3];

    //ステータス
    Vector2 GirlPos;
    SpriteRenderer SR;
    public Rigidbody2D RB;

    private void Start(){
        RB = this.gameObject.GetComponent<Rigidbody2D>();//RigidBody取得
        SR = this.gameObject.GetComponent<SpriteRenderer>();//SpriteRenderer取得

        //リストに追加
        InstanceMassage.Add(this);

        //ステータス初期化
        //SR.sprite = //サイズ：普通
        GirlPos = GameObject.FindWithTag("Girl").transform.position - transform.position;//Girlの方向を取得
        RB.velocity = GirlPos * Speed.Nomal;//取得した方向をVelocityへ加算
    }

    private void OnTriggerEnter2D(Collider2D collision){

        switch (collision.tag){
            case TagName.Massage:
                //衝突したセリフを破壊
                MassageStatus.InstanceMassage.Remove(collision.GetComponent<MassageStatus>());
                Destroy(collision.gameObject);

                //Janleによって処理変更
                MassageAction(collision.GetComponent<MassageStatus>().Janle);
                break;
            case TagName.Girl:
                InstanceMassage.Remove(this);
                Destroy(this.gameObject);
                //評価を変更したりするメソッド呼び出し
                break;
            default:
                break;
        }
    }

    //セリフ接触時アクション
    private void MassageAction(MassageStatus.PMJanle MS){

        Sprite ChangeSprite = new Sprite();//変更先Sprite
        float ChangeSpeed = new float();//変更先Sprite

        switch (MS){
            case MassageStatus.PMJanle.Berak:
                InstanceMassage.Remove(this);//リスト解除
                Destroy(this.gameObject);
                break;
            case MassageStatus.PMJanle.Bigger:
                //変更するコライダーの大きさ決定
                //変更するスプライト決定
                ChangeSpeed = Speed.Fast;//変更するスピード決定
                break;
            case MassageStatus.PMJanle.Smoler:
                //変更するコライダーの大きさ決定
                //変更するスプライト決定
                ChangeSpeed = Speed.Slow;
                break;
            default:
                break;
        }
        //SR.sprite = ChangeSprite;//スプライト変更
        RB.velocity = GirlPos * ChangeSpeed;//スピード変更
    }
}

