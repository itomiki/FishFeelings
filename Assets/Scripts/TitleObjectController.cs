using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleObjectController : MonoBehaviour{
    
    //振幅
    private float Amplitude;
    //周波数
    private float Omega;

    // Start is called before the first frame update
    void Start(){

        //振幅の大きさを決める
        int amp = Random.Range(10, 50);
		this.Amplitude = amp / 100.0f;

        //周波数の値を決める
        int omg = Random.Range(5, 10);
		this.Omega = omg / 10.0f;
    }

    // Update is called once per frame
    void Update(){

		//エサギミックを上下に揺らす
		transform.Translate(0.0f, (this.Amplitude * Mathf.Sin(this.Omega * Time.time) * Time.deltaTime), 0.0f);
        
    }
}
