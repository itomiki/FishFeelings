using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour{

    //移動させるコンポーネントを入れる
    private Rigidbody2D myRigidbody;
    //Collider2Dを入れる
    private Collider2D myObjectCollider2D;
    //
    private AudioSource myAudioSource;

    //地面に触れたかどうか（true == 触れた, false == 触れてない）
    private bool isTouch = false;

    //ScoreControllerスクリプトを呼び出すために使用
    private GameObject ScoreControllerObject;
    private ScoreController ScoreController;

    //
    private GameObject SoundObject;
    private GameObject TrashSoundObject;
    private TrashSoundPlay TrashSoundPlay;

    //落下速度
    private float FallSpeed = -0.9f;
    //振幅および周波数（落下時の揺れに使用）
    private float Amplitude = 30.0f;
    private float Omega = 1.0f;

    // Start is called before the first frame update
    void Start(){
        //Collider2Dを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();
        this.myObjectCollider2D = gameObject.GetComponent<Collider2D>();

        //ScoreControllerスクリプトを取得
        this.ScoreControllerObject = GameObject.Find("ScoreController");
        this.ScoreController = this.ScoreControllerObject.GetComponent<ScoreController>();

        //
        this.SoundObject = GameObject.Find("Sound");
        this.TrashSoundObject = GameObject.Find("TrashSound");
        this.TrashSoundPlay = this.TrashSoundObject.GetComponent<TrashSoundPlay>();
    }

    // Update is called once per frame
    void Update(){
        //地面に触れていない場合
        if(this.isTouch == false){
            //落下させる
            this.myRigidbody.velocity = new Vector2(0.0f, this.FallSpeed);
            //揺れさせる
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.Amplitude * Mathf.Cos(this.Omega * Time.time));
        }
    }

    //判定に衝突した
    void OnTriggerEnter2D(Collider2D other){
        //playerに衝突およびまだ地面に触れていない場合
        if(other.gameObject.tag == "player" && this.isTouch == false){

            //自身がglassbrownまたはpetbottleの場合
            if(gameObject.tag == "glassbrown" || gameObject.tag == "petbottle"){
                //ScoreControllerスクリプトのScoreIncrease関数を呼び出す
                this.ScoreController.ScoreIncrease(0.3f);

			//自身がglassgreenまたはsekkenbottleの場合
            }else if(gameObject.tag == "glassgreen" || gameObject.tag == "sekkenbottle"){
                //ScoreControllerスクリプトのScoreIncrease関数を呼び出す
                this.ScoreController.ScoreIncrease(0.5f);
            }
            //
            //GetComponent<AudioSource>().Play();
            //
            this.TrashSoundPlay.trashSound();

            //オブジェクトを破壊する
            Destroy(this.gameObject);

		//地面に触れた場合
        }else if(other.gameObject.tag == "underwall"){
            //Collider2DのisTriggerのチェックを外す
            this.myObjectCollider2D.isTrigger = false;
            this.isTouch = true;
        }
    }
}