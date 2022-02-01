using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownController : MonoBehaviour{

    //CountDownTextオブジェクトを入れる
    private GameObject CountDownText;
    //経過時間
    private float CurrentTime = 0.0f;
    //カウントダウンの長さ
    private float CountTime = 3.49f;
    //表示する時間
    private float DisplayTime;
    
    // Start is called before the first frame update
    void Start(){

        //CountDownTextの実体を検索
        CountDownText = GameObject.Find("CountDownText");
        
    }

    // Update is called once per frame
    void Update(){
        //表示時間を計算する
        DisplayTime = CountTime - CurrentTime;

        //表示時間が1秒以下の時
        if(DisplayTime <= 1){
            //GameSceneを呼び出す
            SceneManager.LoadScene("GameScene");
        }

        //カウントダウンを表示する
        CountDownText.GetComponent<Text>().text = DisplayTime.ToString("F0");
        //経過時間を更新する
        CurrentTime += Time.deltaTime;
    }
}
