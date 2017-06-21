using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public enum GameState {Title = 1,Game,Result, }//ゲームの状態
    public GameState NowState;//現在の状態
    private GameState LastStae;//ステートの最後

    private bool ShotF = false;//角度変更中か否か判定フラグ

    //ゲーム中のみ生成するオブジェクト
    public GameObject GirlMassagePre;
    private GameObject GM;
    public Vector3 GirlMassagePos;
    public Vector3 GirlMassageRotate;

    //各種スクリプト
    private Player Player;
    public Enemy Enemy;
    public Girl Girl;

    private bool IsStart = true;

    //天使
    public GameObject AngelPre;
    private GameObject AngelObj;
    public Vector3 AngelPos;

    //タイトル
    public GameObject TitlePre;
    private GameObject TitleObj;
    public Vector3 TitlePos;

    //次のステートに進める
    private void NextState(){
        //ステートが最後なら最初に戻す
        if (NowState >= LastStae){
            NowState = (GameState)1;
            //スタートに戻る
            IsStart = true;
        }
        //ステートを進める
        else {
            NowState++;
        }

    }

    private void Title(){
        if (IsStart){
            TitleObj = (GameObject)Instantiate(TitlePre,TitlePos, Quaternion.identity);
            IsStart = false;
        }

        if (Input.GetButtonDown("Fire1")){
            //タップされたら次のシーンへ遷移
            NextState();
            //ゲームシーンに生成
            AngelObj = (GameObject)Instantiate(AngelPre, AngelPos, Quaternion.identity);
            GM = (GameObject)Instantiate(GirlMassagePre, GirlMassagePos, Quaternion.Euler(GirlMassageRotate));
            Girl.Text = GM.transform.GetChild(0).GetComponent<TextMesh>();
            MassageList.MassageSelection(5);
            Girl.TalkTitleChange();
            //タイトルシーンから削除
            Destroy(TitleObj);

            //紐づけスクリプト捜索
            Player = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<Player>();
            return;
        }
    }//タイトルシーン処理

    private void Game(){

        //評価が最悪／最低になったら次のステートへ遷移
        if(Girl.NowRated >= Girl.MaxRated || Girl.NowRated <= Girl.MinRated){
            //吹き出しを破棄
            PlayerMassage.GameEnd();
            EnemyMassage.GameEnd();
            Destroy(GM);
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
            //評価を基に戻す
            Girl.NowRated = 0;
            //メガホンを基に戻す
            Player.MegahonReset();
            return;
        }
    }//リザルトシーン処理

    private void Awake(){
        //タイトルから始まる
        NowState = GameState.Title;

        //ステートの最後を取得
        LastStae = (GameState)System.Enum.GetNames(typeof(GameState)).Length;
    }

    void Update(){
        switch (NowState){
            case GameState.Title:
                Title();
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
