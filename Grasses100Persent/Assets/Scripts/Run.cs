using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour {

    public float Interval;//停止時間
    private float Timer = 0;//時間計測

    public float Speed;//移動量
    public Vector3 StartPos;//始点
    public float EndXPos;//終点

    void Update(){
        if (transform.position.x > EndXPos){
            Wait();//画面外で停止
        }
        else {
            //移動
            Vector3 NewPos = transform.position;
            NewPos.x += Speed * Time.deltaTime;
            transform.position = NewPos;
        }
    }

    private void Wait(){
        transform.position = (Timer >= Interval) ? StartPos : transform.position;//一定時間経過でposition変更
        Timer = (Timer >= Interval) ? 0 : Timer + Time.deltaTime;//一定時間経過まで時間計測
    }//一時停止メソッド

}
