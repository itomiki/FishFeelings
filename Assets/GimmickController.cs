using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickController : MonoBehaviour{

    //振幅
    private float Amplitude;
    //周波数
    private float Omega;
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

    // Start is called before the first frame update
    void Start(){
        //振幅の大きさを決める
        int amp = Random.Range(10, 81);
        //int amp = Random.Range(5, 31);
		this.Amplitude = amp / 100.0f;
		//this.Amplitude = amp / 1000.0f;
		Debug.Log("Amplitude " + this.Amplitude);

        //周波数の値を決める
        int omg = Random.Range(5, 10);
		this.Omega = omg / 10.0f;
		Debug.Log("Omega" + this.Omega);

        //int frq = Random.Range(5, 10);
		//this.Frequency = frq / 10.0f;
		//Debug.Log("Frequency " + this.Frequency);

        //エサギミックの引き上げ時間を決める
        int ott = Random.Range(16, 21);
        this.Outtime = ott * 1.0f;
		Debug.Log("OutTime " + this.Outtime);
    }

    // Update is called once per frame
    void Update(){
        //現在の落下距離が落下距離（目的地）以下の場合
        if(this.Currentdistance <= this.Falldistance){
            //エサギミックを落下させる
            transform.Translate(0.0f, this.Fallspeed * Time.deltaTime, 0.0f);
            this.Currentdistance += (-this.Fallspeed * Time.deltaTime);

		//目的地到達後かつ引き上げ時間に満たない場合
        }else if(this.Currentdistance > this.Falldistance && this.Outtime >= this.Currenttime){
			//エサギミックを上下に揺らす
			transform.Translate(0.0f, (this.Amplitude * Mathf.Sin(this.Omega * Time.time) * Time.deltaTime), 0.0f);
			//transform.Translate(0.0f, this.Amplitude * Mathf.Sin(2 * Mathf.PI * this.Frequency * Time.time), 0.0f);
			this.Currenttime += Time.deltaTime;

        //引き上げ時間を満たした場合
        }else if(this.Outtime < this.Currenttime){
            //エサギミックを上昇させる
            transform.Translate(0.0f, -this.Fallspeed * Time.deltaTime, 0.0f);

            if(transform.position.y > 15.0f){
                //エサギミックを破壊する
                Destroy(gameObject);
            }
        }
    }
}