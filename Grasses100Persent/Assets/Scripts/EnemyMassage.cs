using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMassage : MonoBehaviour {
    ////タグ一覧
    //private class TagName{
    //    public const string Massage   = "Massage";
    //    public const string Girl      = "Girl";
    //}

    //速度一覧
    private class Speed{
        public const float Slow  = 0.05f;
        public const float Nomal = 0.1f;
        public const float Fast  = 0.2f;
    }

    //種類
    private enum Janle {Smole,Normal,Big };
    private Janle NowJanle;

    //リスト
    private static List<EnemyMassage> InstanceMassage = new List<EnemyMassage>();
    private static List<Vector2> IMSpeed = new List<Vector2>();

    //吹き出し
    public Sprite SmoleMassage = new Sprite();
    public Sprite NormalMassage = new Sprite();
    public Sprite BigMassage = new Sprite();

    //ステータス
    public int SetMassageNum;//セリフ
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
    private void OnTriggerEnter2D(Collider2D collision){
        //衝突物によって処理変更
        switch (collision.tag){
            case "Massage"://衝突物：吹き出し
                //衝突したセリフを破壊
                PlayerMassage.List.Remove(collision.GetComponent<PlayerMassage>());
                Destroy(collision.gameObject);

                //Janleによって処理変更
                MassageAction(collision.GetComponent<PlayerMassage>().ThisJanle);
                break;
            case "Girl":
                InstanceMassage.Remove(this);
                Destroy(this.gameObject);
                Girl.Rated(SetMassageNum);
                break;
            default:
                break;
        }
    }

    //セリフ接触時アクション
    private void MassageAction(PlayerMassage.Janle PMJ){
        switch (PMJ){
            case PlayerMassage.Janle.Break://接触：こわれろー
                InstanceMassage.Remove(this);//リスト解除
                Destroy(this.gameObject);
                break;
            case PlayerMassage.Janle.Bigger://接触：おおきくなれー
                //最大でなければ大きくする
                if (NowJanle != Janle.Big){
                    NowJanle++;
                }
                break;
            case PlayerMassage.Janle.Smoler://接触：ちいさくなれ―
                //最小でなければ小さくする
                if (NowJanle != Janle.Smole){
                    NowJanle--;
                }
                break;
            default:
                break;
        }
        MassgeChanger();
    }

    private void MassgeChanger(){
        Sprite ChangeSprite = new Sprite();//変更先Sprite
        float ChangeSpeed = new float();//変更先速度

        //声量判定
        switch (NowJanle){
            //声量によってスプライトを変更する
            case Janle.Smole://声量：小
                ChangeSprite = SmoleMassage;
                ChangeSpeed = Speed.Slow;
                break;
            case Janle.Normal://声量：中
                ChangeSprite = NormalMassage;
                ChangeSpeed = Speed.Nomal;
                break;
            case Janle.Big://声量：大
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
        //ステータス取得
        RB = GetComponent<Rigidbody2D>();//RigidBody取得
        SR = GetComponent<SpriteRenderer>();//SpriteRenderer取得
        Text = transform.GetChild(0).GetComponent<TextMesh>();//TextMesh取得
        GameObject GirlObj = GameObject.FindWithTag("Girl");
        Girl = GirlObj.GetComponent<Girl>();//Girlスクリプト取得
        GirlPos = GirlObj.transform.position - transform.position;//Girlの方向を取得

        InstanceMassage.Add(this);//リストに追加

        //吹き出し初期化
        NowJanle = (Janle)Random.Range(0,3);//声量をランダムに決定
        //セリフをランダムに決定
        while (true){
            int index = Random.Range(0, MassageList.UseMassage.Length);
            if(MassageList.UseMassage[index] != 0){//０は使用しない
                SetMassageNum = MassageList.UseMassage[index];//テキストを番号で取得
                break;
            }
        }
        Text.text = MassageList.Massage[SetMassageNum];//セリフを更新
        MassgeChanger();

    }

}

