using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishObjectController : MonoBehaviour{

    //Rigidbody2Dコンポーネントを入れる
    private Rigidbody2D myRigidbody;
    //Animatorコンポーネントを入れる
    private Animator myAnimator;
    
    //ajiのオブジェクト //ajiのAnimatorを取得するため
    private GameObject aji;

    //上に泳ぐかどうか（true == 泳ぐ, false == 泳がない）
    private bool isUp = false;

    //上昇スピードおよび下降スピード
    private float Upvelocity = 0.75f;
    private float Downvelocity = -0.5f;
    
    // Start is called before the first frame update
    void Start(){
        //ajiオブジェクトの実体を検索
        this.aji = GameObject.Find("aji");
        //Animatorコンポーネントの実体を取得
        this.myAnimator = this.aji.GetComponent<Animator>();
        //Swimをfalseにする（Animator）
        myAnimator.SetBool("Swim", false);
        //Rigidbody2Dコンポーネントを取得する
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        //isUpの場合
        if(isUp){
            //上昇する
            myRigidbody.velocity = new Vector2(0.0f, Upvelocity);
        }

		if(transform.position.y < -0.5f){
			isUp = true;
            myAnimator.SetBool("Swim", true);
        }

        if(transform.position.y > 1.75f){
            //下降する
            myRigidbody.velocity = new Vector2(0.0f, Downvelocity);
            isUp = false;
            myAnimator.SetBool("Swim", false);
        }
    }

    //StartButtonが押された時に呼び出される
    public void GetMyStartButtonDown(){
        GetComponent<AudioSource>().Play();
        //1秒後にloadScene関数を呼び出す
        Invoke("CountDownSceneLoad", 1f);
    }
    //
    public void GetMyTutorialButtonDown(){
        GetComponent<AudioSource>().Play();
        //1秒後にloadScene関数を呼び出す
        Invoke("TutorialSceneLoad", 1f);
    }


    //CountDownSceneに遷移する関数
    public void CountDownSceneLoad(){
        //CountDownSceneに遷移
        SceneManager.LoadScene("CountDownScene");
    }
    //TutorialSceneに遷移する関数
    public void TutorialSceneLoad(){
        //TutorialSceneに遷移
        SceneManager.LoadScene("TutorialScene");
    }
}
