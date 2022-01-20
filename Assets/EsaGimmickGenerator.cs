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

    //EsaGimmickを配置する4ポイントを設定する（X軸）
    private int[] EsaGimmickPos = { 0, 0, 0, 0 };   //0 == EsaGimmickあり, 1 == EsaGimmickなし
    private float LeftMaxPos_x = -6.0f;     //EsaGimmickPos[0] --- ポイント0 
    private float LeftMiddlePos_x = -2.0f;  //EsaGimmickPos[1] --- ポイント1
    private float RightMaxPos_x = 6.0f;     //EsaGimmickPos[2] --- ポイント2
    private float RightMiddlePos_x = 2.0f;  //EsaGimmickPos[3] --- ポイント3

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

        //EsaGimmickを生成する
        GameObject cake = Instantiate(CakeGimmickPrefab);
		cake.transform.position = new Vector2(MakePos_x + (offset_x / 10.0f) * i, MakePos_y + (offset_y / 10.0f));
		this.EsaGimmickPos[0] = 1;
		Debug.Log(cake.transform.position.x + ", " + cake.transform.position.y);
    }
}
