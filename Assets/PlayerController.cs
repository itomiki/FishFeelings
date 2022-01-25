using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    //移動させるコンポーネントを入れる
    private Rigidbody2D myRigidbody;
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //ScoreControllerスクリプトを呼び出すために使用
    private GameObject ScoreControllerObject;
    private ScoreController ScoreController;

    //移動量
    private float velocity = 3.0f;
	//Playerの向きを指定（1 == 左向き, -1 == 右向き）
	private int Key = 0;

    //ajiのオブジェクト //ajiのAnimatorを取得するため
    private GameObject aji;
    //エサのオブジェクト
    private GameObject ItemObject;
    //エサに当たったかどうか（true == 当たった, false == 当たってない)
    private bool isItem = false;

    //エサへの攻撃方法 = 画面スワイプ
    //エサのHP
    private float esaHP;
    //エサを食べた時に獲得するポイント
    private float esaPoint;
    //スワイプ開始位置
    private Vector2 startSwipePos;
    //スワイプ終了位置
    private Vector2 endSwipePos;
    //スワイプの距離
    private float swipeLength;

    // Start is called before the first frame update
    void Start(){
        //Rigidbody2Dコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();
        
        //ajiオブジェクトの実体を検索
        this.aji = GameObject.Find("aji");
        //Animatorコンポーネントを取得
        this.myAnimator = this.aji.GetComponent<Animator>();

        //ScoreControllerオブジェクトの実体を検索
        this.ScoreControllerObject = GameObject.Find("ScoreController");
        //ScoreControllerを取得
        this.ScoreController = this.ScoreControllerObject.GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update(){
        //エサへの衝突有無
        switch (isItem){
            //エサに当たってない時
            case false:
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
					transform.localScale = new Vector3(Key, 1, 1);
				}
                break;

            //エサに当たった時
            case true:
                //playerをエサに固定する
                if(this.Key == 1){
					this.transform.position = ItemObject.transform.position + new Vector3(1.0f, 0.0f, 0.0f);

                }else if(this.Key == -1){
                    if(this.ItemObject.tag == "noodle"){
						this.transform.position = ItemObject.transform.position + new Vector3(-1.0f, 0.0f, 0.0f);

                    }else if(this.ItemObject.tag == "onigiri"){
						this.transform.position = ItemObject.transform.position + new Vector3(-1.25f, 0.0f, 0.0f);

                    }else{
						this.transform.position = ItemObject.transform.position + new Vector3(-1.5f, 0.0f, 0.0f);
                    }
                }

                //エサへの攻撃
                if(Input.GetMouseButtonDown(0)){
                    //スワイプ開始位置を取得
                    this.startSwipePos = Input.mousePosition;
                    //Debug.Log("startSwipePos" + this.startSwipePos);

                }else if(Input.GetMouseButtonUp(0)){
                    //スワイプ終了位置を取得
                    this.endSwipePos = Input.mousePosition;
                    //Debug.Log("endSwipePos" + this.endSwipePos);

                    //スワイプの距離を計算  1スワイプ目安 = 800~1200
                    this.swipeLength = Mathf.Pow((Mathf.Pow((this.endSwipePos.x - this.startSwipePos.x), 2.0f) + Mathf.Pow((this.endSwipePos.y - this.startSwipePos.y), 2.0f)), 0.5f);
                    //Debug.Log("swipeLength" + this.swipeLength);

                    //エサにダメージを与える
                    this.esaHP -= this.swipeLength;
                    //esaHPが0以下の時
                    if(this.esaHP <= 0){
                        //ScoreControllerのScoreAdd関数を呼び出す
                        this.ScoreController.ScoreAdd(this.esaPoint);
                        //エサを破壊
                        Destroy(this.ItemObject);
                        this.isItem = false;
                    }
                }
                break;
        }
        //playerが画面上部へ移動した時
        if(transform.position.y >= 8.0f){
            //GameOverを表示

            //playerを破壊
            Destroy(gameObject);
        }
    }

    //判定に衝突した
    void OnTriggerEnter2D (Collider2D other){
        //エサに当たった時
        if(other.gameObject.tag == "cake" || other.gameObject.tag == "burger" || other.gameObject.tag == "ebi" || other.gameObject.tag == "noodle" || other.gameObject.tag == "onigiri" || other.gameObject.tag == "syokupan"){
            //エサのRigidbody2Dをoff
            other.GetComponent<Rigidbody2D>().simulated = false;
			isItem = true;
			this.ItemObject = other.gameObject;

            //衝突したエサの種類によって
            switch(other.gameObject.tag){
                //cakeやburgerに当たった時
                case "cake":
                case "burger":
                    esaHP = 6000.0f;
                    esaPoint = 250.0f;
                    break;

                //ebiやnoodleに当たった時
                case "ebi":
                case "noodle":
                    esaHP = 3000.0f;
                    esaPoint = 100.0f;
                    break;

                //onigiriやsyokupanに当たった時
                case "onigiri":
                case "syokupan":
                    esaHP = 1000.0f;
                    esaPoint = 25.0f;
                    break;
            }
            /*　if文のパターン
            if(other.gameObject.tag == "cake" || other.gameObject.tag == "burger"){
                esaHP = 20.0f;
            }else if(other.gameObject.tag == "ebi" || other.gameObject.tag == "noodle"){
                esaHP = 15.0f;
            }else if(other.gameObject.tag == "onigiri" || other.gameObject.tag == "syokupan"){
                esaHP = 10.0f;
            }
            */
        }
    }
}
