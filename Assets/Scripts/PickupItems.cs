using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour {

    public bool carrying;
    public GameObject carriedObject;
    public float cooldown;
    public Vector3 offset;

	
	void Start () {
        carrying = false;
        cooldown = 0;
        //offset = new Vector3(0, -0.6f, 0);
	}


	
	void Update () {
        if (cooldown > 0) {
            cooldown = cooldown - Time.deltaTime;
            //Debug.Log("Cooling down: " + cooldown);
        }

        if (carrying){
            if (carriedObject) {
                carriedObject.transform.localPosition = Vector3.zero;
            }
            else {
                carrying = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Right-clicked - dropping items.");

            if (carrying){
                carrying = false;
                cooldown = 1;
    
                Rigidbody2D rb = carriedObject.GetComponent<Rigidbody2D>();
       
                rb.gravityScale = 1.0f;

                carriedObject.layer = 11;

                carriedObject.transform.parent = null;
                carriedObject = null;

            }
            else {
                Debug.Log("Nothing to drop.");
            }

        }
            
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //Debug.Log("Found: " + other.gameObject.name + ", state: carrying="+carrying+", cooldown=" + cooldown);

        if (carrying == false && cooldown <= 0 && other.gameObject.layer == 11){
            Debug.Log("Picked up: " + other.gameObject.name);
            carriedObject = other.gameObject;
            carrying = true;


            //add child
            carriedObject.transform.parent = this.transform;
            carriedObject.transform.localPosition = Vector3.zero;

            //disable gravity
            Rigidbody2D rb = carriedObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;

            carriedObject.layer = 15;

        }

    }

}
