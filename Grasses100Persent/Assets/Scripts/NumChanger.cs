using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumChanger : MonoBehaviour {

    public Sprite[] NumSprite;

    public Image TenNumber;
    public Image OneNumber;

    int TenNum;
    int OneNum;

    private void ImageChanger(Image ChangeNum,int Num){
        ChangeNum.sprite = NumSprite[Num];
    }

    private void Update(){
        //位の数リセット
        TenNum = 0;
        OneNum = MassageList.ReturnDifficulty();

        //位の数取得
        while (OneNum >= 10){
            OneNum -= 10;//１０未満になるまで１０の位を減らす
            TenNum++;//減らした数が１０の位の数
        }

        //イメージ変更
        ImageChanger(TenNumber, TenNum);
        ImageChanger(OneNumber, OneNum);

    }

}
