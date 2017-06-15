using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMassage : MonoBehaviour {
    //タグ一覧
    private class TagName{
        public const string Massage   = "Massage";
        public const string Girl      = "Girl";
    }

    //速度一覧
    private class Speed{
        public const float Slow  = 0.05f;
        public const float Nomal = 0.1f;
        public const float Fast  = 0.2f;
    }

    //大きさ
    private enum Size {Smole,Normal,Big };
    private Size NowSize;

    //リスト
    private static List<EnemyMassage> InstanceMassage = new List<EnemyMassage>();
    private static List<Vector2> IMSpeed = new List<Vector2>();

    //吹き出し
    public Sprite SmoleMassage = new Sprite();
    public Sprite NormalMassage = new Sprite();
    public Sprite BigMassage = new Sprite();

    //ステータス
    public int MassageNum;//セリフ
    Vector2 GirlPos;//移動先
    SpriteRenderer SR;//スプライト
    public Rigidbody2D RB;//リジッドボディ
    private TextMesh Text;//テキスト

    //紐づけスクリプト
    Girl Girl;

    //停止メソッド
    public static bool IsFreeze{
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
        }
    }

    //衝突時処理
    //private void OnTriggerEnter2D(Collider2D collision){
    //    //衝突物によって処理変更
    //    switch (collision.tag){
    //        case TagName.Massage:
    //            //衝突したセリフを破壊
    //            PlayerMassage.List.Remove(collision.GetComponent<PlayerMassage>());
    //            Destroy(collision.gameObject);

    //            //Janleによって処理変更
    //            MassageAction(collision.GetComponent<PlayerMassage>().Janle);
    //            break;
    //        case TagName.Girl:
    //            InstanceMassage.Remove(this);
    //            Destroy(this.gameObject);
    //            Girl.Rated(MassageNum);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //セリフ接触時アクション
    //private void MassageAction(PlayerMassage.PMJanle MS){
    //    switch (MS){
    //        case PlayerMassage.PMJanle.Berak:
    //            InstanceMassage.Remove(this);//リスト解除
    //            Destroy(this.gameObject);
    //            break;
    //        case PlayerMassage.PMJanle.Bigger:
    //            //最大でなければ大きくする
    //            if (NowSize != Size.Big) {
    //                NowSize++;
    //            }
    //            break;
    //        case PlayerMassage.PMJanle.Smoler:
    //            //最小でなければ小さくする
    //            if (NowSize != Size.Smole){
    //                NowSize--;
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //    MassgeChanger();
    //}

    private void MassgeChanger(){
        Sprite ChangeSprite = new Sprite();//変更先Sprite
        float ChangeSpeed = new float();//変更先速度

        //声量判定
        switch (NowSize){
            case Size.Smole:
                ChangeSprite = SmoleMassage;
                ChangeSpeed = Speed.Slow;
                break;
            case Size.Normal:
                ChangeSprite = NormalMassage;
                ChangeSpeed = Speed.Nomal;
                break;
            case Size.Big:
                ChangeSprite = BigMassage;
                ChangeSpeed = Speed.Fast;
                break;
            default:
                break;
        }

        //SR.sprite = ChangeSprite;//吹き出し変更
        RB.velocity = GirlPos * ChangeSpeed;//スピード変更
    }

    private void Awake(){
        RB = this.gameObject.GetComponent<Rigidbody2D>();//RigidBody取得
        SR = this.gameObject.GetComponent<SpriteRenderer>();//SpriteRenderer取得
        Text = this.gameObject.transform.GetChild(0).GetComponent<TextMesh>();

        //リストに追加
        InstanceMassage.Add(this);

        //移動開始
        GirlPos = GameObject.FindWithTag("Girl").transform.position - transform.position;//Girlの方向を取得
        RB.velocity = GirlPos * Speed.Nomal;//取得した方向をVelocityへ加算

        //ステータス初期化
        NowSize = (Size)Random.Range(0,3);//声量をランダムに決定
        while (true){
            int index = Random.Range(0, MassageList.UseMassage.Length);
            if(MassageList.UseMassage[MassageNum] != 0){
                MassageNum = MassageList.UseMassage[index];
                break;
            }
        }
        Text.text = MassageList.Massage[MassageNum];//セリフをランダムに決定
        MassgeChanger();

    }

}

