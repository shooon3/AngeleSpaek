using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour {

    public float Speed;//移動量
    public Vector3 StartPos;//始点
    public float EndXPos;//終点
    public float Interval;
    private float Timer = 0;

    void Update(){
        if (transform.position.x > EndXPos){
            Wait();
        }
        else {
            transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    private void Wait(){
        transform.position = (Timer >= Interval) ? StartPos : transform.position;
        Timer = (Timer >= Interval) ? 0 : Timer + Time.deltaTime;
    }
}
