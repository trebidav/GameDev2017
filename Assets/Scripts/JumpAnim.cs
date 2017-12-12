using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class JumpAnim : MonoBehaviour {

    public AnimationCurve curveUp;
    public AnimationCurve curveDown;
    public AnimationCurve jumpSpeed;
    public GameObject obj;
    public Transform target;
    public Vector3 current;
    public float length;
    public float progress;
    public Vector3 heading;
    //public float gravity;
    public float add;
    public float t;
    public float fast;
    public GameObject helper;

    public float currentlength;

    public Vector3 targetlocal;
    public bool directionUpwards;

    // Use this for initialization
    void Start() {

        // create helper for jumping
        helper = new GameObject("jumphelper");
        helper.transform.parent = this.transform;

        // find cat 
        obj = GameObject.Find("cat_stand");

        //find B
        target = transform.parent.Find("B");

        //set cat's parent the jump helper
        obj.transform.parent = helper.transform;

        //reset relative localposition
        helper.transform.localPosition = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;

        // set heading vector and compute length of path
        heading = target.position - transform.position;
        length = heading.magnitude;

        // reset
        progress = 0f;
        add = 0;
        current = Vector3.zero;
        t = 1.0f;

        targetlocal = helper.transform.InverseTransformPoint(target.position);





	}

    // Update is called once per frame
    void Update()
    {

        if (t > 0f)
            // debug - wait t seconds
            t -= Time.deltaTime;
        else {

            fast = (3.0f + jumpSpeed.Evaluate(progress) * 2.5f);

            currentlength = (targetlocal - helper.transform.localPosition).magnitude;
       
            progress = (length - currentlength) / length;

            helper.transform.localPosition = Vector3.MoveTowards(helper.transform.localPosition, targetlocal, Time.deltaTime * fast);

            if (heading.y > 0) {
                // jumping up
                current.y = curveUp.Evaluate(progress);
            }
            else {
                // jumpin down
                current.y = curveDown.Evaluate(progress);
            }


            obj.transform.localPosition = current;
    
        }
	}
}


