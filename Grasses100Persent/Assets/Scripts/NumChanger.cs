using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumChanger : MonoBehaviour {

    public Sprite[] DifficultySprite;

    public Image DifficultyText;

    private void Update(){
        DifficultyText.sprite = DifficultySprite[(int)MassageList.NowDiffculty];
    }

}
