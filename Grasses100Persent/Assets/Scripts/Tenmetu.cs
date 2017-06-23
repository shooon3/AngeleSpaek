using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tenmetu : MonoBehaviour{


    private float alfa = 0;

    [Range(0.1f, 0.9f)]
    public float AddAlfa;

    private SpriteRenderer SR;

    void Start(){
        SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, alfa);
        alfa += AddAlfa * Time.deltaTime;

        AddAlfa = (alfa >= 1 || alfa <= 0) ? -AddAlfa : AddAlfa;
    }

  
   

   
}
