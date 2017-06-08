using System.Collections;
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
        public const float Nomal = 0.0f;
        public const float Fast  = 0.0f;
    }

    //コライダー大きさ一覧
    private class ColliderSize{
        public const float Smole = 0.0f;
        public const float Nomal = 0.0f;
        public const float Big = 0.0f;
    }

    //セリフ一覧
    public string[] TalkMassage = new string[] {
        "遊園地",
        "動物園",
        "",
    };

    //スプライト
    public Sprite[] MassageSprite = new Sprite[3];

    //ステータス
    Vector3 GirlPos;
    SpriteRenderer SR;
    Rigidbody2D RB;

    private void Start(){
        RB = this.gameObject.GetComponent<Rigidbody2D>();//RigidBody取得
        SR = this.gameObject.GetComponent<SpriteRenderer>();//SpriteRenderer取得

        //ステータス初期化
        //SR.sprite = //サイズ：普通
        GirlPos = GameObject.FindWithTag("Girl").transform.position - transform.position;//Girlの方向を取得
        RB.velocity = GirlPos * Speed.Nomal;//取得した方向をVelocityへ加算
    }

    private void OnTriggerEnter2D(Collider2D collision){
        switch (collision.tag){
            case TagName.Massage:
                //メッセージにセリフの種類を判定するステータスを設定。
                //ステータスを引数に渡してMassageActionを呼び出す
                //MassageAction();
                break;
            case TagName.Girl:
                Destroy(this.gameObject);
                //評価を変更したりするメソッド呼び出し
                break;
            default:
                break;
        }
    }

    //セリフ接触時アクション
    private void MassageAction(string MassageKind){

        Sprite ChangeSprite = new Sprite();//変更先Sprite
        float ChangeSpeed = new float();//変更先Sprite

        switch (MassageKind){
            case "Break":
                Destroy(this.gameObject);
                break;
            case "Bigger":
                //変更するコライダーの大きさ決定
                //変更するスプライト決定
                ChangeSpeed = Speed.Fast;//変更するスピード決定
                break;
            case "Smoler":
                //変更するコライダーの大きさ決定
                //変更するスプライト決定
                ChangeSpeed = Speed.Slow;
                break;
            default:
                break;
        }
        SR.sprite = ChangeSprite;//スプライト変更
        RB.velocity = GirlPos * ChangeSpeed;//スピード変更
    }
}

