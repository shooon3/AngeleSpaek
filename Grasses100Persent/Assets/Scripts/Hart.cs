using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hart : MonoBehaviour {

    private static float MaxRated;//評価最大値

    private Vector3 FormatScale;//大きさ基準

    private void Awake(){
        FormatScale = transform.localScale;//基準取得
        MaxRated = MassageList.MaxRated;
    }

	// Update is called once per frame
	void Update () {
        //型変換
        float NowRated = Girl.NowRated;
        transform.localScale = FormatScale * (NowRated / MaxRated);//大きさを割合で変更
    }
}
