using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ENMassagePre;//エネミーセリフプレファブ
    private GameObject ENMassage;//エネミーセリフ

    public void ENMassageSet()
    {
        ENMassage = Instantiate(ENMassagePre, transform.position, Quaternion.identity) as GameObject;//セリフを生成
        Debug.Log("敵がセリフを配置しました。");
    }

 
}
