using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour {

    public float AddRoll;//変化量
    private float NowRoll;
	
    void Awake(){
        NowRoll = AddRoll;
    }
    
	void Update () {
        //回転
        Vector3 NewRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        NewRotation.y += NowRoll;
        transform.rotation = Quaternion.Euler(NewRotation);

        //回転量変化
        NowRoll += AddRoll;
    }
}
