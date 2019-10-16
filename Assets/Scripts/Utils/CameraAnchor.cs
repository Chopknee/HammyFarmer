using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour {

    public Transform followObject;

    public float accelerationMultiplier = 3;
    public float deceleratiion = 0.85f;
    float dirVel = 0;
    float direction = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position = followObject.position;

        dirVel += (Input.GetAxis("Mouse X") * accelerationMultiplier) * Time.deltaTime;
        dirVel *= deceleratiion;
        direction += dirVel;

        transform.rotation = Quaternion.Euler(0, direction, 0);

    }
}
