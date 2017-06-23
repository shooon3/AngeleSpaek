using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public enum GameState {Title = 1,Difficulty,Game,Result, }//ゲームの状態
    public GameState NowState;//現在の状態
    private GameState LastStae;//ステートの最後

    private bool ShotF = false;//角度変更中か否か判定フラグ

    public int UseMassageNum;//使用するワードの個数

    //プレファブ一覧
    //タイトル
    public GameObject TitlePre;
    public GameObject RunAngelPre;
    public GameObject TapToStartPre;
    //難易度設定
    public GameObject DifficultyText;
    public GameObject AddWordButton;
    public GameObject ReduceWordButton;
    public GameObject NumBox;
    public Sprite[] NumSprite;
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

    //ＢＧＭ
    public AudioSource BGM;
    public AudioClip TitleBGM;
    public AudioClip GameBGM;
    public AudioClip ClearSound;
    public AudioClip OverSound;
    public float SilegntTime;

    //public AudioSource AS;

    //次のステートに進める
    private void NextState(){
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
    }

    private void NextState(GameState GS){
        //シーンで使用したオブジェクトを削除
        ObjDestroyer();
        //指定したステートへ飛ぶ
        NowState = GS;
        //オブジェクト生成
        ObjInstancer();
    }

    private void ObjInstancer(){
        switch (NowState) {
            case GameState.Title:
                TitleObj                 = (GameObject)Instantiate(TitlePre, TitlePos, Quaternion.identity);
                RunAngelObj    = (GameObject)Instantiate(RunAngelPre, RunAngelPos, Quaternion.identity);
                TapToStartObj = (GameObject)Instantiate(TapToStartPre, TTSPos, Quaternion.identity);
                BGM.clip = TitleBGM;
                BGM.loop = true;
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

                //ゲーム準備
                UseMassageNum = (UseMassageNum < 1) ? 1 : UseMassageNum;//バグ回避
                //MassageList.MassageSelection(UseMassageNum);//使用ワード選択
                Girl.TalkTitleChange();
                BGM.clip = GameBGM;
                BGM.loop = true;
                break;
            case GameState.Result:
                RatedTextObj = (GameObject)Instantiate(RatedTextPre, RatedTextPos, Quaternion.identity);
                RatedTextObj.GetComponent<SpriteRenderer>().sprite = (Girl.NowRated >= Girl.MaxRated) ? ClearSprite : GameOverSprite;
                BGM.clip = (Girl.NowRated >= Girl.MaxRated) ? ClearSound : OverSound;
                BGM.loop = false;
                break;
            default:
                break;
        }

        BackGrandImage.sprite = (NowState == GameState.Result) ? ResultBack : GameBack;//背景変更
        //BGM.clip = (NowState == GameState.Title) ? TitleBGM : GameBGM;

        Invoke("BGMPlayer", SilegntTime);
        //BGM.Play();

    }//シーンに必要なオブジェクトを生成

    private void ObjDestroyer(){
        switch (NowState){
            case GameState.Title:
                Destroy(TitleObj);
                Destroy(RunAngelObj);
                Destroy(TapToStartObj);
                break;
            case GameState.Game:
                Destroy(AngelObj);
                Destroy(OtaniObj);
                Destroy(GirlObj);
                Destroy(RatedHartObj);
                PlayerMassage.GameEnd();
                EnemyMassage.GameEnd();
                break;
            case GameState.Result:
                Destroy(RatedTextObj);
                break;
            default:
                break;
        }
    }//シーンで使用したオブジェクトを削除

    private void Title(){
        if (Input.GetButtonDown("Fire1")){
            //タップされたら次のシーンへ遷移
            //AS.Play();
            NextState();
        }
    }//タイトルシーン処理

    private void DiffiCultySelect(){

    }

    private void Game(){

        //評価が最悪／最低になったら次のステートへ遷移
        if(Girl.NowRated >= Girl.MaxRated || Girl.NowRated <= Girl.MinRated){
            NextState();
            return;
        }

        //指を離すとセリフ発射
        if (Input.GetButtonUp("Fire1") && ShotF){
            ShotF = false;

            //フリーズ解除
            PlayerMassage.IsFreeze = false;
            EnemyMassage.IsFreeze = false;

            Player.Shot();
        }

        //ロングタップ開始時角度変更を止める
        if (Input.GetButtonDown("Fire1")){
            ShotF = true;

            //フリーズ
            PlayerMassage.IsFreeze = true;
            EnemyMassage.IsFreeze = true;

            Player.MassageSet();
        }

        if (ShotF){
            Player.MassageChange();
        }
        else {
            Player.DegChanger();
            Enemy.IsShot();
            Girl.TalkTitleTimer();
        }

    }//ゲームシーン処理

    private void Result(){
        if (Input.GetButtonDown("Fire1")){
            //タップで次のシーンへ遷移
            NextState();
        }
    }//リザルトシーン処理

    private void Awake(){
        //タイトルから始まる
        NextState(GameState.Title);

        //ステートの最後を取得
        LastStae = (GameState)System.Enum.GetNames(typeof(GameState)).Length;
    }

    private void BGMPlayer(){
        BGM.Play();
    }

    public void AddWord(){
        UseMassageNum = (UseMassageNum >= MassageList.UseMassage.Length) ? MassageList.UseMassage.Length : UseMassageNum + 1;
    }

    public void ReduceWord(){
        UseMassageNum = (UseMassageNum <= 0) ? 0 : UseMassageNum - 1;
    }

    public void GameStart(){
        MassageList.MassageSelection(UseMassageNum);
        NextState(GameState.Game);
    }

    void Update(){
        switch (NowState){
            case GameState.Title:
                Title();
                break;
            case GameState.Difficulty:
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
