using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownFish : MonoBehaviour{

    //移動させるコンポーネントを入れる
    private Rigidbody2D myRigidbody;
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    
    //移動量
    private float velocity = 3.0f;
	//Playerの向きを指定（1 == 左向き, -1 == 右向き）
	private int Key = 0;

    //ajiのオブジェクト //ajiのAnimatorを取得するため
    private GameObject aji;



    // Start is called before the first frame update
    void Start(){

        //Rigidbody2Dコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //ajiオブジェクトの実体を検索
        this.aji = GameObject.Find("aji");
        //Animatorコンポーネントを取得
        this.myAnimator = this.aji.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){

		//移動
		if((Input.GetKey(KeyCode.W) && transform.position.y < 5.0f) || (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 5.0f)){
			this.myRigidbody.velocity = new Vector2(0.0f, this.velocity);
			//Swimアニメーション
			this.myAnimator.SetBool("Swim", true);

		}else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			this.myRigidbody.velocity = new Vector2(0.0f, -this.velocity);
			this.myAnimator.SetBool("Swim", true);

		}else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			this.myRigidbody.velocity = new Vector2(-this.velocity, 0.0f);
			this.myAnimator.SetBool("Swim", true);
			//左向き
			this.Key = 1;

		}else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			this.myRigidbody.velocity = new Vector2(this.velocity, 0.0f);
			this.myAnimator.SetBool("Swim", true);
			//右向き
			this.Key = -1;

		}else{
			this.myRigidbody.velocity = new Vector2(0.0f, 0.0f);
			//Idleアニメーション
			this.myAnimator.SetBool("Swim", false);
			//Debug.Log(this.myAnimator.GetBool("Swim"));

		}

		//Playerの向きを変える
		if(Key != 0){
			transform.localScale = new Vector3(Key, 1.0f, 1.0f);
		}
    }
}
