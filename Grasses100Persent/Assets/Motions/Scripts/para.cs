using UnityEngine;
using System;
using System.Collections;
using live2d;

[ExecuteInEditMode]
public class para : MonoBehaviour
{
    public TextAsset mocFile;
    public Texture2D[] textureFiles;

    private Live2DModelUnity live2DModel;
    private Matrix4x4 live2DCanvasPos;

    // パラメーター
    /*
    [Range(-30.0f, 30.0f)]
    public float angle_x;       // 角度 X
    [Range(-30.0f, 30.0f)]
    public float angle_y;       // 角度 Y
    [Range(-30.0f, 30.0f)]
    public float angle_z;       // 角度 Z
    [Range(0.0f, 2.0f)]
    public float eye_l_open;    // 左眼 開閉
    [Range(0.0f, 1.0f)]
    public float eye_l_smile;   // 左眼 笑顔
    [Range(0.0f, 2.0f)]
    public float eye_r_open;    // 右眼 開閉
    [Range(0.0f, 1.0f)]
    public float eye_r_smile;   // 右眼 笑顔
    [Range(0.0f, 1.0f)]
    public float eye_form;      // 眼 変形
    [Range(-1.0f, 1.0f)]
    public float eye_ball_x;    // 目玉 X
    [Range(-1.0f, 1.0f)]
    public float eye_ball_y;    // 目玉 Y
    [Range(-1.0f, 1.0f)]
    public float eye_ball_form; // 目玉 収縮
    [Range(-1.0f, 1.0f)]
    public float brow_l_y;      // 左眉 上下
    [Range(-1.0f, 1.0f)]
    public float brow_r_y;      // 右眉 上下
    [Range(-1.0f, 1.0f)]
    public float brow_l_x;      // 左眉 左右
    [Range(-1.0f, 1.0f)]
    public float brow_r_x;      // 右眉 左右
    [Range(-1.0f, 1.0f)]
    public float brow_l_angle;  // 左眉 角度
    [Range(-1.0f, 1.0f)]
    public float brow_r_angle;  // 右眉 角度
    [Range(-1.0f, 1.0f)]
    public float brow_l_form;   // 左眉 変形
    [Range(-1.0f, 1.0f)]
    public float brow_r_form;   // 右眉 変形
    [Range(-1.0f, 1.0f)]
    public float mouth_form;    // 口 変形
    [Range(0.0f, 1.0f)]
    public float mouth_open_y;  // 口 開閉
    [Range(0.0f, 1.0f)]
    public float cheek;         // 照れ
    [Range(0.0f, 1.0f)]
    public float body_angle_x;  // 体の回転 X
    [Range(-10.0f, 10.0f)]
    public float body_angle_y;  // 体の回転 Y
    [Range(-10.0f, 10.0f)]
    public float body_angle_z;  // 体の回転 Z
    [Range(0.0f, 1.0f)]
    public float breath;        // 呼吸
    [Range(0.0f, 1.0f)]
    public float arm_l_a;       // 左腕 A
    [Range(-1.0f, 1.0f)]
    public float arm_r_a;       // 右腕 A
    [Range(-1.0f, 1.0f)]
    public float arm_l_b;       // 左腕 B
    [Range(-1.0f, 1.0f)]
    public float arm_r_b;       // 右腕 B
    [Range(-1.0f, 1.0f)]
    public float bust_y;        // 胸 揺れ
    [Range(-1.0f, 1.0f)]
    public float hair_front;    // 髪揺れ 前
    [Range(-1.0f, 1.0f)]
    public float hair_back;     // 髪揺れ 後        
    */
    [Range(0.0f, 1.0f)]
    public float kubi;

