using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoRetryController : MonoBehaviour{

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    //ボタンが押された時に呼び出される
    public void OnClick(){
        GetComponent<AudioSource>().Play();
        //1秒後にloadScene関数を呼び出す
        Invoke("loadScene", 1f);
    }

    //CountDownSceneに遷移する関数
    public void loadScene(){
		//CountDownSceneに遷移する
        SceneManager.LoadScene("CountDownScene");
    }
}
