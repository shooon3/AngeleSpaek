using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyChanger : MonoBehaviour {

    public Sprite[] DifficultySprite;//Sprite

    //コンポーネント
    public Image DifficultyText;

    private void Update(){
        DifficultyText.sprite = DifficultySprite[(int)MassageList.NowDiffculty];//Sprite変更
    }

}
