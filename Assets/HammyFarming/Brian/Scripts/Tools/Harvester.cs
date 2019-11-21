using HammyFarming.Brian.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Tools {

    public class Harvester : MonoBehaviour {

        public float activationTime = 1;
        public float wheelAngularVelocity = 1000;

        public bool capturing;

        Timeout activateTimeout;
        Timeout deactivateTimeout;

        ConfigurableJoint[] wheelsCJs;
        Rigidbody[] wheelsRBs;

        public GameObject frontLeftWheel;
        public GameObject frontRightWheel;
        public GameObject backLeftWheel;
        public GameObject backRigtWheel;

        private void Awake() {
            wheelsCJs = new ConfigurableJoint[4];
            wheelsCJs[0] = frontLeftWheel.GetComponent<ConfigurableJoint>();
            wheelsCJs[1] = frontRightWheel.GetComponent<ConfigurableJoint>();
            wheelsCJs[2] = backLeftWheel.GetComponent<ConfigurableJoint>();
            wheelsCJs[3] = backRigtWheel.GetComponent<ConfigurableJoint>();

            wheelsRBs = new Rigidbody[4];
            wheelsRBs[0] = frontLeftWheel.GetComponent<Rigidbody>();
            wheelsRBs[1] = frontRightWheel.GetComponent<Rigidbody>();
            wheelsRBs[2] = backLeftWheel.GetComponent<Rigidbody>();
            wheelsRBs[3] = backRigtWheel.GetComponent<Rigidbody>();

            foreach (ConfigurableJoint wheel in wheelsCJs) {
                wheel.angularYMotion = ConfigurableJointMotion.Locked;
            }
            activateTimeout = new Timeout(activationTime, false);
            deactivateTimeout = new Timeout(activationTime, false);
        }

        public Vector3 wheelTorqueAxis;
        //Vector3 wheelTorque;

        private void Update() {
            if (activateTimeout.Tick(Time.deltaTime)) {
                capturing = true;
                activateTimeout.Reset();
                foreach (ConfigurableJoint wheel in wheelsCJs) {
                    wheel.angularYMotion = ConfigurableJointMotion.Free;
                }
            }

            if (deactivateTimeout.Tick(Time.deltaTime)) {
                deactivateTimeout.Reset();
                foreach (Rigidbody rb in wheelsRBs) {
                    rb.angularVelocity = Vector3.zero;
                    rb.velocity = Vector3.zero;
                }

                foreach (ConfigurableJoint wheel in wheelsCJs) {
                    wheel.angularYMotion = ConfigurableJointMotion.Locked;
                }
            }

            if (capturing) {

                Vector2 rollDirection = Director.InputMasterController.Hammy.Roll.ReadValue<Vector2>();

                if (Mathf.Abs(rollDirection.y) > 0.1f) {
                    //All 4 wheels drive forward or backwards
                    foreach (Rigidbody rb in wheelsRBs) {
                        rb.AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.y);
                    }
                } else if (Mathf.Abs(rollDirection.x) > 0.1f) {
                    if (rollDirection.y > 0) {
                        //left forward
                        //right backwards
                        wheelsRBs[0].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);
                        wheelsRBs[2].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);
                        wheelsRBs[1].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);
                        wheelsRBs[3].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);

                    } else {
                        //left backwards
                        //right forward
                        wheelsRBs[0].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);
                        wheelsRBs[2].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);
                        wheelsRBs[1].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);
                        wheelsRBs[3].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);
                    }
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