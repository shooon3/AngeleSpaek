using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class MassageList{
    //enum宣言
    public enum Diffculty {Easy,Normal,Hard };
    public static Diffculty NowDiffculty = Diffculty.Normal;

    //使用単語
    public static int[] UseMassage=new int[7] { 0,0,0,0,0,0,0};
    private static int UseMassageCount;
    private static int[] UseMassageNum = { 3, 5, 7 };

    //大谷君の会話間隔
    public static float ShotInterval;
    private static float[] ShotIntervalPre = { 5.0f, 3.0f,1.5f };

    //彼女の話題変更間隔
    public static float ChangeInterval;
    private static float[] ChangeIntervalPre = { 20.0f, 15.0f, 10.0f };

    //使用する単語をランダムに決定
    private static void MassageSelection(){
        //配列の範囲を出ないようにする
        if(UseMassageCount > UseMassage.Length){
            UseMassageCount = UseMassage.Length;
        }

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
    }

    public static string[] Massage = {
        "",
        "動物園",
        "水族館",
        "遊園地",
        "映画館",
        "学校",
        "図書館",
        "公園",
        "海",
        "山",
        "川",
        "犬",
        "猫",
        "オウム",
        "寿司",
        "ステーキ",
        "ラーメン",
        "ケーキ",
        "野鳥観察",
        "天体観測",
        "映画鑑賞",
        "BBQ",
        "釣り",
        "登山",
        "ダーツ",
    };

    public static void DifficultySet(){
        //キャスト変換を済ませる
        int index = (int)NowDiffculty;
        
        //使用ワード数決定
        UseMassageCount = UseMassageNum[index];
        //言葉発射間隔設定
        ShotInterval = ShotIntervalPre[index];
        //話題変更間隔設定
        ChangeInterval = ChangeIntervalPre[index];

        MassageSelection();
    }

    public static void DifficultyToHard(){
        NowDiffculty = (NowDiffculty != Diffculty.Hard) ? NowDiffculty + 1 : Diffculty.Hard;
    }

    public static void DifficultyToEasy(){
        NowDiffculty = (NowDiffculty != Diffculty.Easy) ? NowDiffculty - 1 : Diffculty.Easy;
    }

}
