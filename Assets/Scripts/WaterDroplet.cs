using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDroplet: MonoBehaviour {

    //Waits a bit before activating the water droplet modification to the farm field.
    public float collisionDelay = 2;
    public Texture2D deformationMap;
    public float mapScale = 1;
    public float mapWeight = 1;


    float del = 0;

    bool collided = false;
    bool shouldWater = false;
    GameObject field;

    [Range(-1f, 1f)]
    public float deformWeight = 1;
    [Range(-1f, 1f)]
    public float tillWeight = 1;
    [Range(-1f, 1f)]
    public float waterWeight = 1;
    public bool additiveOnly = false;

    void Update () {
        del += Time.deltaTime;
        if (del >= collisionDelay) {
            if (collided) {
                if (shouldWater) {
                    field.GetComponent<FarmFieldDeformation>().Deform(gameObject, deformationMap, 1, mapScale, mapWeight, deformWeight, tillWeight, waterWeight, additiveOnly, GetComponent<Rigidbody>().velocity);
                }
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter ( Collision collision ) {
        //Aaaaaahhh
        //Destroy(this);
        collided = true;
    }

    public void OnTriggerEnter ( Collider other ) {
        //Ahhhh
        if (other.CompareTag("FarmField")) {
            field = other.gameObject;
            shouldWater = true;
        }
    }
}
