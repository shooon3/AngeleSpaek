using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public string StateFName;

    private float Timer;
    private float Interval;
    public float AniInterval;
    public float ShotPosYAjaster;

    public string AniFName;

    private Animator animator;

    public GameObject ENMassagePre;//エネミーセリフプレファブ
    //private GameObject ENMassage;//エネミーセリフ

    public void IsShot(){
        Timer += Time.deltaTime;//経過時間計測

        if(Timer >= Interval - AniInterval){
            animator.SetBool(AniFName,true);
        }
        //一定時間経過でセリフ発射
        if (Timer >= Interval){
            Shot();
            Timer = 0.0f;//タイマーリセット
        }
    }
    private void Shot(){
        //ENMassage = Instantiate(ENMassagePre, transform.position, Quaternion.identity) as GameObject;//セリフを生成
        Vector3 ShotPos = transform.position;
        ShotPos.y += ShotPosYAjaster;
        Instantiate(ENMassagePre, ShotPos, Quaternion.identity);
        animator.SetBool(AniFName, false);
    }

    private void Awake(){
        animator = GetComponent<Animator>();
        Interval = MassageList.ShotInterval;
    }

}
