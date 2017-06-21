﻿using UnityEngine; 
using System.Collections; 
 
[ExecuteInEditMode] 
public class TextAdjuster : MonoBehaviour{ 
 
    [Range(0, 10)] 
    public float fontScale; 
    TextMesh tetxMesh; 
 
    void Start() {
        tetxMesh = GetComponent<TextMesh>();
    } 
 	 
    void Update(){ 
        Vector3 defaultScale = new Vector3(1, 1, 1) * fontScale; 
        int fontSize = tetxMesh.fontSize; 
        fontSize = fontSize == 0 ? 12 : fontSize; 
 
        float scale = 0.1f * 128 / fontSize; 
        transform.localScale = defaultScale* scale; 
    } 
} 
