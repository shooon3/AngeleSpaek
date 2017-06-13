using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private bool ShotF;//角度変更中か否か判定フラグ

    //各種スクリプト
    public Player Player;
    public Enemy Enemy;


    void Update () {

        //指を離すとセリフ発射
        if (Input.GetButtonUp("Fire1")){
            ShotF = false;

            //フリーズ解除
            MassageStatus.IsFreeze = false;
            Massage.IsFreeze = false;

            Player.Shot();
        }

        //ロングタップ開始時角度変更を止める
        if (Input.GetButtonDown("Fire1")){
            ShotF = true;

            //フリーズ
            MassageStatus.IsFreeze = true;
            Massage.IsFreeze = true;

            Player.MassageSet();
        }

        if (ShotF){
            Player.MassageChange();
        }
        else{
            Player.DegChanger();
            Enemy.IsShot();
        }

      

    }

}
