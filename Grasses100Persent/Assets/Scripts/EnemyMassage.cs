using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMassage : MonoBehaviour {
    //速度一覧
    private class Speed{
        public const float Slow  = 0.05f;
        public const float Nomal = 0.1f;
        public const float Fast  = 0.2f;
    }

    //吹き出しの状態
    public enum Janle {Smole,Normal,Big };
    private Janle NowJanle;

    //リスト
    private static List<EnemyMassage> List = new List<EnemyMassage>();
    private static List<Vector2> IMSpeed = new List<Vector2>();

    //吹き出しSprite
    public Sprite SmoleMassage = new Sprite();
    public Sprite NormalMassage = new Sprite();
    public Sprite BigMassage = new Sprite();
    public Sprite[] MassageSprites;//０：小さい　１：普通　２：大きい

    //ステータス・コンポーネント
    public int SetMassageNum;//セリフ内容
    Vector2 GirlPos;//ターゲット
    SpriteRenderer SR;
    public Rigidbody2D RB;
    private TextMesh Text;

    //紐づけスクリプト
    Girl Girl;

    public GameObject CollisionSound;//接触時ＳＥ

    //表示位置調整用変数
    private static float Z_PosNum = -1;
    private static float zPosRank = 10.0f;//分割数

    public static bool IsFreeze{
        set{

            for (int i = 0; i < List.Count; i++){
                if (value){//一時停止
                    IMSpeed.Add(List[i].RB.velocity);//速度保存
                    List[i].RB.velocity = Vector2.zero;
                }
                else{//再生
                    if (IMSpeed.Count >= 1){//エラー回避
                        List[i].RB.velocity = IMSpeed[i];//速度登録
                    }
                }
            }

            //リストリセット
            if (!value){
                IMSpeed.Clear();
            }

        }
    }//一時停止管理変数

    public static void ZAjaster(){
        for (int i = 0; i < List.Count; i++){
            //リストの番号に合わせてｚ座標を調整
            Vector3 Pos = List[i].transform.position;
            Pos.z = i / zPosRank * Z_PosNum + Z_PosNum;
            List[i].transform.position = Pos;
        }
    }//表示位置調整メソッド

    private void OnTriggerEnter2D(Collider2D collision){
        //衝突物によって処理変更
        switch (collision.tag){
            case "Massage"://衝突物：吹き出し
                //スクリプト取得
                PlayerMassage PM = collision.GetComponent<PlayerMassage>();
                PM.Destroyer();

                //Janleによって処理変更
                MassageAction(PM.ThisJanle);
                break;

            case "Girl"://衝突物：少女
                //自身を破壊
                Destroyer();
                
                //評価
                Girl.Rated(SetMassageNum, NowJanle);
                break;

            default:
                break;
        }
    }//衝突時処理

    private void MassageAction(PlayerMassage.Janle PMJ){
        GameObject CollisionSoundObj;//ＳＥ

        //接触したプレイヤーのセリフの種類で処理切替
        switch (PMJ){
            case PlayerMassage.Janle.Break://接触：こわれろー
                //破壊ＳＥ再生
                CollisionSoundObj = (GameObject)Instantiate(CollisionSound);
                CollisionSoundObj.GetComponent<CollisionSound>().SetSE(global::CollisionSound.CollisionIs.Destroy);

                //自身を破壊
                Destroyer();
                break;

            case PlayerMassage.Janle.Bigger://接触：おおきくなれー
                //最大でなければ大きくする
                if (NowJanle != Janle.Big){
                    NowJanle++;

                    //状態変化ＳＥ再生
                    CollisionSoundObj = (GameObject)Instantiate(CollisionSound);
                    CollisionSoundObj.GetComponent<CollisionSound>().SetSE(global::CollisionSound.CollisionIs.Change);
                }
                break;

            case PlayerMassage.Janle.Smoler://接触：ちいさくなれ―
                //最小でなければ小さくする
                if (NowJanle != Janle.Smole){
                    NowJanle--;

                    //状態変化ＳＥ再生
                    CollisionSoundObj = (GameObject)Instantiate(CollisionSound);
                    CollisionSoundObj.GetComponent<CollisionSound>().SetSE(global::CollisionSound.CollisionIs.Change);
                }
                break;

            default:
                break;
        }
        MassgeChanger();
    }//セリフ接触時アクション

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

        SR.sprite = ChangeSprite;//吹き出し更新
        RB.velocity = GirlPos * ChangeSpeed;//スピード更新
    }//状態変化管理メソッド

    public static void GameEnd(){

        //リスト登録済み吹き出しすべて削除
        var AnotherList = List.ToArray();//配列に変換
        foreach (var EM in AnotherList) {
            EM.Destroyer();
        }

    }//ゲーム終了処理呼び出しメソッド

    private void Destroyer(){
        //自身を削除
        List.Remove(this);
        ZAjaster();
        Destroy(this.gameObject);
    }//ゲーム終了処理

    private void Awake(){

        //ステータス・コンポーネント取得
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        Text = transform.GetChild(0).GetComponent<TextMesh>();
        GameObject GirlObj = GameObject.FindWithTag("Girl");
        Girl = GirlObj.GetComponent<Girl>();
        GirlPos = GirlObj.transform.position - transform.position;

        List.Add(this);//リストに追加
        ZAjaster();

        //吹き出し初期化
        NowJanle = (Janle)Random.Range(0,3);//声量をランダムに決定
        while (true){
            int index = Random.Range(0, MassageList.UseMassage.Length);//セリフをランダムに決定
            if (MassageList.UseMassage[index] != 0){//０は使用しない
                SetMassageNum = MassageList.UseMassage[index];//テキストを番号で取得
                break;
            }
        }
        Text.text = MassageList.Massage[SetMassageNum];//セリフを更新
        MassgeChanger();//テキスト更新

    }

}

