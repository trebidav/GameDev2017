using UnityEngine;
using System.Collections.Generic;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove : MonoBehaviour{

    private Quaternion rot;
	private PolyNavAgent _agent;
	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}
    void Start(){
        rot = transform.rotation;
    }

	void Update() {

        if (Input.GetMouseButtonDown(0)){
			agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 2);
	}



	//Message from Agent
	void OnDestinationReached(){

		//do something here...
	}

	//Message from Agent
	void OnDestinationInvalid(){

		//do something here...
	}
}
