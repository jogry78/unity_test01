using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour {

    public float move_speed = 5.0f;
    public float rot_speed = 3.0f;
    public GameObject cam;
    public NavMeshAgent agent;
    // Use this for initialization
    void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        GameObject goal = GameObject.Find("Goal");
        //agent.destination = goal.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
        /*
        if (Input.GetKey("w"))
        {
            gameObject.transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            gameObject.transform.Translate(Vector3.back * move_speed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            gameObject.transform.Translate(Vector3.left * move_speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            gameObject.transform.Translate(Vector3.right * move_speed * Time.deltaTime);
        }

        float rotY = Input.GetAxis("Mouse X") * rot_speed;
        float rotX = Input.GetAxis("Mouse Y") * rot_speed;

        transform.localRotation *= Quaternion.Euler(0, rotY, 0);

        //Camera cam = (Camera)gameObject.GetComponent("Main Camera");
        cam.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);

    */
    }
}
