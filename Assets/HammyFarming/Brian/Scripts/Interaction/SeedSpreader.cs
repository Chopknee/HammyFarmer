using HammyFarming.Brian.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Interaction {

    public class SeedSpreader: MonoBehaviour {

        public Rigidbody wheelRB;

        public float averageWheelVelocity;

        Average wheelSpeedAverage;

        public ConstantDrip[] spawners;
        public float minSpeed;

        void Start () {
            if (wheelRB == null || spawners == null) {
                gameObject.SetActive(false);
            }
            wheelSpeedAverage = new Average(20);
        }

        void Update () {
            //Try to figure out the rotational velocity of the wheel. (We don't care about the 'z' velocity)
            Vector2 vel = new Vector2(wheelRB.angularVelocity.x, wheelRB.angularVelocity.z);
            averageWheelVelocity = wheelSpeedAverage.GetNext(vel.magnitude);
            if (averageWheelVelocity > 0.5f) {
                GetComponentInChildren<SprinklerSpin>().spinSpeed = averageWheelVelocity;
            } else {
                GetComponentInChildren<SprinklerSpin>().spinSpeed = 0;
            }
            if (averageWheelVelocity > minSpeed) {
                SetSpawners(true);
            } else {
                SetSpawners(false);
            }
        }

        void SetSpawners ( bool active ) {
            foreach (ConstantDrip cd in spawners) {
                cd.active = active;
            }
        }
    }
}