using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //メッセージ変更用変数
    private float ShotPow;//セリフ変更判定用変数
    public float AddPow;//判定用変数変化量
    private const float MaxPow = 135;//範囲最大
    private const float SmolerPow = 45;//判定：ちいさくなーれ
    private const float BiggerPow = 90;//判定：おおきくなーれ
    private const float InstanceposAdjuster = 2;//配置位置調整

    //発射角変更用変数
    private bool IsAddDeg = true;//角度変更判定フラグ
    private float ShotDeg;//発射角
    public float AddDeg;//角度変化量
    public float MaxDeg;//角度最大値
    public float MinDeg;//角度最小値
    
    public float ShotSpeed;//セリフ速度

    public float FreezeTime;//停止時間

    //照準
    private GameObject LazerSight;
    private SpriteRenderer LazerSightSR;

    //吹き出し
    public GameObject MassagePre;//セリフプレファブ
    private GameObject MassageObj;//セリフ

    public AudioSource AS;

    //紐づけスクリプト
    private PlayerMassage PM;

    public void DegChanger(){
        //角度変更判定
        if (!IsAddDeg){
            return;
        }

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

        //発射角更新
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, ShotDeg));

    }//発射角変更メソッド

    public void MassageSet(){
        //角度変更を止める
        IsAddDeg = false;

        LazerSight.SetActive(true);//照準表示

        float ShotRad = ShotDeg * Mathf.Deg2Rad;//ラジアン変換
        Vector3 ShotVec = new Vector3(Mathf.Cos(ShotRad), Mathf.Sin(ShotRad),0);//角度計算
        MassageObj = Instantiate(MassagePre, transform.position + ShotVec * InstanceposAdjuster + new Vector3(0, 0, transform.position.z - 3), Quaternion.identity) as GameObject;//吹き出しを生成
        PM = MassageObj.GetComponent<PlayerMassage>();//吹き出しを記憶

    }//セリフ配置メソッド

    public void MassageChange(){
        //種類変更はループする
        if(ShotPow > MaxPow){
            ShotPow = 0;
        }

        ShotPow += AddPow;//パワー加算

        //ShotPowを判定
        if (ShotPow < SmolerPow){
            //判定：ちいさくなれー
            PM.ThisJanle = PlayerMassage.Janle.Smoler;
        }
        else if(ShotPow > BiggerPow){
            //判定：おおきくなれ―
            PM.ThisJanle = PlayerMassage.Janle.Bigger;
        }
        else{
            //判定：こわれろー
            PM.ThisJanle = PlayerMassage.Janle.Break;
        }

        PM.TextChange();//テキスト更新

    }//セリフ変更メソッド

    public void Shot(){
        //ShotPowリセット
        ShotPow = 0;

        //セリフ発射
        float ShotRad = ShotDeg * Mathf.Deg2Rad;//ラジアン変換
        Vector2 ShotVec = new Vector2(Mathf.Cos(ShotRad), Mathf.Sin(ShotRad));//角度計算
        MassageObj.GetComponent<Rigidbody2D>().velocity = ShotVec * ShotSpeed;//加速

        AS.Play();//発射音再生

        //停止
        Invoke("ShotFreeze",FreezeTime);

        //照準
        LazerSightSR.color = Reset();//α値調整
        LazerSight.SetActive(false);//照準非表示

    }//セリフ発射メソッド

    private void ShotFreeze(){
        IsAddDeg = true;
    }//停止解除メソッド

    private Color Reset(){
        Color ResetColor = LazerSightSR.color;
        ResetColor.a = 0;
        return ResetColor;
    }//照準α値初期化メソッド

    private void Awake(){
        //コンポーネント取得
        AS = GetComponent<AudioSource>();
        LazerSight = transform.GetChild(1).gameObject;
        LazerSightSR = LazerSight.GetComponent<SpriteRenderer>();
        LazerSightSR.color = Reset();
        LazerSight.SetActive(false);
    }

}
