using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FaceVelocity: MonoBehaviour {

    public Vector3 upAxis = Vector3.down;
    public float minVelocity = 0.25f;
    public bool useAverageVelocity = false;
    public int averageVelocityBins = 10;
    Vector3[] velocities;

    Rigidbody rb;
    float minVelSquared;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody>();
        minVelSquared = minVelocity * minVelocity;
        if (useAverageVelocity) {
            velocities = new Vector3[averageVelocityBins];
        }
    }

    int cVel = 0;
    // Update is called once per frame
    void Update () {

        Vector3 vel = rb.velocity;
        if (useAverageVelocity) {
            velocities[cVel] = vel;
            vel = Vector3.zero;
            foreach (Vector3 v in velocities) {
                vel += v;
            }
            vel = vel / averageVelocityBins;
            cVel++;
            if (cVel >= averageVelocityBins) {
                cVel = 0;
            }
        }
        if (vel.sqrMagnitude > minVelSquared) {
            transform.up = -vel.normalized;
        } else {
            transform.rotation = Quaternion.identity;
        }
    }
}
