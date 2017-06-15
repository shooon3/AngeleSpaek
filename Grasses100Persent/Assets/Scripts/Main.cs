using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private bool ShotF;//角度変更中か否か判定フラグ

    //各種スクリプト
    public Player Player;
    public Enemy Enemy;
    public Girl Girl;


    void Update () {

        //指を離すとセリフ発射
        if (Input.GetButtonUp("Fire1")){
            ShotF = false;

            //フリーズ解除
            PlayerMassage.IsFreeze = false;
            EnemyMassage.IsFreeze = false;

            Player.Shot();
        }

        //ロングタップ開始時角度変更を止める
        if (Input.GetButtonDown("Fire1")){
            ShotF = true;

            //フリーズ
            PlayerMassage.IsFreeze = true;
            EnemyMassage.IsFreeze = true;

            Player.MassageSet();
        }

        if (ShotF){
            Player.MassageChange();
        }
        else{
            Player.DegChanger();
            Enemy.IsShot();
            Girl.TalkTitleTimer();
        }

      

    }

}
