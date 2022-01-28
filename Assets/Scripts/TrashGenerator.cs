using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour{

    //使用するPrefabを入れる
    public GameObject GlassBrownPrefab;
    public GameObject GlassGreenPrefab;
    public GameObject PetbottlePrefab;
    public GameObject SekkenBottlePrefab;

    //上記Prefabを代入するために使用
    private GameObject TrashType;
    //インスタンスを作成するために使用
    private GameObject TrashObject;

    //TrashObjectを配置するポイント（x座標）
    private float LeftPos_x = -4.0f;
    private float CenterPos_x = 0.0f;
    private float RightPos_x = 4.0f;
    //TrashObjectのx座標
    private float MakePos_x;
    //TrashObjectのy座標
    private float Pos_y = 7.0f;

    //TrashObject第1便までの時間
    private float FirstTime;
    private bool FirstTerm = false;
    //TrashObject第2便までの時間
    private float SecondTime;
    private bool SecondTerm = false;
    //TrashObject第3便までの時間
    private float ThirdTime;
    private bool ThirdTerm = false;

    //現在の経過時間
    private float CurrentTime = 0.0f;

    // Start is called before the first frame update
    void Start(){
        //FirstTimeおよびSecondTimeおよびThirdTimeをランダムで決める
        int firsttime = Random.Range(15, 21);
        this.FirstTime = firsttime * 1.0f;

        int secondtime = Random.Range(35, 46);
        this.SecondTime = secondtime  * 1.0f;

        int thirdtime = Random.Range(55, 61);
        this.ThirdTime = thirdtime * 1.0f;
        
    }

    // Update is called once per frame
    void Update(){
        //TrashObject第1便
        if(this.CurrentTime >= this.FirstTime && this.FirstTerm == false){
            this.FirstTerm = true;
            TrashGenerate();
            Debug.Log("TrashGenerator FirstTerm OK");
            Debug.Log("FirstTime = " + this.FirstTime);

		//TrashObject第2便
        }else if(this.CurrentTime >= this.SecondTime && this.SecondTerm == false){
            this.SecondTerm = true;
            TrashGenerate();
            Debug.Log("TrashGenerator SecondTerm OK");
            Debug.Log("SecondTime = " + this.SecondTime);

		//TrashObject第3便
        }else if(this.CurrentTime >= this.ThirdTime && this.ThirdTerm == false){
            this.ThirdTerm = true;
            TrashGenerate();
            Debug.Log("TrashGenerator ThirdTerm OK");
            Debug.Log("ThirdTime = " + this.ThirdTime);
        }

        //経過時間を更新
        this.CurrentTime += Time.deltaTime;
    }

    //TrashObjectを生成する関数
    void TrashGenerate(){
        //X座標のオフセットを決める
        int offset_x = Random.Range(0, 20);

        //プラスかマイナスを決める
		int plusminus = Random.Range(0, 2);
		float i;
		if(plusminus == 0){
			i = -1.0f;
		}else{
			i = 1.0f;
		}

        //TrashObjectを配置するポイント（x座標）をランダムで決める
        int num1 = Random.Range(0, 3);
        switch(num1){
            case 0:
                this.MakePos_x = this.LeftPos_x + (offset_x / 10.0f) * i;
                break;

            case 1:
                this.MakePos_x = this.CenterPos_x + (offset_x / 10.0f) * i;
                break;

            case 2:
                this.MakePos_x = this.RightPos_x + (offset_x / 10.0f) * i;
                break;
        }

        //TrashTypeを4タイプからランダムで決める
        int num2 = Random.Range(0, 4);
        switch(num2){
            case 0:
                this.TrashType = this.GlassBrownPrefab;
                this.TrashObject = Instantiate(this.TrashType);
                break;

            case 1:
                this.TrashType = this.GlassGreenPrefab;
                this.TrashObject = Instantiate(this.TrashType);
                break;

            case 2:
                this.TrashType = this.PetbottlePrefab;
                this.TrashObject = Instantiate(this.TrashType);
                break;

            case 3:
                this.TrashType = this.SekkenBottlePrefab;
                this.TrashObject = Instantiate(this.TrashType);
                break;
        }

        //TrashObjectを生成する
        this.TrashObject.transform.position = new Vector2(this.MakePos_x, this.Pos_y);
    }
}