using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUIController : MonoBehaviour{

    //獲得したスコア（結果）
    private float DisplayScore;
    //演出用スコア
    private float CurrentScore = 0.0f;
    //獲得したスコアを表示したかどうか（true == 表示した, false == まだ表示していない）
    private bool isScore = false;

    //ScoreTextを入れる
    private GameObject scoreText;
    //ReTryButtonを入れる
    private GameObject reTryButtonObject;
    //GoTitleButtonを入れる
    private GameObject goTitleButtonObject;
    //ArrowPrefabを入れる
    public GameObject ArrowPrefab;

    //演出用の矢印を出したかどうか（ture == 出した, false = まだ出していない）
    private bool isBreak = false;


    // Start is called before the first frame update
    void Start(){
        //UIControllerのgoResultScoreを代入する
        DisplayScore = UIController.goResultScore;

        //ScoreTextの実体を検索する
        scoreText = GameObject.Find("ScoreText");

        //ReTryButtonの実体を検索する
        reTryButtonObject = GameObject.Find("ReTryButton");
        //ReTryButtonを非表示にする
        reTryButtonObject.SetActive(false);
        //GoTitleButtonの実体を検索する
        goTitleButtonObject = GameObject.Find("GoTitleButton");
        //GoTitleButtonを非表示にする
        goTitleButtonObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if(isScore && isBreak == false){
            //獲得したスコアを表示
            scoreText.GetComponent<Text>().text = DisplayScore.ToString("F0") + "pt";
            //ArrowPos関数を呼び出す
            ArrowPos();
            //Wait関数を呼び出す
            Wait();

        }else if(isScore == false && isBreak == false){
            if(CurrentScore >= DisplayScore - 10.0f || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Return)){
                isScore = true;
            }else{
                //演出用スコアに10ずつ加算する
                CurrentScore += 10.0f;
                //演出用スコアを表示する
                scoreText.GetComponent<Text>().text = CurrentScore.ToString("F0") + "pt";
            }
        }
    }

    //矢印を設置する関数
    public void ArrowPos(){
        //獲得したスコアが3000以下の場合
        if(DisplayScore <= 3000){
            //サウンドを鳴らす
            GetComponent<AudioSource>().Play();
            //矢印を生成する
            ArrowPrefab = Instantiate(ArrowPrefab);
            //矢印を設置する
            ArrowPrefab.transform.position = new Vector2(-2.6f, -3.4f);
            isBreak = true;

		//獲得したスコアが7000以下の場合
        }else if(DisplayScore <= 7000){
			GetComponent<AudioSource>().Play();
			ArrowPrefab = Instantiate(ArrowPrefab);
            ArrowPrefab.transform.position = new Vector2(-1.0f, -0.7f);
            isBreak = true;


        }else{
			GetComponent<AudioSource>().Play();
			ArrowPrefab = Instantiate(ArrowPrefab);
            ArrowPrefab.transform.position = new Vector2(0.5f, 1.75f);
            isBreak = true;
        }
    }

    //少し待たせる関数
    public void Wait(){
        //reTryTitle関数を呼び出す
        Invoke("reTryORgoTitle", 1.5f);
    }

    //ReTryButtonANDGoTitleButtonを表示する関数
    public void reTryORgoTitle(){
        //ボタンを表示する
        this.reTryButtonObject.SetActive(true);
		this.goTitleButtonObject.SetActive(true);
    }
}
