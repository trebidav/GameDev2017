using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {


    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Jump () {
        anim.SetTrigger("Jump");
    }
    void Stand() {
        anim.SetTrigger("Stand");
    }
}
