using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //メッセージ変更用変数
    private float ShotPow;//セリフ変更判定用変数
    public float AddPow;//判定用変数変化量
    private float MaxPow = 100;//範囲最大
    private float MinPow = 0;//範囲最小
    public float SmolerPow;//判定：ちいさくなーれ
    public float BiggerPow;//判定：おおきくなーれ

    private string[] MassageText = { "ちいさくなーれ", "こわれろー", "おおきくなーれ" };
    private string ShotMassageText;//発射するメッセージ

    //発射角
    private float ShotDeg;//発射角
    public float AddDeg;//角度変化量
    public float MaxDeg;//角度最大値
    public float MinDeg;//角度最小値

    //セリフ速度
    public float ShotSpeed;

    //ゲームオブジェクト
    public GameObject MassagePre;//セリフプレファブ
    private GameObject Massage;//セリフ

    private PlayerMassage PM;//セリフステータス

    public Sprite SmollerMassage;//ちいさくなーれ
    public Sprite BreakMassge;//こわれろー
    public Sprite BiggerMassage;//おおきくなーれ

    //発射角変更メソッド
    public void DegChanger(){
        //発射角が範囲を超えた場合加算量の符号を反転
        if(ShotDeg > MaxDeg){
            ShotDeg = MaxDeg;
            AddDeg = -AddDeg;
        }
        else if(ShotDeg < MinDeg){
            ShotDeg = MinDeg;
            AddDeg = -AddDeg;
        }

        //発射角に変化量を加算
        ShotDeg += AddDeg;

        //キャラクターの角度を変更
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, ShotDeg));

    }

    //セリフ配置メソッド
    public void MassageSet(){
        Massage = Instantiate(MassagePre,transform.position,Quaternion.identity) as GameObject;//セリフを生成
        PM = Massage.GetComponent<PlayerMassage>();
        Debug.Log("セリフを配置しました。");
    }

    //セリフ変更メソッド
    public void MassageChange(){
        //範囲外
        if(ShotPow < MinPow||ShotPow > MaxPow){
            AddPow = -AddPow;
        }

        ShotPow += AddPow;//パワー加算

        //ShotPowの値でメッセージ変更
        if(ShotPow < SmolerPow){//基準値以下
            PM.Janle = PlayerMassage.PMJanle.Smoler;
        }
        else if(ShotPow > BiggerPow){//基準値以上
            PM.Janle = PlayerMassage.PMJanle.Bigger;
        }
        else{
            PM.Janle = PlayerMassage.PMJanle.Berak;
        }

        Massage.GetComponent<PlayerMassage>().Changer();
    }

    //セリフ発射メソッド
    public void Shot(){
        //ShotPowリセット
        ShotPow = 0;
        if(AddPow < 0){
            AddPow = -AddPow;
        }

        //インスタンス化されたセリフにAddForceする。
        float ShotRad = ShotDeg * Mathf.Deg2Rad;//ラジアン変換
        Vector2 ShotVec = new Vector2(Mathf.Cos(ShotRad), Mathf.Sin(ShotRad));//角度計算
        Massage.GetComponent<Rigidbody2D>().velocity = ShotVec * ShotSpeed;
        PlayerMassage.InstanceMassage.Add(Massage.GetComponent<PlayerMassage>());
        //発射後、弾がずれるところは止まることで解決
    }

}
