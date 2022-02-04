using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour{

    //---------------
    //WebGL_Version
    //---------------
    ///*
    //Tutorial1オブジェクトを入れる
    private GameObject tutorial1_Object;
    //Tutorial2オブジェクトを入れる
    private GameObject tutorial2_Object;
    //---------------
    //*/

    //---------------
    //Android_Version
    //---------------
    /*
    //Tutorial1_Androidオブジェクトを入れる
    private GameObject tutorial1_Android_Object;
    //Tutorial2_Androidオブジェクトを入れる
    private GameObject tutorial2_Android_Object;
    //---------------
    */

    //GoNextButtonを入れる
    private GameObject goNextButtonObject;
    //GoBackButtonを入れる
    private GameObject goBackButtonObject;

    // Start is called before the first frame update
    void Start(){
		//---------------
		//WebGL_Version
		//---------------
        ///*
        //Tutorial1オブジェクトの実体を検索する
        tutorial1_Object = GameObject.Find("Tutorial1");

        //Tutorial2オブジェクトの実体を検索する
        tutorial2_Object = GameObject.Find("Tutorial2");
        //Tutorial2オブジェクトを非表示にする
        tutorial2_Object.SetActive(false);
		//---------------
        //*/

		//---------------
		//Android_Version
		//---------------
        /*
        //Tutorial1_Androidオブジェクトの実体を検索する
        tutorial1_Android_Object = GameObject.Find("Tutorial1_Android");

        //Tutorial2_Androidオブジェクトの実体を検索する
        tutorial2_Android_Object = GameObject.Find("Tutorial2_Android");
        //Tutorial2_Androidオブジェクトを非表示にする
        tutorial2_Android_Object.SetActive(false);
		//---------------
        */


        //GoNextButtonの実体を検索する
        goNextButtonObject = GameObject.Find("GoNextButton");

        //GoBackButtonの実体を検索する
        goBackButtonObject = GameObject.Find("GoBackButton");
        //GoBackButtonを非表示にする
        goBackButtonObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    //GoTitleButtonが押された時に呼び出される
    public void GoTitleButtonDown(){
        GetComponent<AudioSource>().Play();
        //TitleSceneLoad関数を呼び出す
        Invoke("TitleSceneLoad", 1.0f);
    }
    //GoStartButtonが押された時に呼び出される
    public void GoStartButtonDown(){
        GetComponent<AudioSource>().Play();
        //CountDownSceneLoad関数を呼び出す
        Invoke("CountDownSceneLoad", 1.0f);
    }
    //GoNextButtonが押された時に呼び出される
    public void GoNextButtonDown(){
        GetComponent<AudioSource>().Play();
        //NextButtonSetActive関数を呼び出す
        Invoke("NextButtonSetActive", 0.2f);
    }
    //GoBackButtonが押された時に呼び出される
    public void GoBackButtonDown(){
        GetComponent<AudioSource>().Play();
        //BackButtonSetActive関数を呼び出す
        Invoke("BackButtonSetActive", 0.2f);
    }


    //TitleSceneに遷移する
    public void TitleSceneLoad(){
        SceneManager.LoadScene("TitleScene");
    }
    //CountDownSceneに遷移する
    public void CountDownSceneLoad(){
        SceneManager.LoadScene("CountDownScene");
    }


    //Tutorial1からTutorial2に遷移する
    public void NextButtonSetActive(){
        //GoNextButtonを非表示にする
        goNextButtonObject.SetActive(false);
        //GoBackButtonを表示する
        goBackButtonObject.SetActive(true);

		//---------------
		//WebGL_Version
		//---------------
        ///*
        //Tutorial1を非表示にする
        tutorial1_Object.SetActive(false);
        //Tutorial2を表示する
        tutorial2_Object.SetActive(true);
        //*/
		//---------------

		//---------------
		//Android_Version
		//---------------
        /*
        //Tutorial1_Androidを非表示にする
        tutorial1_Android_Object.SetActive(false);
        //Tutorial2_Androidを表示する
        tutorial2_Android_Object.SetActive(true);
        */
		//---------------
    }
    //Tutorial2からTutorial1に遷移する
    public void BackButtonSetActive(){
        //GoNextButtonを表示する
        goNextButtonObject.SetActive(true);
        //GoBackButtonを非表示にする
        goBackButtonObject.SetActive(false);

		//---------------
		//WebGL_Version
		//---------------
        ///*
        //Tutorial1を表示する
        tutorial1_Object.SetActive(true);
        //Tutorial2を非表示にする
        tutorial2_Object.SetActive(false);
        //*/
		//---------------

		//---------------
		//Android_Version
		//---------------
        /*
        //Tutorial1_Androidを表示する
        tutorial1_Android_Object.SetActive(true);
        //Tutorial2_Androidを非表示にする
        tutorial2_Android_Object.SetActive(false);
        */
		//---------------
    }
}
