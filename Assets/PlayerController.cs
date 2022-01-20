using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    private Rigidbody2D myRigidbody;

    private float velocity = 3.0f;

    // Start is called before the first frame update
    void Start(){
        this.myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}
