using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private bool ShotF;//角度変更中か否か判定フラグ

    //各種スクリプト
    public Player Player;
  

	void Update () {

        //指を離すとセリフ発射
        if (Input.GetButtonUp("Fire1")){
            ShotF = false;
            Player.Shot();
        }

        //ロングタップ開始時角度変更を止める
        if (Input.GetButtonDown("Fire1")){
            ShotF = true;
            Player.MassageSet();
        }

        if (ShotF){
            Player.MassageChange();
        }
        else{
            Player.DegChanger();
        }

      

    }

}
