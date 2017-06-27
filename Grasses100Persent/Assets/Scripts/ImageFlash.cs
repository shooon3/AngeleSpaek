using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFlash : MonoBehaviour {

    private float alfa = 0;

    [Range(0.1f, 0.9f)]
    public float AddAlfa;

    private Image Image;

    void Start(){
        Image = GetComponent<Image>();
    }

    void Update()
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, alfa);
        alfa += AddAlfa * Time.deltaTime;

        AddAlfa = (alfa >= 1 || alfa <= 0) ? -AddAlfa : AddAlfa;
    }

}
