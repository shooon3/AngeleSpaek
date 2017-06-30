using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    //言葉発射間隔計測用タイマー
    private float Timer;//経過時間
    private float Interval;//発射間隔
    //public float AniInterval;//アニメーションのずれ調整用変数

    //public float ShotPosYAjaster;//生成位置調整用変数

    ////アニメーション管理
    //public string AniFName;//アニメーション遷移管理変数名
    //private float AniSpeed;//アニメーション速度記憶変数
    //private Animator animator;

    public GameObject ENMassagePre;//吹き出しプレファブ

    //public bool AniStop{
    //    set{
    //        if (animator != null){//エラー回避
    //            if (value){//停止
    //                AniSpeed = animator.speed;//speed保存
    //                animator.speed = 0;//停止
    //            }
    //            else {//再生
    //                try{
    //                    animator.speed = AniSpeed;
    //                }
    //                catch{//エラー回避
    //                    animator.speed = 1;
    //                }
    //            }
    //        }
    //    }
    //}//アニメーション管理変数

    public void IsShot(){
        Timer += Time.deltaTime;//経過時間計測

        //口パク開始
        //if (Timer >= Interval - AniInterval){
        //    if (animator != null){//エラー回避
        //        animator.SetBool(AniFName, true);
        //    }
        //}

        //一定時間経過でセリフ発射
        if (Timer >= Interval){
            Shot();
            Timer = 0.0f;//タイマーリセット
        }
    }//セリフ発射間隔管理メソッド

    private void Shot(){
        //ENMassage = Instantiate(ENMassagePre, transform.position, Quaternion.identity) as GameObject;//セリフを生成
        Vector3 ShotPos = transform.position;
        //ShotPos.y += ShotPosYAjaster;//生成位置調整
        Instantiate(ENMassagePre, ShotPos, Quaternion.identity);

        //if (animator != null){
        //    animator.SetBool(AniFName, false);//アニメーション停止
        //}

    }//セリフ生成メソッド

    private void Awake(){
        //コンポーネント等取得
        //animator = GetComponent<Animator>();
        Interval = MassageList.ShotInterval;//難易度設定
    }

}
