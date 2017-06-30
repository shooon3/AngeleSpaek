using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour {

    public enum ObjectJanle {Image,Sprite };//オブジェクト種類
    public ObjectJanle Janle;//InspectorWindowでオブジェクト種類指定

    [Range(0,1)]
    private float Alpha;//更新α値

    [Range(0.1f, 0.9f)]
    public float AddAlpha;//α値変化量

    private bool OutOfRange;//範囲外判定フラグ

    //コンポーネント
    private SpriteRenderer SR;
    private Image Image;

    private void GetComponetSelect(){
        Color StartColor;//初期色

        switch (Janle){//オブジェクトの種類によってコンポーネント取得
            case ObjectJanle.Image:
                Image = GetComponent<Image>();

                //α値初期化
                StartColor = Image.color;
                StartColor.a = 0;
                Image.color = StartColor;
                break;
            case ObjectJanle.Sprite:
                SR = GetComponent<SpriteRenderer>();

                //α値初期化
                StartColor = SR.color;
                StartColor.a = 0;
                SR.color = StartColor;
                break;
            default:
                break;
        }
    }//点滅物切替メソッド

    private void AlphaChanger(){
        Color NextColor;//変更後色

        switch (Janle){
            case ObjectJanle.Image:
                NextColor = Image.color;//色取得
                Alpha = NextColor.a + AddAlpha * Time.deltaTime;//α値変化
                NextColor.a = Alpha;//α値更新
                Image.color = NextColor;//色更新
                break;
            case ObjectJanle.Sprite:
                NextColor = SR.color;//色取得
                Alpha = NextColor.a + AddAlpha * Time.deltaTime;//α値変化
                NextColor.a = Alpha;//α値更新
                SR.color = NextColor;//色更新
                break;
            default:
                break;
        }

        //α値領域外判定
        OutOfRange = (Alpha >= 1||Alpha <= 0) ? true : false;

        //領域外判定→変化量反転
        if (OutOfRange){
            AddAlpha = -AddAlpha;
        }

    }//α値変化メソッド

    void Awake(){
        GetComponetSelect();//点滅物切替
    }

    void Update(){
        AlphaChanger();//α値変更
    }
}
