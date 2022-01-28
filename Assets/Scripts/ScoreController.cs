using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour{

    //スコア合計
    public float TotalScore = 0.0f;
    private float firstScoreGrade = 200.0f;
    private float secondScoreGrade = 600.0f;

    //スコアの上昇割合
    private float ScoreAddRate = 1.0f;
    
    //EsaGimmickGeneratorのEsaGradeCntl関数を呼び出す時に使用
    private int SendCount = 0;

    //EsaGimmickGeneratorスクリプトを呼び出すために使用
    private GameObject EsaGimmickGeneratorObject;
    private EsaGimmickGenerator EsaGimmickGenerator;

    //UIControllerスクリプトを呼び出す時に使用
    private GameObject CanvasObject;
    private UIController UIController;

    //PlayerControllerスクリプト呼び出す時に使用
    private GameObject PlayerObject;
    private PlayerController PlayerController;

    //IsBigBig関数を呼び出すフラグ
    private bool FirstIsBigBig = true;
    private bool SecondIsBigBig = true;

    // Start is called before the first frame update
    void Start(){
        //EsaGimmickGeneratorオブジェクトの実体を検索
        this.EsaGimmickGeneratorObject = GameObject.Find("EsaGimmickGenerator");
        //EsaGimmickGeneratorを取得
        this.EsaGimmickGenerator = this.EsaGimmickGeneratorObject.GetComponent<EsaGimmickGenerator>();

        //EsaGimmickGeneratorオブジェクトの実体を検索
        this.CanvasObject = GameObject.Find("Canvas");
        //EsaGimmickGeneratorを取得
        this.UIController = this.CanvasObject.GetComponent<UIController>();

        //Playerオブジェクトの実体を検索
        this.PlayerObject = GameObject.Find("Player");
        //PlayerControllerを取得
        this.PlayerController = this.PlayerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update(){
        //スコア合計によって場合分け
        if(this.TotalScore >= this.firstScoreGrade && this.SendCount == 0){
            SendCount += 1;
            //EsaGimmickGeneratorのEsaGradeCntl関数を呼び出す
            this.EsaGimmickGenerator.EsaGradeCntl();

        }else if(this.TotalScore >= this.secondScoreGrade && this.SendCount == 1){
            SendCount += 1;
            //EsaGimmickGeneratorのEsaGradeCntl関数を呼び出す
            this.EsaGimmickGenerator.EsaGradeCntl();
        }

        //IsBigBig関数を呼び出してPlayerを大きくする
        if(this.TotalScore >= 600.0f && FirstIsBigBig){
            this.PlayerController.IsBigBig();
            FirstIsBigBig = false;

        }else if(this.TotalScore >= 1200.0f && SecondIsBigBig){
            this.PlayerController.IsBigBig();
            SecondIsBigBig = false;
        }
    }

    //スコア加算関数（PlayerControllerスクリプトから呼び出す）
    public void ScoreAdd(float comeScore){
        //スコアを加算
        this.TotalScore += (comeScore * this.ScoreAddRate);
        //ComeTotalScore関数を呼び出して現在のスコアを渡す
        this.UIController.ComeTotalScore(this.TotalScore);
        Debug.Log(this.TotalScore);
    }

    //スコア上昇関数（TrashControllerスクリプトから呼び出す）
    public void ScoreIncrease(float IncreaseRate){
        //スコア上昇割合を変更する
        this.ScoreAddRate += IncreaseRate;
        //スコア上昇割合をComeScoreIncrease関数に渡す
        this.UIController.ComeScoreIncrease(IncreaseRate * 100.0f);
        Debug.Log("ScoreIncreaseRate = " + this.ScoreAddRate);
    }
}