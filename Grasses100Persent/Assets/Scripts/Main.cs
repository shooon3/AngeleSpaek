using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public enum GameState {Title = 1,Difficulty,Game,Result, }//ゲームの状態
    public GameState NowState;//現在の状態
    private GameState LastStae;//ステートの最後

    public static bool ShotF = false;//角度変更判定フラグ
    public static bool ReturnToTitleF;//リターンタイトル判定フラグ

    //プレファブ一覧
    //タイトル
    public GameObject TitlePre;
    public GameObject RunAngelPre;
    public GameObject TapToStartPre;
    //難易度設定
    public GameObject AddDifficultyButtonPre;
    public GameObject ReduceDifficultyButtonPre;
    public GameObject DifficultyNumBoxPre;
    public GameObject NextStateButtonPre;
    //ゲーム
    public GameObject AngelPre;
    public GameObject OtaniPre;
    public GameObject GirlPre;
    public GameObject RatedHartPre;
    //リザルト
    public GameObject RatedTextPre;
    public Sprite ClearSprite;
    public Sprite GameOverSprite;

    //ゲームオブジェクト一覧
    //タイトル
    private GameObject TitleObj;
    private GameObject RunAngelObj;
    private GameObject TapToStartObj;
    //難易度設定
    private GameObject AddDifficultyButtonObj;
    private GameObject ReduceDifficultyButtonObj;
    private GameObject DifficultyNumBoxObj;
    private GameObject NextStateButtonObj;
    //ゲーム
    private GameObject AngelObj;
    private GameObject OtaniObj;
    private GameObject GirlObj;
    private GameObject RatedHartObj;
    //リザルト
    private GameObject RatedTextObj;

    //生成位置一覧
    //タイトル
    public Vector3 TitlePos;
    public Vector3 RunAngelPos;
    public Vector3 TTSPos;
    //難易度設定
    public Vector3 ADBPos;
    public Vector3 RDBPos;
    public Vector3 DNBPos;
    public Vector3 NSBPos;
    //ゲーム
    public Vector3 AngelPos;
    public Vector3 OtaniPos;
    public Vector3 GirlPos;
    public Vector3 RatedHartPos;
    //リザルト
    public Vector3 RatedTextPos;

    //各種スクリプト
    private Player Player;
    private Enemy Enemy;
    private Girl Girl;

    //背景
    public Sprite GameBack;
    public Sprite ResultBack;
    public Image BackGrandImage;
    public Transform Canvas;

    //ＢＧＭ
    public AudioSource BGM;
    public AudioClip TitleBGM;
    public AudioClip DifficultySelectBGM;
    public AudioClip GameBGM;
    public AudioClip ClearSound;
    public AudioClip OverSound;
    public float SilegntTime;

    public void NextState(){
        //シーンから不要なものを削除
        ObjDestroyer();
        
        if (NowState >= LastStae){
            NowState = (GameState)1;//ステートが最後なら最初に戻す
        }
        else {
            NowState++;//ステートを進める
        }

        //シーンに必要なオブジェクトを生成
        ObjInstancer();
    }//ステート進行メソッド

    private void NextState(GameState GS){
        //シーンで使用したオブジェクトを削除
        ObjDestroyer();
        //指定したステートへ飛ぶ
        NowState = GS;
        //オブジェクト生成
        ObjInstancer();
    }//ステート遷移メソッド

    private void ObjInstancer(){
        //ステートによって生成物切替
        switch (NowState) {
            case GameState.Title:
                TitleObj = (GameObject)Instantiate(TitlePre, TitlePos, Quaternion.identity);
                RunAngelObj = (GameObject)Instantiate(RunAngelPre, RunAngelPos, Quaternion.identity);
                TapToStartObj = (GameObject)Instantiate(TapToStartPre, TTSPos, Quaternion.identity);

                //ＢＧＭ準備
                BGM.clip = TitleBGM;
                BGM.loop = true;
                break;

            case GameState.Difficulty:

                //オブジェクト生成
                AddDifficultyButtonObj = (GameObject)Instantiate(AddDifficultyButtonPre);
                ReduceDifficultyButtonObj = (GameObject)Instantiate(ReduceDifficultyButtonPre);
                DifficultyNumBoxObj               = (GameObject)Instantiate(DifficultyNumBoxPre);
                NextStateButtonObj                   = (GameObject)Instantiate(NextStateButtonPre);

                //子オブジェクトに設定
                AddDifficultyButtonObj.transform.SetParent(Canvas);
                ReduceDifficultyButtonObj.transform.SetParent(Canvas);
                DifficultyNumBoxObj.transform.SetParent(Canvas);
                NextStateButtonObj.transform.SetParent(Canvas);

                //RectTransform調整
                RectTransform ADB = AddDifficultyButtonObj.GetComponent<RectTransform>();
                RectTransform RDB = ReduceDifficultyButtonObj.GetComponent<RectTransform>();
                RectTransform DNB = DifficultyNumBoxObj.GetComponent<RectTransform>();
                RectTransform NSB  = NextStateButtonObj.GetComponent<RectTransform>();

                //位置調整
                ADB.localPosition = ADBPos;
                RDB.localPosition = RDBPos;
                DNB.localPosition = DNBPos;
                NSB.localPosition  = NSBPos;

                //大きさ調整
                ADB.localScale = new Vector3(1, 1, 1);
                RDB.localScale = new Vector3(1, 1, 1);
                DNB.localScale = new Vector3(1, 1, 1);
                NSB.localScale  = new Vector3(1, 1, 1);

                //クリックイベント設定
                AddDifficultyButtonObj.GetComponent<Button>().onClick.AddListener(delegate { MassageList.DifficultyToHard(); });
                ReduceDifficultyButtonObj.GetComponent<Button>().onClick.AddListener(delegate { MassageList.DifficultyToEasy(); });
                NextStateButtonObj.GetComponent<Button>().onClick.AddListener(delegate { MassageList.DifficultySet(); });
                NextStateButtonObj.GetComponent<Button>().onClick.AddListener(delegate { NextState(); });

                //ＢＧＭ準備
                BGM.clip = DifficultySelectBGM;
                break;

            case GameState.Game:
                //オブジェクト生成
                AngelObj           = (GameObject)Instantiate(AngelPre, AngelPos, Quaternion.identity);
                OtaniObj            = (GameObject)Instantiate(OtaniPre, OtaniPos, Quaternion.identity);
                GirlObj                 = (GameObject)Instantiate(GirlPre, GirlPos, Quaternion.identity);
                RatedHartObj = (GameObject)Instantiate(RatedHartPre, RatedHartPos, Quaternion.identity);

                //スクリプト紐づけ
                Player = AngelObj.transform.GetChild(0).GetComponent<Player>();
                Enemy = OtaniObj.GetComponent<Enemy>();
                Girl = GirlObj.GetComponent<Girl>();

                //最初の話題を決定
                Girl.TalkTitleChange();

                //ＢＧＭ準備
                BGM.clip = GameBGM;
                BGM.loop = true;
                break;

            case GameState.Result:
                RatedTextObj = (GameObject)Instantiate(RatedTextPre, RatedTextPos, Quaternion.identity);
                RatedTextObj.GetComponent<SpriteRenderer>().sprite = (Girl.NowRated >= Girl.MaxRated) ? ClearSprite : GameOverSprite;

                //ＢＧＭ準備
                BGM.clip = (Girl.NowRated >= Girl.MaxRated) ? ClearSound : OverSound;
                BGM.loop = false;
                break;

            default:
                break;
        }

        BackGrandImage.sprite = (NowState == GameState.Result) ? ResultBack : GameBack;//背景変更

        Invoke("BGMPlayer", SilegntTime);//ＢＧＭ再生

    }//シーンに必要なオブジェクトを生成

    private void ObjDestroyer(){
        //ステートによって削除オブジェクト切替
        switch (NowState){
            case GameState.Title:
                Destroy(TitleObj);
                Destroy(RunAngelObj);
                Destroy(TapToStartObj);
                break;

            case GameState.Difficulty:
                Destroy(AddDifficultyButtonObj);
                Destroy(ReduceDifficultyButtonObj);
                Destroy(DifficultyNumBoxObj);
                Destroy(NextStateButtonObj);
                break;

            case GameState.Game:
                Destroy(AngelObj);
                Destroy(OtaniObj);
                Destroy(GirlObj);
                Destroy(RatedHartObj);
                
                //吹き出し削除
                PlayerMassage.GameEnd();
                EnemyMassage.GameEnd();
                break;

            case GameState.Result:
                Destroy(RatedTextObj);
                
                //フラグ初期化
                ReturnToTitleF = false;
                break;

            default:
                break;
        }
    }//シーンで使用したオブジェクトを削除

    private void Title(){
        if (Input.GetButtonDown("Fire1")){
            //タップされたら次のシーンへ遷移
            NextState();
        }
    }//タイトルシーン処理

    private void Game(){

        //評価が最悪／最低になったら次のステートへ遷移
        if(Girl.NowRated >= Girl.MaxRated || Girl.NowRated <= Girl.MinRated){
            NextState();
            return;
        }

        //角度変更フラグで処理切替
        if (ShotF){

            Player.MassageChange();

            //指を離すとセリフ発射
            if (Input.GetButtonUp("Fire1") && ShotF){

                //フリーズ解除
                Enemy.AniStop = false;
                PlayerMassage.IsFreeze = false;
                EnemyMassage.IsFreeze = false;

                Player.Shot();

                ShotF = false;
            }

        }
        else {

            Player.DegChanger();//角度変更
            Enemy.IsShot();//大谷君しゃべる
            Girl.TalkTitleTimer();//女の子話題を変える

            //ロングタップ開始時角度変更を止める
            if (Input.GetButtonDown("Fire1")){
                Player.MassageSet();

                //フリーズ
                Enemy.AniStop = true;
                PlayerMassage.IsFreeze = true;
                EnemyMassage.IsFreeze = true;

                ShotF = true;
            }

        }

    }//ゲームシーン処理

    private void Result(){
        if (Input.GetButtonDown("Fire1") && ReturnToTitleF){
            //タップで次のシーンへ遷移
            NextState();
        }
    }//リザルトシーン処理

    private void BGMPlayer(){
        BGM.Play();
    }

    private void Awake(){
        //タイトルから始める
        NextState(GameState.Title);

        ReturnToTitleF = false;//フラグ初期化

        //ステートの最後を取得
        LastStae = (GameState)System.Enum.GetNames(typeof(GameState)).Length;
    }

    void Update(){
        //ステートによって処理切替
        switch (NowState){
            case GameState.Title:
                Title();
                break;

            case GameState.Difficulty:
                //ボタン管理
                break;

            case GameState.Game:
                Game();
                break;

            case GameState.Result:
                Result();
                break;

            default:
                //Stateにない値が入っているのは異常なので終了する
                Application.Quit();
                break;
        }

    }

}
