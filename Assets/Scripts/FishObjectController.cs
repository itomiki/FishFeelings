using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishObjectController : MonoBehaviour{

    //
    private Rigidbody2D myRigidbody;
    //
    private Animator myAnimator;
    
    //ajiのオブジェクト //ajiのAnimatorを取得するため
    private GameObject aji;

    //
    private bool isUp = false;

    //
    private float Upvelocity = 0.75f;
    private float Downvelocity = -0.5f;
    
    // Start is called before the first frame update
    void Start(){
        this.aji = GameObject.Find("aji");
        this.myAnimator = this.aji.GetComponent<Animator>();
		myAnimator.SetBool("Swim", false);

        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
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
        Invoke("loadScene", 1f);
    }

    //CountDownSceneに遷移する関数
    public void loadScene(){
        //CountDownSceneに遷移
        SceneManager.LoadScene("CountDownScene");
    }
}
