using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class MassageList{

    public enum Diffculty {Easy,Normal,Hard };//難易度段階
    public static Diffculty NowDiffculty = Diffculty.Normal;//難易度（初期はNormal）

    //使用単語
    public static int[] UseMassage=new int[7] { 0,0,0,0,0,0,0};//単語一覧
    private static int UseMassageCount;//使用単語数
    private static int[] UseMassageNum = { 3, 5, 7 };//難易度別使用単語数

    //大谷君の会話間隔
    public static float ShotInterval;//使用会話間隔
    private static float[] ShotIntervalPre = { 5.0f, 3.0f,1.5f };//難易度別会話間隔

    //彼女の話題変更間隔
    public static float ChangeInterval;//使用話題変更間隔
    private static float[] ChangeIntervalPre = { 20.0f, 15.0f, 10.0f };//難易度別変更間隔

    //彼女の最大評価値
    public static int MaxRated;//使用最大評価値
    private static int[] MaxRatePre = { 6, 8, 8 };//難易度別最大評価値

    private static void MassageSelection(){
      
        //配列の範囲を出ないようにする
        if(UseMassageCount > UseMassage.Length){
            UseMassageCount = UseMassage.Length;
        }

        //使用単語決定
        for (int i = 0; i < UseMassage.Length; i++){
            int index = 0;

            if (i < UseMassageCount){
                index = Random.Range(1, Massage.Length);//ランダムにワードを決定

                //同じワードが含まれないようにする
                for (int j = 0; j < i; j++){
                    if (UseMassage[j] == index){
                        continue;
                    }

                }

            }

            //ワードを確定
            UseMassage[i] = index;

        }

    }//使用単語決定メソッド

    public static string[] Massage = {
        "",
        "旅行",
        "スポーツ",
        "ペット",
        "グルメ",
        "アニメ",
        "映画",
        "ゲーム",
    };

    public static void DifficultySet(){
        //キャスト変換を済ませる
        int index = (int)NowDiffculty;
        
        //値設定
        UseMassageCount = UseMassageNum[index];//使用ワード数
        ShotInterval = ShotIntervalPre[index];//言葉発射間隔
        ChangeInterval = ChangeIntervalPre[index];//話題変更間隔
        MaxRated = MaxRatePre[index];//最大評価値
        MassageSelection();//使用単語

    }//難易度設定メソッド

    public static void DifficultyToHard(){
        NowDiffculty = (NowDiffculty != Diffculty.Hard) ? NowDiffculty + 1 : Diffculty.Hard;
    }//難易度上昇

    public static void DifficultyToEasy(){
        NowDiffculty = (NowDiffculty != Diffculty.Easy) ? NowDiffculty - 1 : Diffculty.Easy;
    }//難易度低下

}
