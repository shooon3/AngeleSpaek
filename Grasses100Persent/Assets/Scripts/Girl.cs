using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour {

    //評価パラメータ
    private int NowRated;//現在の評価
    private int MaxRated;//評価最大値
    private int MinRated;//評価最低値

    //セリフ
    private enum MassageRated{Good,Normal,Bad}//評価段階
    private MassageRated[] WordRated;//評価一覧
    private string[] Words;//開示セリフ

    //評価
    public void Rated(string Massage){
        //当たったセリフを開示セリフに照らし合わせる
        for (int i = 0; i < Words.Length; i++){
            if (Massage == Words[i]){
                //評価判定
                switch (WordRated[i]){
                    case MassageRated.Good://高評価
                        NowRated++;//評価上げ
                        if(NowRated > MaxRated){
                            NowRated = MaxRated;
                        }
                        break;
                    case MassageRated.Normal://普通
                        break;
                    case MassageRated.Bad://悪評価
                        NowRated--;//評価下げ
                        if(NowRated < MinRated){
                            NowRated = MinRated;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }

}
