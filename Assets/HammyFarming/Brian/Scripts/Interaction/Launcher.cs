using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Interaction {

    public class Launcher: MonoBehaviour {

        public float force = 100;

        List<Rigidbody> rigidBodies = new List<Rigidbody>();

        public void FixedUpdate () {
            for (int i = 0; i < rigidBodies.Count; i++) {
                Rigidbody rb = rigidBodies[i];
                if (rb) {
                    rb.AddForce(transform.up * force * rb.mass);
                } else {
                    rigidBodies.RemoveAt(i);
                    i--;
                }
            }
        }

        public void OnTriggerEnter ( Collider other ) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb & !rigidBodies.Contains(rb)) {
                rigidBodies.Add(rb);
            }
        }

        public void OnTriggerExit ( Collider other ) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb & rigidBodies.Contains(rb)) {
                rigidBodies.Remove(rb);
            }
        }

        private void OnDrawGizmos () {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * ( force / 100f ));
            Gizmos.DrawCube(transform.position + transform.up * ( force / 100f ), Vector3.one);
        }

    }
}