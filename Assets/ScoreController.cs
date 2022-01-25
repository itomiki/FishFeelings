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

    // Start is called before the first frame update
    void Start(){
        //EsaGimmickGeneratorオブジェクトの実体を検索
        this.EsaGimmickGeneratorObject = GameObject.Find("EsaGimmickGenerator");
        //EsaGimmickGeneratorを取得
        this.EsaGimmickGenerator = this.EsaGimmickGeneratorObject.GetComponent<EsaGimmickGenerator>();
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
    }

    //スコア加算関数（PlayerControllerスクリプトから呼び出す）
    public void ScoreAdd(float comeScore){
        //スコアを加算
        this.TotalScore += (comeScore * this.ScoreAddRate);
        Debug.Log(this.TotalScore);
    }

    //スコア上昇関数（TrashControllerスクリプトから呼び出す）
    public void ScoreIncrease(float IncreaseRate){
        //スコア上昇割合を変更する
        this.ScoreAddRate += IncreaseRate;
        Debug.Log("ScoreIncreaseRate = " + this.ScoreAddRate);
    }
}