    void Start()
    {
        Live2D.init();

        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
        for (int i = 0; i < textureFiles.Length; i++)
        {
            live2DModel.setTexture(i, textureFiles[i]);
        }

        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);
        // 値の初期化
        ValueReset();
    }


    void OnRenderObject()
    {
        if (live2DModel == null) return;
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        //if (!Application.isPlaying)
        //{
        //  live2DModel.update();
        //  live2DModel.draw();
        //  return;
        //}

        //double t = (UtSystem.getUserTimeMSec()/1000.0) * 2 * Math.PI  ;
        //live2DModel.setParamFloat( "PARAM_ANGLE_X" , (float) (30 * Math.Sin( t/3.0 )) ) ;

        // パラメータ更新
        /*live2DModel.setParamFloat("PARAM_ANGLE_X", (float)angle_x);             // 角度 X
        live2DModel.setParamFloat("PARAM_ANGLE_Y", (float)angle_y);             // 角度 Y
        live2DModel.setParamFloat("PARAM_ANGLE_Z", (float)angle_z);             // 角度 Z
        live2DModel.setParamFloat("PARAM_EYE_L_OPEN", (float)eye_l_open);       // 左眼 開閉
        live2DModel.setParamFloat("PARAM_EYE_L_SMILE", (float)eye_l_smile);     // 左眼 笑顔
        live2DModel.setParamFloat("PARAM_EYE_R_OPEN", (float)eye_r_open);       // 右眼 開閉
        live2DModel.setParamFloat("PARAM_EYE_R_SMILE", (float)eye_r_smile);     // 右眼 笑顔
        live2DModel.setParamFloat("PARAM_EYE_FORM", (float)eye_form);           // 眼 変形
        live2DModel.setParamFloat("PARAM_EYE_BALL_X", (float)eye_ball_x);       // 目玉 X
        live2DModel.setParamFloat("PARAM_EYE_BALL_Y", (float)eye_ball_y);       // 目玉 Y
        live2DModel.setParamFloat("PARAM_EYE_BALL_FORM", (float)eye_ball_form); // 目玉 収縮
        live2DModel.setParamFloat("PARAM_BROW_L_Y", (float)brow_l_y);           // 左眉 上下
        live2DModel.setParamFloat("PARAM_BROW_R_Y", (float)brow_r_y);           // 右眉 上下
        live2DModel.setParamFloat("PARAM_BROW_L_X", (float)brow_l_x);           // 左眉 左右
        live2DModel.setParamFloat("PARAM_BROW_R_X", (float)brow_r_x);           // 右眉 左右
        live2DModel.setParamFloat("PARAM_BROW_L_ANGLE", (float)brow_l_angle);   // 左眉 角度
        live2DModel.setParamFloat("PARAM_BROW_R_ANGLE", (float)brow_r_angle);   // 右眉 角度
        live2DModel.setParamFloat("PARAM_BROW_L_FORM", (float)brow_l_form);     // 左眉 変形
        live2DModel.setParamFloat("PARAM_BROW_R_FORM", (float)brow_r_form);     // 右眉 変形
        live2DModel.setParamFloat("PARAM_MOUTH_FORM", (float)mouth_form);       // 口 変形
        live2DModel.setParamFloat("PARAM_MOUTH_OPEN_Y", (float)mouth_open_y);   // 口 開閉
        live2DModel.setParamFloat("PARAM_CHEEK", (float)cheek);                 // 照れ
        live2DModel.setParamFloat("PARAM_BODY_ANGLE_X", (float)body_angle_x);   // 体の回転 X
        live2DModel.setParamFloat("PARAM_BODY_ANGLE_Y", (float)body_angle_y);   // 体の回転 X
        live2DModel.setParamFloat("PARAM_BODY_ANGLE_Z", (float)body_angle_z);   // 体の回転 Z
        live2DModel.setParamFloat("PARAM_BREATH", (float)breath);               // 呼吸
        live2DModel.setParamFloat("PARAM_ARM_L_A", (float)arm_l_a);             // 左腕 A
        live2DModel.setParamFloat("PARAM_ARM_R_A", (float)arm_r_a);             // 右腕 A
        live2DModel.setParamFloat("PARAM_ARM_L_A", (float)arm_l_b);             // 左腕 B
        live2DModel.setParamFloat("PARAM_ARM_R_A", (float)arm_r_b);             // 右腕 B 
        live2DModel.setParamFloat("PARAM_BUST_Y", (float)bust_y);               // 胸 揺れ   
        live2DModel.setParamFloat("PARAM_HAIR_FRONT", (float)hair_front);       // 髪揺れ 前
        live2DModel.setParamFloat("PARAM_HAIR_BACK", (float)hair_back);         // 髪揺れ 後
        */
        live2DModel.setParamFloat("PARAM_KUBI", (float)kubi);

        live2DModel.update();
        live2DModel.draw();
    }

    // 値の初期化を行う
    public void ValueReset()
    {
        /*
        this.angle_x = 0.0f;        // 角度 X
        this.angle_y = 0.0f;        // 角度 Y
        this.angle_z = 0.0f;        // 角度 Z
        this.eye_l_open = 1.0f;     // 左眼 開閉
        this.eye_l_smile = 0.0f;    // 左眼 笑顔
        this.eye_r_open = 1.0f;     // 右眼 開閉
        this.eye_r_smile = 0.0f;    // 右眼 笑顔
        this.eye_ball_x = 0.0f;     // 目玉 X
        this.eye_ball_y = 0.0f;     // 目玉 Y
        this.brow_l_y = 0.0f;       // 左眉 上下
        this.brow_r_y = 0.0f;       // 右眉 上下
        this.brow_l_x = 0.0f;       // 左眉 左右
        this.brow_r_x = 0.0f;       // 右眉 左右
        this.brow_l_angle = 0.0f;   // 左眉 角度
        this.brow_r_angle = 0.0f;   // 右眉 角度
        this.brow_l_form = 0.0f;    // 左眉 変形
        this.brow_r_form = 0.0f;    // 右眉 変形
        this.mouth_form = 1.0f;     // 口 変形
        this.mouth_open_y = 0.0f;   // 口 開閉
        this.cheek = 0.0f;          // 照れ
        this.body_angle_x = 0.0f;   // 体の回転 X
        this.body_angle_y = 0.0f;   // 体の回転 Y
        this.body_angle_z = 0.0f;   // 体の回転 Z
        this.breath = 0.0f;         // 呼吸
        this.arm_l_a = 0.5f;        // 左腕 A
        this.arm_r_a = 0.5f;        // 右腕 A
        this.arm_l_b = 0.0f;        // 左腕 B
        this.arm_r_b = 0.0f;        // 右腕 B
        this.bust_y = 0.0f;         // 胸 揺れ
        this.hair_front = 0.0f;     // 髪揺れ 前
        this.hair_back = 0.0f;      // 髪揺れ 後
        */
        this.kubi = 0.5f;
    }
}