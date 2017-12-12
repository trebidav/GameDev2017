using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

    public string Scene;
    public bool Switch;

	// Use this for initialization
	void Start () {
        Scene = "Tutorial";
        Switch = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Switch){
            Switch = false;
            Change();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cat"){
            Change();
        }

    }

    public void Change(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
    }
}
