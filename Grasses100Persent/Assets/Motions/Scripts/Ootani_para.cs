using UnityEngine;
using System;
using System.Collections;
using live2d;

[ExecuteInEditMode]
public class Ootani_para : MonoBehaviour
{
    public TextAsset mocFile;
    public Texture2D[] textureFiles;

    private Live2DModelUnity live2DModel;
    private Matrix4x4 live2DCanvasPos;

    // パラメーター
    [Range(0.0f, 2.0f)]
    public float eye_l_open;    // 左眼 開閉
    [Range(0.0f, 2.0f)]
    public float eye_r_open;    // 右眼 開閉
    [Range(0.0f, 1.0f)]
    public float mouth_open_y;  // 口 開閉
    [Range(0.0f, 1.0f)]
    public float breath;        // 呼吸


    void Start()
    {
        Live2D.init();

        //　モデルのロード
        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);

        // Live2Dのレンダーモード変更
        live2DModel.setRenderMode(Live2D.L2D_RENDER_DRAW_MESH);

        for (int i = 0; i < textureFiles.Length; i++)
        {
            live2DModel.setTexture(i, textureFiles[i]);
        }


        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);

        //値の初期化
        ValueReset();
    }


    
    void Update()
    //void OnRenderObject()
    {
        if (live2DModel == null) return;
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        if (!Application.isPlaying)
        {
            live2DModel.update();
            live2DModel.draw();
            return;
        }

        double t = (UtSystem.getUserTimeMSec() / 1000.0) * 2 * Math.PI;
        live2DModel.setParamFloat("PARAM_ANGLE_X", (float)(30 * Math.Sin(t / 3.0)));

        // パラメータ更新

        live2DModel.setParamFloat("PARAM_EYE_L_OPEN", (float)eye_l_open);       // 左眼 開閉
        live2DModel.setParamFloat("PARAM_EYE_R_OPEN", (float)eye_r_open);       // 右眼 開閉
        live2DModel.setParamFloat("PARAM_MOUTH_OPEN_Y", (float)mouth_open_y);   // 口 開閉
        live2DModel.setParamFloat("PARAM_BREATH", (float)breath);               // 呼吸


        live2DModel.update();
        live2DModel.draw();
    }


    // 値の初期化を行う
    public void ValueReset()
    {
        this.eye_l_open = 1.0f;     // 左眼 開閉
        this.eye_r_open = 1.0f;     // 右眼 開閉
        this.mouth_open_y = 0.0f;   // 口 開閉
        this.breath = 0.0f;         // 呼吸

    }
}