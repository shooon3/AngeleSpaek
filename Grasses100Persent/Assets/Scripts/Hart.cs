using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hart : MonoBehaviour {

    private static float NowRated;
    private static float MaxRated = Girl.MaxRated;

    private Vector3 FormatScale;

    private void Awake(){
        FormatScale = transform.localScale;
    }

	// Update is called once per frame
	void Update () {
        //型変換
        NowRated = Girl.NowRated;
        transform.localScale = FormatScale * (NowRated / MaxRated);
    }
}
