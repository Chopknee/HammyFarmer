using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Animation {

    [RequireComponent(typeof(Rigidbody))]
    public class TorqueOverTime: MonoBehaviour {

        public float angularForce = 1;
        public Vector3 angularAxis = new Vector3(0, 1, 0);
        public bool relativeAxis = true;
        Rigidbody rb;

        void Start () {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate () {
            if (relativeAxis) {
                rb.AddRelativeTorque(angularForce * angularAxis);
            } else {
                rb.AddTorque(angularForce * angularAxis);
            }
        }
    }
}