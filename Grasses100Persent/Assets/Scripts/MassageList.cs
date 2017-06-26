using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class MassageList{
    public enum Janle { Plase, Animal, Food, Hobby };

    public static int[] UseMassage=new int[10] { 0,0,0,0,0,0,0,0,0,0,};

    private static int UseMassageNum = 3;
    private static int MinUseMassageNum = 3;

    //使用する単語をランダムに決定
    public static void MassageSelection(){
        //配列の範囲を出ないようにする
        if(UseMassageNum > UseMassage.Length){
            UseMassageNum = UseMassage.Length;
        }

        for (int i = 0; i < UseMassage.Length; i++){
            int index = 0;

            if (i < UseMassageNum){
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

    public static void AddDifficulty(){
        //UseMassageの範囲を出ないように加算
        UseMassageNum = (UseMassageNum >= UseMassage.Length)?UseMassage.Length:UseMassageNum + 1;
    }

    public static void ReduceDifficulty(){
        //MinUseMassageNumの範囲を出ないように減算
        UseMassageNum = (UseMassageNum <= MinUseMassageNum) ? MinUseMassageNum : UseMassageNum - 1;
    }

    public static int ReturnDifficulty(){
        return UseMassageNum;
    }

}
