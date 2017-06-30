using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour {

    public enum CollisionIs {Change,Destroy };//サウンド種類

    public AudioClip[] ChangeSE;//音量変更時サウンド
    public AudioClip[] DestroySE;//破壊時サウンド

    private AudioSource AS;

    public void SetSE(CollisionIs x){
        //引数で音の種類を宣言
        switch (x){
            case CollisionIs.Change://声量変換
                AS.clip = ChangeSE[Random.Range(0, ChangeSE.Length - 1)];//ランダムに音を決定
                break;
            case CollisionIs.Destroy://破壊
                AS.clip = DestroySE[Random.Range(0, DestroySE.Length - 1)];//ランダムに音を決定
                break;
            default:
                break;
        }
        if (AS.clip != null){
            AS.Play();
            Destroy(this.gameObject, AS.clip.length);//再生後自動で消滅
        }
        else{
            Destroy(this.gameObject);//エラー回避
        }
    }//AudioClip設定メソッド

    private void Awake(){
        AS = GetComponent<AudioSource>();//AudioSource取得
    }

}
