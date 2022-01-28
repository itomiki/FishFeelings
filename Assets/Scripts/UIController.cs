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
    //
    private GameObject timeLimitText;
    //
    private Text TimeLimitText;
    //
    private GameObject finishText;

    //スコア合計
    private float TotalScore = 0.0f;
    //スコア上昇割合
    private float IncreaseRate = 0.0f;

    //
    private float TimeLimit = 90.0f;
    //
    private float CurrentTime = 0.0f;

    //GameOverフラグ
    private bool isGameOver = false;

    //
    private bool isFinish = false;

    //
    private GameObject PlayerObject;
    private PlayerController PlayerController;

    //
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
        //
        this.timeLimitText = GameObject.Find("TimeLimit");
        //
        this.TimeLimitText = this.timeLimitText.GetComponent<Text>();
        //
        this.finishText = GameObject.Find("Finish");

        //
        this.PlayerObject = GameObject.Find("Player");
        //
        this.PlayerController = this.PlayerObject.GetComponent<PlayerController>();

        //
        this.SoundObject = GameObject.Find("Sound");
        this.BGMObject = GameObject.Find("BGM");

        //ScoreControllerオブジェクトの実体を検索
        //this.ScoreControllerObject = GameObject.Find("ScoreController");
        //ScoreControllerを取得
        //this.ScoreController = this.ScoreControllerObject.GetComponent<ScoreController>();
        
    }

    // Update is called once per frame
    void Update(){
        //GameOverでない場合
        if(isGameOver == false && isFinish == false){
            //スコアを表示する
            this.totalScoreText.GetComponent<Text>().text = "Score: " + this.TotalScore.ToString("F0") + "pt";

            //
            this.CurrentTime = this.TimeLimit - Time.time;
            //
            if(this.CurrentTime > 0){
                if(this.CurrentTime < 10){
                    this.TimeLimitText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                }

                //
				this.timeLimitText.GetComponent<Text>().text = this.CurrentTime.ToString("F0");
            
			}else{
				Finish();
            }
        }
    }

    //スコア合計関数（ScoreControllerスクリプトから呼び出す）
    public void ComeTotalScore(float comeTotalScore){
        this.TotalScore = comeTotalScore;
    }

    //スコア上昇割合を表示する関数（ScoreControllerスクリプトから呼び出す）
    public void ComeScoreIncrease(float comeIncreaseRate){
        this.IncreaseRate += comeIncreaseRate;
        this.scoreBonusText.GetComponent<Text>().text = "+" + this.IncreaseRate.ToString("F0") + "%UP";
        Debug.Log("UI_ComeScoreIncrease");
    }

    //GameOver関数（PlayerControllerスクリプトから呼び出す）
    public void GameOver(){
        //GameOverを表示する
        if(this.isFinish == false){
			this.gameOverText.GetComponent<Text>().text = "Game Over";
            GetComponent<AudioSource>().Play();
            this.BGMObject.GetComponent<AudioSource>().Stop();
			this.isGameOver = true;
        }
    }

    //
    public void Finish(){
        //
		this.PlayerController.IsFinish();
        //
        this.finishText.GetComponent<Text>().text = "Finish!";
        this.isFinish = true;
    }
}