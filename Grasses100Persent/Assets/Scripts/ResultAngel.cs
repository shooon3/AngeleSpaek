using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAngel : MonoBehaviour {

    public float Speed;//移動量
    public Vector3 StopPos;//止まる位置

    //生成物
    public GameObject ReturnToTitlePre;
    private GameObject ReturnToTitleObj;
    public Vector3 ReturnToTitlePos;

    private void Awake(){
        StartCoroutine(ResultMove());
    }

    private IEnumerator ResultMove(){
        Vector3 NowPos = transform.position;
        while (transform.position.y > StopPos.y){//移動
            NowPos.y = transform.position.y - Speed * Time.deltaTime;
            transform.position = NowPos;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //移動終了後生成物を生成
        ReturnToTitleObj = (GameObject)Instantiate(ReturnToTitlePre, ReturnToTitlePos, Quaternion.identity);
        Main.ReturnToTitleF = true;
    }

    private void OnDestroy(){
        Destroy(ReturnToTitleObj);
    }

}
