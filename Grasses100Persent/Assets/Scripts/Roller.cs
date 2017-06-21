using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour {

    public float AddRoll;
    private float NowAddRoll;
	
    void Awake(){
        NowAddRoll = AddRoll;
    }
    
    // Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y + NowAddRoll, transform.rotation.z));
       NowAddRoll += AddRoll;
    }
}
