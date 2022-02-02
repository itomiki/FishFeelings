using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour{

    //totalScoreTextオブジェクト
    private GameObject totalScoreText;
    //gameOverTextオブジェクト
    private GameObject gameOverText;
    //scoreBonusTextオブジェクト
    private GameObject scoreBonusText;
    //timeLimitTextオブジェクト
    private GameObject timeLimitText;
    //TimeLimitTextオブジェクトのTextコンポーネントを入れる
    private Text TimeLimitText;
    //finishTextオブジェクト
    private GameObject finishText;

    //goResultButtonObject（結果ボタン）
    private GameObject goResultButtonObject;

    //スコア合計
    private float TotalScore = 0.0f;
    //スコア上昇割合
    private float IncreaseRate = 0.0f;
    //ResultSceneで使用するスコア（static）
    public static float goResultScore;

    //タイムリミット
    private float TimeLimit = 90.0f;
    //現在の経過時間
    private float CurrentTime = 0.0f;
    //表示するタイム
    private float DisplayTime;

    //GameOverフラグ
    private bool isGameOver = false;

    //Gameが終了したかどうか（true == 終了, false == ゲームプレイ中）
    private bool isFinish = false;

    //PlayerControllerスクリプトを呼び出すために使用
    private GameObject PlayerObject;
    private PlayerController PlayerController;

    //BGMをストップさせる時に使用
    private GameObject SoundObject;
    private GameObject BGMObject;

    //ScoreControllerスクリプトを呼び出すために使用
	//private GameObject ScoreControllerObject;
    //private ScoreController ScoreController;


    // Start is called before the first frame update
    void Start(){

        //TotalScoreオブジェクオの実体を検索
        this.totalScoreText = GameObject.Find("TotalScore");
        //GameOverオブジェクトの実体を検索
        this.gameOverText = GameObject.Find("GameOver");
        //ScoreBonusオブジェクトの実体を検索
        this.scoreBonusText = GameObject.Find("ScoreBonus");
        //TimeLimitオブジェクトの実体を検索
        this.timeLimitText = GameObject.Find("TimeLimit");
        //TimeLimitオブジェクトのテキストコンポーネントを取得する
        this.TimeLimitText = this.timeLimitText.GetComponent<Text>();
        //Finishオブジェクトの実体を検索する
        this.finishText = GameObject.Find("Finish");

        //GoResultButtonオブジェクトの実体を検索する
        this.goResultButtonObject = GameObject.Find("GoResultButton");
        //GoResultButtonを非表示にする
        this.goResultButtonObject.SetActive(false);

        //Playerオブジェクトの実体を検索
        this.PlayerObject = GameObject.Find("Player");
        //PlayerControllerスクリプトを取得する
        this.PlayerController = this.PlayerObject.GetComponent<PlayerController>();

        //BGMを停止するために使用
        this.SoundObject = GameObject.Find("Sound");
        this.BGMObject = GameObject.Find("BGM");

        //ScoreControllerオブジェクトの実体を検索
        //this.ScoreControllerObject = GameObject.Find("ScoreController");
        //ScoreControllerを取得
        //this.ScoreController = this.ScoreControllerObject.GetComponent<ScoreController>();
        
    }

    // Update is called once per frame
    void Update(){
        //GameOverかつFinishでない場合
        if(isGameOver == false && isFinish == false){
            //スコアを表示する
            if(TotalScore >= 5000){
                //スコアが5000を超えた場合”????”にする
                this.totalScoreText.GetComponent<Text>().text = "????pt";

            }else{
				this.totalScoreText.GetComponent<Text>().text = this.TotalScore.ToString("F0") + "pt";
            }

            //DisplayTimeを計算
            this.DisplayTime = this.TimeLimit - this.CurrentTime;

            //DisplayTimeが0秒より大の時
            if(this.DisplayTime > 0){
                //DisplayTimeが10秒以下の時
                if(this.DisplayTime < 10.49f){
                    //DisplayTimeの色を赤色にする
                    this.TimeLimitText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                }

                //DisplayTimeを表示する
                this.timeLimitText.GetComponent<Text>().text = this.DisplayTime.ToString("F0");
            
			}else{
                //Finish関数を呼び出す
                Finish();
            }
        }

        //経過時間を更新
        this.CurrentTime += Time.deltaTime;
    }

    //スコア合計関数（ScoreControllerスクリプトから呼び出す）
    public void ComeTotalScore(float comeTotalScore){
        this.TotalScore = comeTotalScore;
    }

    //スコア上昇割合を表示する関数（ScoreControllerスクリプトから呼び出す）
    public void ComeScoreIncrease(float comeIncreaseRate){
        this.IncreaseRate += comeIncreaseRate;
        this.scoreBonusText.GetComponent<Text>().text = "+" + this.IncreaseRate.ToString("F0") + "%UP";
        //Debug.Log("UI_ComeScoreIncrease");
    }

    //GameOver関数（PlayerControllerスクリプトから呼び出す）
    public void GameOver(){
        //isFnishがfalseの場合
        if(this.isFinish == false){
            //GameOverを表示する
            this.gameOverText.GetComponent<Text>().text = "Game Over";
            //GameOverの時に流れる音を流す
            GetComponent<AudioSource>().Play();
            //BGMを停止する
            this.BGMObject.GetComponent<AudioSource>().Stop();
            //Wait関数を呼び出す
            Wait();
			this.isGameOver = true;
        }
    }

    //Finishを表示する関数（制限時間を満了した場合呼び出される）
    public void Finish(){
        //PlayerControllerスクリプトのIsFinish関数を呼び出す
        this.PlayerController.IsFinish();
        //Finish!を表示する
        this.finishText.GetComponent<Text>().text = "Finish!";
        //Wait関数を呼び出す
        Wait();
        this.isFinish = true;
    }

    //少し待たせる関数
    public void Wait(){
        //GoResult関数を呼び出す
        Invoke("GoResult", 1.5f);
    }

    //ResultSceneに表示するトータルスコアを渡す（代入しておく）
    public void GoResult(){
        //GameOverの場合
        if(isGameOver){
            //トータルスコアを0にする
            goResultScore = 0.0f;
        }else{
            //goResultScoreにTotalScoreを代入する
            goResultScore = this.TotalScore;
        }
        //goResultButtonを表示させる
        this.goResultButtonObject.SetActive(true);
    }
}