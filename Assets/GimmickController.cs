using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickController : MonoBehaviour{

    //振幅
    private float Amplitude;
    private float HitAmplitude = 2.0f;  //Playerがヒットした時の振幅
    //周波数
    private float Omega;
    private float HitOmega = 2.0f;      //playerがヒットした時の周波数
    //private float Frequency;

    //エサギミックの落下速度
    private float Fallspeed = -2;
    //落下距離（目的地）
    private float Falldistance = 7.0f;
    //現在の落下距離
    private float Currentdistance = 0.0f;
    //エサギミックの引き上げ時間
    private float Outtime;
    //現在の経過時間（エサギミック到達後）
    private float Currenttime = 0.0f;

    //playerがヒットしたかどうか（true == ヒットした, false == ヒットしてない）
    private bool isPlayerHit = false;

    //衝突したエサのオブジェクト
    private GameObject ItemObject;

    // Start is called before the first frame update
    void Start(){
        //振幅の大きさを決める
        int amp = Random.Range(10, 81);
		this.Amplitude = amp / 100.0f;
        //int amp = Random.Range(5, 31);
		//this.Amplitude = amp / 1000.0f;
		//Debug.Log("Amplitude " + this.Amplitude);

        //周波数の値を決める
        int omg = Random.Range(5, 10);
		this.Omega = omg / 10.0f;
		//Debug.Log("Omega" + this.Omega);

        //int frq = Random.Range(5, 10);
		//this.Frequency = frq / 10.0f;
		//Debug.Log("Frequency " + this.Frequency);

        //エサギミックの引き上げ時間を決める
        int ott = Random.Range(16, 21);
        this.Outtime = ott * 1.0f;
		//Debug.Log("OutTime " + this.Outtime);
    }

    // Update is called once per frame
    void Update(){
        //現在の落下距離が落下距離（目的地）以下の場合
        if(this.Currentdistance <= this.Falldistance){
            //エサギミックを落下させる
            transform.Translate(0.0f, this.Fallspeed * Time.deltaTime, 0.0f);
            this.Currentdistance += (-this.Fallspeed * Time.deltaTime);

		//目的地到達後_かつ_引き上げ時間に満たない_かつ_playerにまだ食べられてない場合
        }else if(this.Currentdistance > this.Falldistance && this.Outtime >= this.Currenttime && this.isPlayerHit == false){
			//エサギミックを上下に揺らす
			transform.Translate(0.0f, (this.Amplitude * Mathf.Sin(this.Omega * Time.time) * Time.deltaTime), 0.0f);
			//transform.Translate(0.0f, this.Amplitude * Mathf.Sin(2 * Mathf.PI * this.Frequency * Time.time), 0.0f);
			this.Currenttime += Time.deltaTime;

		//引き上げ時間を満たした場合_または_エサが食べられた場合
        }else if(this.Outtime < this.Currenttime || this.ItemObject == null){
            //エサギミックを上昇させる
            transform.Translate(0.0f, -this.Fallspeed * Time.deltaTime, 0.0f);

            if(transform.position.y > 15.0f){
                //エサギミックを破壊する
                Destroy(gameObject);
                //経過時間を0に戻す
                this.Currenttime = 0.0f;
            }

		//playerがヒットした時
        }else if(this.isPlayerHit == true){
            //エサギミックを大きく上下に揺らす
            transform.Translate(0.0f, (this.HitAmplitude * Mathf.Sin(this.HitOmega * Time.time) * Time.deltaTime), 0.0f);
			this.Currenttime += Time.deltaTime;
        }
    }

    //判定に衝突した
    void OnTriggerEnter2D (Collider2D other){
        //playerが衝突した場合
        if(other.gameObject.tag == "player"){
			isPlayerHit = true;
		}
    }

    //判定から離れた
    void OnTriggerExit2D (Collider2D other){
        //エサが針から離れた時
        if(other.gameObject.tag == "cake" || other.gameObject.tag == "burger" || other.gameObject.tag == "ebi" || other.gameObject.tag == "noodle" || other.gameObject.tag == "onigiri" || other.gameObject.tag == "syokupan"){
            this.ItemObject = other.gameObject;
		}
    }
}