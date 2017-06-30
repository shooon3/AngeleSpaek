using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatedIcon : MonoBehaviour {

    public enum Rated { Up, Down }//評価
    public Rated NowRated{
        set{
            SR.sprite = (value == Rated.Up) ? RatedUpIcon[Random.Range(0, RatedUpIcon.Length)] : RatedDownIcon[Random.Range(0, RatedDownIcon.Length)];
        }
    }//評価でSprite変更

    //localScale
    private Vector3 StartScale;//初期値
    private float NowScale = 1;//比率
    public float AddScale;//変化量
    public float MaxScale;//最大値

    //角度
    private Vector3 NewRotate;//更新角度
    public float AddDeg;//変化量

    //透明度
    private Color NewColor;//更新色
    public float AddAlpha;//変化量

    //Sprite
    public Sprite[] RatedUpIcon;//評価アップ
    public Sprite[] RatedDownIcon;//評価ダウン

    //コンポーネント
    private SpriteRenderer SR;

    private void Awake(){
        //各変数初期化
        SR = GetComponent<SpriteRenderer>();//コンポーネント取得
        StartScale = transform.localScale;//初期位置取得
        NewRotate = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);//初期角度取得
        NewColor = new Color(SR.color.r, SR.color.g, SR.color.b, SR.color.a);//初期色取得
        AddAlpha = SR.color.a / ((MaxScale - NowScale) / AddScale);//変化量決定
    }

    public void LateUpdate(){
        //一定以上大きくなったら破棄
        if (NowScale >= MaxScale){
            Destroy(this.gameObject);
            return;
        }

        //変化量更新
        NowScale += AddScale;//大きさ
        NewRotate.z += AddDeg;//角度
        NewColor.a -= AddAlpha;//透明度

        transform.localScale = StartScale * NowScale;//巨大化
        transform.Rotate(NewRotate);//回転
        SR.color = NewColor;
    }


}
