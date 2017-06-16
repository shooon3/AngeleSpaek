using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    private float Scaler = 1;//比率
    public float AddScale;
    public float MaxScale;

    public float AddDeg = 1;

    public float AddAlpha;

    private Vector3 StartScale;

    private SpriteRenderer SR;

    private void Awake(){
        SR = GetComponent<SpriteRenderer>();
        StartScale = transform.localScale;
        AddAlpha = SR.color.a / ((MaxScale - Scaler) / AddScale);
    }

    private void Update(){
        if(Scaler >= MaxScale){
            Destroy(this.gameObject);
            return;
        }

        Scaler += AddScale;

        transform.localScale = StartScale * Scaler;
        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + AddDeg));
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, SR.color.a - AddAlpha);
    }

}
