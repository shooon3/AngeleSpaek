using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    private float Timer;
    private float Interval;

    public GameObject ENMassagePre;//エネミーセリフプレファブ
    //private GameObject ENMassage;//エネミーセリフ

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
    }

    private void Awake(){
        Interval = MassageList.ShotInterval;
    }

}
