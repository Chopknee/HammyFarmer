using HammyFarming.Brian.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Tools {

    public class Harvester : MonoBehaviour {

        public float activationTime = 1;

        public bool capturing;

        Timeout activateTimeout;
        Timeout deactivateTimeout;

        ConfigurableJoint[] wheels;

        public GameObject frontLeftWheel;
        public GameObject frontRightWheel;
        public GameObject backLeftWheel;
        public GameObject backRigtWheel;

        private void Awake() {
            wheels = new ConfigurableJoint[4];
            wheels[0] = frontLeftWheel.GetComponent<ConfigurableJoint>();
            wheels[1] = frontRightWheel.GetComponent<ConfigurableJoint>();
            wheels[2] = backLeftWheel.GetComponent<ConfigurableJoint>();
            wheels[3] = backRigtWheel.GetComponent<ConfigurableJoint>();

            foreach (ConfigurableJoint wheel in wheels) {
                wheel.angularYMotion = ConfigurableJointMotion.Locked;
            }
            activateTimeout = new Timeout(activationTime, false);
            deactivateTimeout = new Timeout(activationTime, false);
        }

        private void Update() {
            if (activateTimeout.Tick(Time.deltaTime)) {
                capturing = true;
                activateTimeout.Reset();
                foreach (ConfigurableJoint wheel in wheels) {
                    wheel.angularYMotion = ConfigurableJointMotion.Free;
                }
            }

            if (deactivateTimeout.Tick(Time.deltaTime)) {
                deactivateTimeout.Reset();
                foreach (ConfigurableJoint wheel in wheels) {
                    wheel.angularYMotion = ConfigurableJointMotion.Locked;
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("HammyBall")) {
                activateTimeout.Start();
                deactivateTimeout.Reset();
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("HammyBall")) {
                capturing = false;
                deactivateTimeout.Start();
                activateTimeout.Reset();
            }
        }
    }
}