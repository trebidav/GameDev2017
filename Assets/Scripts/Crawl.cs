using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Crawl : MonoBehaviour
{


    public GameObject sibling;
    public bool active = false;
    public GameObject obj;
    public GameObject catObject;


    void SetActiveValue(bool setval)
    {

        active = setval;
        Debug.Log("[" + this.gameObject.name + "] Set active to: " + setval);
    }

    void Start()
    {

        // FIND THE SIBLING

        if (this.gameObject.name == "B")
        {
            Debug.Log("[" + this.gameObject.name + "] Found sibling: " + transform.parent.Find("A"));
            sibling = transform.parent.Find("A").gameObject;
        }
        else if (this.gameObject.name == "A")
        {
            Debug.Log("[" + this.gameObject.name + "] Found sibling: " + transform.parent.Find("B"));
            sibling = transform.parent.Find("B").gameObject;
        }

        catObject = GameObject.Find("TheCat");

        active = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[" + this.gameObject.name + "] I've collided with: " + other.gameObject.name + ", " + other.gameObject.tag);
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Usable_crawl")
        {

            sibling.SendMessage("SetActiveValue", true);
            obj = other.gameObject;

        }

        if (other.isTrigger)
        {
            Debug.Log("[" + this.gameObject.name + "] Found trigger: " + other.gameObject.name);
            Debug.Log("Just a trigger..");
        }

        else if (other.gameObject.tag == "Cat")
        {

            Debug.Log("[" + this.gameObject.name + "] I've got the cat ready to crawl!");
            //catObject = other.gameObject;

            if (active)
            {



                catObject.SendMessage("Crawl", sibling.transform.position); //send
                active = false;
                sibling.SendMessage("PlaySound");
                sibling.SendMessage("DestroyObject");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Usable_crawl")
        {
            Debug.Log(sibling.gameObject.name + " has been deactivated.");
            sibling.SendMessage("SetActiveValue", false);
            obj = null;
        }
        else if (other.gameObject.tag == "Cat")
        {
            Debug.Log("[" + this.gameObject.name + "] The Cat left..");
            //catObject = null;
        }
    }

    public void DestroyObject()
    {
        if (obj)
        {
            obj.tag = "Usable_used";
            obj.layer = 14;
        }
    }
    public void PlaySound()
    {
        if (obj) obj.GetComponent<AudioSource>().Play();
    }
}
