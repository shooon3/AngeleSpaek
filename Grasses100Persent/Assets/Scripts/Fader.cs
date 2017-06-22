using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

    public Image Back;

    public void FadeOut(){
    }

    private IEnumerator FadeOutColutin(){
        while (true){

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
