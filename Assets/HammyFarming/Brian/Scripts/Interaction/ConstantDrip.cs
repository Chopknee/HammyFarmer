using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Interaction {

    public class ConstantDrip: MonoBehaviour {

        public GameObject prefab;
        public float delay;
        public bool active;

        public Vector3 spawnPosition;

        public Vector3 appliedForceDirection;
        public float forceMultiplier;

        Vector3 spawnPt {
            get {
                return transform.rotation * spawnPosition;
            }
        }

        Vector3 fc {
            get {
                return ( transform.rotation * appliedForceDirection ).normalized * forceMultiplier;
            }
        }

        float del = 0;

        void Update () {
            if (active) {
                del += Time.deltaTime;
                if (del > delay) {
                    del = 0;
                    GameObject go = Instantiate(prefab, transform.position + spawnPt, Quaternion.identity);
                    if (go.GetComponent<Rigidbody>() != null) {
                        go.GetComponent<Rigidbody>().AddForce(fc);
                    }
                }
            }
        }

        private void OnDrawGizmos () {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position + spawnPt, Vector3.one * 0.3f);
            Gizmos.DrawLine(transform.position + spawnPt, transform.position + spawnPt + fc);
        }
    }
}