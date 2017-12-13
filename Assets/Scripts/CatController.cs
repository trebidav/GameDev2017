using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    public bool jumping;
    public bool moving;
    public bool crawling;

    public Vector3 moveTarget;
    public Vector3 jumpTarget;

    public GameObject JumpHelper;

    public Animator anim;

    public AnimationCurve jumpCurve;
    public AnimationCurve jumpSpeed;

    public float jumpLength;
    public float jumpProgress;

    public float jumpLengthCurrent;
    public float jumpAnimSpeed;

    public Vector3 jumpHeading;
    public Vector3 jumpCurrentY;

    private bool debugBool = false;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        jumpProgress = 0;
    }
    
    // Update is called once per frame
    void Update () {
        if (!jumping && debugBool){
            debugBool = false;
            moveTarget.y = transform.position.y;
        }

        if ((moveTarget - transform.position).magnitude < 0.1f)
        {
            moving = false;
            anim.SetTrigger("Stay");
        }

        if (moving && !debugBool)
        {
            // look towards the object
            if (moveTarget.x - transform.position.x < 0 && transform.localScale.x > 0 || moveTarget.x - transform.position.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            // move to object
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * 2f);
            anim.SetTrigger("Walk");

        }

        if (jumping && !moving || jumping && moving && debugBool){
            
            //change direction
            if (jumpTarget.x - transform.position.x < 0 && transform.localScale.x > 0 || jumpTarget.x - transform.position.x > 0 && transform.localScale.x < 0){
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            //get progress and length for further purposes (animation + jump
            jumpLengthCurrent = (jumpTarget - JumpHelper.transform.position).magnitude;
            jumpProgress = (jumpLength - jumpLengthCurrent) / jumpLength;


            //ANIMATION
            if ((jumpTarget - JumpHelper.transform.position).magnitude < 0.2f){
                jumping = false;

                transform.parent = null;
                Destroy(JumpHelper);

                anim.ResetTrigger("JumpDown");
                anim.ResetTrigger("JumpUp");
                anim.SetTrigger("Stay");

            }
            else {
                if (jumpProgress < 0.5f)
                {
                    anim.SetTrigger("JumpUp");

                }
                else if (jumpProgress >= 0.5f)
                {
                    anim.SetTrigger("JumpDown");
                }
            }


            //JUMP

            jumpAnimSpeed = (3.0f + jumpSpeed.Evaluate(jumpProgress) * 2f);
            JumpHelper.transform.position = Vector3.MoveTowards(JumpHelper.transform.position, jumpTarget, Time.deltaTime * jumpAnimSpeed);
            jumpCurrentY.y = jumpCurve.Evaluate(jumpProgress);

            transform.position = JumpHelper.transform.position + jumpCurrentY;



        }
        if (crawling){

            anim.SetTrigger("Crawl");
            if (!moving) {
                crawling = false;
                anim.ResetTrigger("Crawl");
                anim.SetTrigger("Stay");
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
            }
        }

    }

    void Jump(Transform [] points){
        
        //create jump helper and set it's position to source (a)
        JumpHelper = new GameObject("JumpHelper");
        JumpHelper.transform.position = points[0].position; 

        //set target to b position
        jumpTarget = points[1].position;

        //first move to the starting point (a)
        moveTarget = points[0].position;

        moving = true;
        jumping = true;

        //set cat as a child of JumpHelper
        transform.parent = JumpHelper.transform;

        jumpHeading = points[1].position - points[0].position;
        jumpLength = jumpHeading.magnitude;
        jumpProgress = 0f;

    }

    void Crawl(Vector3 target){

        moveTarget = target;
        crawling = true;
        moving = true;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y/2, transform.localScale.z);

    }

    void Move(Vector3 target){

        moving = true;
        moveTarget = target;


        debugBool = true;

    }

    public void Stay()
    {
        moveTarget = transform.position;
        moving = false;
    }

}
