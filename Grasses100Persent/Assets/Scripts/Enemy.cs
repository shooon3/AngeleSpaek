using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    private float Timer;
    public float Interval;

    public GameObject ENMassagePre;//エネミーセリフプレファブ
    //private GameObject ENMassage;//エネミーセリフ

    private Animator Animator;

    public void IsShot(){
        Timer += Time.deltaTime;//経過時間計測

        //一定時間経過でセリフ発射
        if (Timer >= Interval){
            Shot();
            Timer = 0.0f;//タイマーリセット
        }
    }
    private void Shot(){
        //ENMassage = Instantiate(ENMassagePre, transform.position, Quaternion.identity) as GameObject;//セリフを生成
        Instantiate(ENMassagePre, transform.position, Quaternion.identity);
        Animator.SetInteger("Motion", 1);//アニメーション開始
    }

    private void Awake(){
        Animator = GetComponent<Animator>();
        Animator.SetInteger("Motion", 0);
    }

}
