using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAngel : MonoBehaviour {
    //transform.position.yを減算
    //一定以下になったら停止

    public float Speed;//移動量
    public Vector3 StopPos;//止まる位置

    public GameObject ReturnToTitlePre;
    private GameObject ReturnToTitle;
    public Vector3 ReturnToTitlePos;

    private void Awake(){
        StartCoroutine(ResultMove());
    }

    private IEnumerator ResultMove(){
        while(transform.position.y > StopPos.y){
            transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime, transform.position.z);//移動
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ReturnToTitle = (GameObject)Instantiate(ReturnToTitlePre, ReturnToTitlePos, Quaternion.identity);
        Main.ReturnToTitleF = true;
    }

    private void OnDestroy(){
        Destroy(ReturnToTitle);
    }

}
