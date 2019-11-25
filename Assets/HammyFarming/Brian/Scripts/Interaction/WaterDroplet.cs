using HammyFarming.Brian.Utils.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Interaction {

    public class WaterDroplet: MonoBehaviour {

        //Waits a bit before activating the water droplet modification to the farm field.
        public float collisionDelay = 2;
        public Texture2D deformationMap;
        public float mapScale = 1;
        public float mapWeight = 1;

        public float deSpawnTime = 15f;


        FarmFieldDeformation ffield;

        [Range(-1f, 1f)]
        public float deformWeight = 1;
        [Range(-1f, 1f)]
        public float tillWeight = 1;
        [Range(-1f, 1f)]
        public float waterWeight = 1;
        public bool additiveOnly = false;

        Timeout despawnTimeout;
        Timeout canWaterTimeout;

        private void Awake () {
            despawnTimeout = new Timeout(deSpawnTime, true);
            canWaterTimeout = new Timeout(collisionDelay, true);
        }

        void Update () {

            canWaterTimeout.Tick(Time.deltaTime);

            if (despawnTimeout.Tick(Time.deltaTime)) {
                Destroy(gameObject);
            }

        }

        public void OnDestroy () {
            if (ffield != null) {
                Vector3 weights = new Vector3(deformWeight, tillWeight, waterWeight);
                ffield.Deform(gameObject, deformationMap, 1, mapScale, mapWeight, weights, additiveOnly, new Vector3(1, 1, 1));
            }
        }

        public void OnTriggerStay ( Collider other ) {
            //Looking for a farm field
            if (other.CompareTag("FarmField")) {

                if (ffield == null) {
                    ffield = other.gameObject.GetComponent<FarmFieldDeformation>();
                }

                if (canWaterTimeout.NormalizedTime >= 1) {
                    Destroy(gameObject);
                }
            }
        }

        public void OnCollisionStay ( Collision collision ) {
            if (canWaterTimeout.NormalizedTime >= 1) {
                Destroy(gameObject);
            }
        }

        public void OnTriggerExit ( Collider other ) {
            ffield = null;
        }
    }
}
