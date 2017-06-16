using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MassageList{
    public enum Janle { Plase, Animal, Food, Hobby };

    public static int[] UseMassage=new int[10];

    //使用する単語をランダムに決定
    public static void MassageSelection(int SelectCount){
        //配列の範囲を出ないようにする
        if(SelectCount > UseMassage.Length){
            SelectCount = UseMassage.Length;
        }

        for (int i = 0; i < SelectCount; i++){
            int index = Random.Range(1, Massage.Length);//ランダムにワードを決定

            ////同じワードが含まれないようにする
            //for (int j = 0; j < i; j++){
            //    if(UseMassage[j] == index){
            //        continue;
            //    }
            //}

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
        "飲食店",
        "学校",
        "図書館",
        "公園",
        "家",
        "海",
        "山",
        "犬",
        "猫",
        "ハムスター",
        "寿司",
        "ハンバーグ",
        "オムライス",
        "ラーメン",
        "ケーキ",
        "野鳥観察",
        "天体観測",
        "映画鑑賞",
        "BBQ",
        "釣り",
        "ダイビング",
        "登山",
        "ダーツ",
        "ジョギング"
    };

    public static Janle[] List ={
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Plase,
        Janle.Animal,
        Janle.Animal,
        Janle.Animal,
        Janle.Food,
        Janle.Food,
        Janle.Food,
        Janle.Food,
        Janle.Food,
        Janle.Hobby,
        Janle.Hobby,
        Janle.Hobby,
        Janle.Hobby,
        Janle.Hobby,
        Janle.Hobby,
        Janle.Hobby,
        Janle.Hobby,
        Janle.Hobby,
    };
}
