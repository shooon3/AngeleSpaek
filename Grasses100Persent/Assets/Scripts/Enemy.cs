using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public string StateFName;

    private float Timer;
    public float Interval;
    public float AniInterval;

    public GameObject ENMassagePre;//エネミーセリフプレファブ
    //private GameObject ENMassage;//エネミーセリフ

    private Animator Animator;

    public void IsShot(){
        Timer += Time.deltaTime;//経過時間計測

        if (Timer >= AniInterval && !Animator.GetBool(StateFName)){
            AniChanger();
        }

        //一定時間経過でセリフ発射
        if (Timer >= Interval){
            Shot();
            Timer = 0.0f;//タイマーリセット
        }
    }
    private void Shot(){
        //ENMassage = Instantiate(ENMassagePre, transform.position, Quaternion.identity) as GameObject;//セリフを生成
        Instantiate(ENMassagePre, transform.position, Quaternion.identity);
        AniChanger();
    }

    private void AniChanger(){
        Animator.SetBool(StateFName, (Animator.GetBool(StateFName)) ? false : true);
    }

    private void AniChanger(bool x){
        Animator.SetBool("Motion", x);
    }

    private void Awake(){
        Animator = GetComponent<Animator>();
        AniChanger(false);
    }

}
