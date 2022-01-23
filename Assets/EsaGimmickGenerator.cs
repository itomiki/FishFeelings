using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsaGimmickGenerator : MonoBehaviour{

    //使用するPrefabを入れる
    public GameObject CakeGimmickPrefab;
    public GameObject BurgerGimmickPrefab;
    public GameObject NoodleGimmickPrefab;
    public GameObject EbiGimmickPrefab;
    public GameObject OnigiriGimmickPrefab;
    public GameObject SyokupanGimmickPrefab;

    //エサPrefabを代入するために使用
    private GameObject EsaType;
    //エサの出現割合
    private int EsaRate;
    //どちらのエサを出現させるか
    private int orEsaType;
    //出現するエサのグレードを指定
    private int EsaGrade = 0;

    //EsaGimmickを配置する4ポイントを設定する（X軸）
    private int[] EsaGimmickPos = { 0, 0, 0, 0 };   //0 == EsaGimmickあり, 1 == EsaGimmickなし
    private float LeftMaxPos_x = -6.0f;     //EsaGimmickPos[0] --- ポイント0 
    private float LeftMiddlePos_x = -2.0f;  //EsaGimmickPos[1] --- ポイント1
    private float RightMaxPos_x = 6.0f;     //EsaGimmickPos[2] --- ポイント2
    private float RightMiddlePos_x = 2.0f;  //EsaGimmickPos[3] --- ポイント3
    //エサオブジェクト
    private GameObject esa0_object; //ポイント0で出現するオブジェクト
    private GameObject esa1_object; //ポイント1で出現するオブジェクト
    private GameObject esa2_object; //ポイント2で出現するオブジェクト
    private GameObject esa3_object; //ポイント3で出現するオブジェクト
    //次のエサが出現するまでの時間（各々のポイントにおいて）
    private float nextEsaTime0 = 0.0f;  //ポイント0
    private float nextEsaTime1 = 0.0f;  //ポイント1
    private float nextEsaTime2 = 0.0f;  //ポイント2
    private float nextEsaTime3 = 0.0f;  //ポイント3

    //EsaGimmickを配置する高さの最大値および最小値（Y軸）
    private int MaximumPos_y = 12;
    private int MinimumPos_y = 9;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

        //ポイント0にEsaGimmickがない場合（ポイントについては上記コメント参照）
        if(this.EsaGimmickPos[0] == 0){
            //EsaGimmickPosCtrl関数を呼び出す
            EsaGimmickPosCtrl(this.LeftMaxPos_x);
            this.EsaGimmickPos[0] = 1;
        }

        //ポイント1にEsaGimmickがない場合
        if(this.EsaGimmickPos[1] == 0){
            EsaGimmickPosCtrl(this.LeftMiddlePos_x);
            this.EsaGimmickPos[1] = 1;
        }

        //ポイント2にEsaGimmickがない場合
        if(this.EsaGimmickPos[2] == 0){
            EsaGimmickPosCtrl(this.RightMiddlePos_x);
            this.EsaGimmickPos[2] = 1;
        }

        //ポイント3にEsaGimmickがない場合
        if(this.EsaGimmickPos[3] == 0){
			EsaGimmickPosCtrl(this.RightMaxPos_x);
            this.EsaGimmickPos[3] = 1;
        }

        //ポイント0のエサギミックが破壊された場合
        if(this.esa0_object == null){
            
            this.nextEsaTime0 += Time.deltaTime;
            //破壊されてから3秒後
            if(this.nextEsaTime0 >= 3.0f){
				this.EsaGimmickPos[0] = 0;
                this.nextEsaTime0 = 0.0f;
            }
        }
        //ポイント1のエサギミックが破壊された場合
        if(this.esa1_object == null){

            this.nextEsaTime1 += Time.deltaTime;
            if(this.nextEsaTime1 >= 3.0f){
				this.EsaGimmickPos[1] = 0;
                this.nextEsaTime1 = 0.0f;
            }
        }
        //ポイント2のエサギミックが破壊された場合
        if(this.esa2_object == null){
            this.nextEsaTime2 += Time.deltaTime;
            if(this.nextEsaTime2 >= 3.0f){
				this.EsaGimmickPos[2] = 0;
                this.nextEsaTime2 = 0.0f;
			}
        }
        //ポイント3のエサギミックが破壊された場合
        if(this.esa3_object == null){
            this.nextEsaTime3 += Time.deltaTime;
            if(this.nextEsaTime3 >= 3.0f){
				this.EsaGimmickPos[3] = 0;
                this.nextEsaTime3 = 0.0f;
			}
        }
    }

    public void EsaGimmickPosCtrl(float MakePos_x){
        //EsaGimmickを配置するY座標を決める
        int MakePos_y = Random.Range(this.MinimumPos_y, this.MaximumPos_y);
        //Y座標のオフセットを決める
        int offset_y = Random.Range(0, 10);
        //X座標のオフセットを決める
        int offset_x = Random.Range(0, 10);

        //プラスかマイナスを決める
		int plusminus = Random.Range(0, 2);
		float i;
		if(plusminus == 0){
			i = -1.0f;
		}else{
			i = 1.0f;
		}

        //EsaGradeによって場合分け（EsaGradeはScoreControllerから指定）
        //EsaGrade == 0 6割_おにぎりor食パン　4割_ヌードルorエビ　0割_ケーキorバーガー
        //EsaGrade == 1 2割_おにぎりor食パン　5割_ヌードルorエビ　3割_ケーキorバーガー
        //EsaGrade == 2 0割_おにぎりor食パン　6割_ヌードルorエビ　4割_ケーキorバーガー
        switch(EsaGrade){
            case 0:
                this.EsaRate = Random.Range(1, 11);
                //おにぎりまたは食パンのどちらかに決める
                if(this.EsaRate <= 6){
                    this.orEsaType = Random.Range(1, 3);
                    if(this.orEsaType == 1){
                        this.EsaType = OnigiriGimmickPrefab;
                    }else{
                        this.EsaType = SyokupanGimmickPrefab;
                    }

				//ヌードルまたはエビのどちらかに決める
                }else{
                    this.orEsaType = Random.Range(1, 3);
                    if(this.orEsaType == 1){
                        this.EsaType = NoodleGimmickPrefab;
                    }else{
                        this.EsaType = EbiGimmickPrefab;
                    }
                }
                break;

            case 1:
                this.EsaRate = Random.Range(1, 11);
                //おにぎりまたは食パンのどちらかに決める
                if(this.EsaRate <= 2){
                    this.orEsaType = Random.Range(1, 3);
                    if(this.orEsaType == 1){
                        this.EsaType = OnigiriGimmickPrefab;
                    }else{
                        this.EsaType = SyokupanGimmickPrefab;
                    }

				//ヌードルまたはエビのどちらかに決める
				}else if(3 <= this.EsaRate && this.EsaRate <= 7){
                    this.orEsaType = Random.Range(1, 3);
                    if(this.orEsaType == 1){
                        this.EsaType = NoodleGimmickPrefab;
                    }else{
                        this.EsaType = EbiGimmickPrefab;
                    }

				//ケーキまたはバーガーのどちらかに決める
                }else{
                    this.orEsaType = Random.Range(1, 3);
                    if(this.orEsaType == 1){
                        this.EsaType = CakeGimmickPrefab;
                    }else{
                        this.EsaType = BurgerGimmickPrefab;
                    }
                }
                break;

            case 2:
                this.EsaRate = Random.Range(1, 11);
				//ヌードルまたはエビのどちらかに決める
                if(this.EsaRate <= 6){
                    this.orEsaType = Random.Range(1, 3);
                    if(this.orEsaType == 1){
                        this.EsaType = NoodleGimmickPrefab;
                    }else{
                        this.EsaType = EbiGimmickPrefab;
                    }

				//ケーキまたはバーガーのどちらかに決める
                }else{
                    this.orEsaType = Random.Range(1, 3);
                    if(this.orEsaType == 1){
                        this.EsaType = CakeGimmickPrefab;
                    }else{
                        this.EsaType = BurgerGimmickPrefab;
                    }
                }
                break;
        }

        //EsaGimmickを生成する
        if(MakePos_x == LeftMaxPos_x){
			this.esa0_object = Instantiate(this.EsaType);
			this.esa0_object.transform.position = new Vector2(MakePos_x + (offset_x / 10.0f) * i, MakePos_y + (offset_y / 10.0f));
            Debug.Log(this.EsaGrade);
            //Debug.Log(cake.transform.position.x + ", " + cake.transform.position.y);

        }else if(MakePos_x == LeftMiddlePos_x){
			this.esa1_object = Instantiate(this.EsaType);
			this.esa1_object.transform.position = new Vector2(MakePos_x + (offset_x / 10.0f) * i, MakePos_y + (offset_y / 10.0f));
            Debug.Log(this.EsaGrade);
            //Debug.Log(cake.transform.position.x + ", " + cake.transform.position.y);

        }else if(MakePos_x == RightMiddlePos_x){
			this.esa2_object = Instantiate(this.EsaType);
			this.esa2_object.transform.position = new Vector2(MakePos_x + (offset_x / 10.0f) * i, MakePos_y + (offset_y / 10.0f));
            Debug.Log(this.EsaGrade);
            //Debug.Log(cake.transform.position.x + ", " + cake.transform.position.y);

        }else if(MakePos_x == RightMaxPos_x){
			this.esa3_object = Instantiate(this.EsaType);
			this.esa3_object.transform.position = new Vector2(MakePos_x + (offset_x / 10.0f) * i, MakePos_y + (offset_y / 10.0f));
            Debug.Log(this.EsaGrade);
            //Debug.Log(cake.transform.position.x + ", " + cake.transform.position.y);
        }
    }

    //EsaGradeを上げる（ScoreControllerから呼び出す）
    public void EsaGradeCntl(){
        this.EsaGrade += 1;
    }
}
