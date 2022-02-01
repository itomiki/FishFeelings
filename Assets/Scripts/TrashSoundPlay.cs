using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSoundPlay : MonoBehaviour{

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    //ゴミに接触した際の音を鳴らす（TrashControllerスクリプトから呼び出す）
    public void trashSound(){
        GetComponent<AudioSource>().Play();
    }
}
