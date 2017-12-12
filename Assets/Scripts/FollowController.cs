using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour {

    public GameObject followedObject;
    private Vector3 velocity = Vector3.zero;

    public float smoothTime = 0.3F;
    private Vector3 target;



	// Use this for initialization
	void Start () {
        followedObject = GameObject.Find("TheBat");
        target.x = 0;
        target.y = 0;
        target.z = -10;
	}
	
	// Update is called once per frame
	void Update () {

        if (followedObject){
            target.x = followedObject.transform.position.x;
            target.y = followedObject.transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        }
	}
}
