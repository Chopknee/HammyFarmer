using HammyFarming.Brian.Base.Hammy;
using HammyFarming.Brian.Utils.Timing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.Tools {

    public class Harvester : MonoBehaviour {

        public float activationTime = 1;
        public float wheelAngularVelocity = 1000;

        public float hammyJointSpring = 100;
        public float hammyJointDamper = 100;


        public Vector3 hammyAnchorPoint;
        //public Transform hammyAnchorPoint;

        public bool capturing;

        Timeout activateTimeout;
        Timeout deactivateTimeout;

        ConfigurableJoint[] wheelsCJs;
        Rigidbody[] wheelsRBs;

        public GameObject frontLeftWheel;
        public GameObject frontRightWheel;
        public GameObject backLeftWheel;
        public GameObject backRigtWheel;

        private GameObject hammy;
        private CameraMotion cameraAnchor;
        private Transform cameraAnchorPoint;

        private ConfigurableJoint hammyJoint;

        bool canReconnect = true;

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

            cameraAnchorPoint = transform.Find("CameraAnchor");
        }

        public Vector3 wheelTorqueAxis;

        private void Update() {
            if (activateTimeout.running) {
                activateTimeout.Tick(Time.deltaTime);
            }

            if (activateTimeout.NormalizedTime > 0.99f && canReconnect) {
                //Taking control of the harvester
                activateTimeout.Reset();
                foreach (ConfigurableJoint wheel in wheelsCJs) {
                    wheel.angularYMotion = ConfigurableJointMotion.Free;
                }
                hammyJoint = MakeHammyJoint(hammy, hammyJointSpring, hammyJointDamper);
                Base.PlayerInput.ControlMaster.Hammy.Jump.performed += HammyJumped;
                capturing = true;
                cameraAnchor = hammy.GetComponentInChildren<CameraMotion>();
                cameraAnchor.managed = true;
                cameraAnchor.managedTargetTransform = cameraAnchorPoint;
            }

            if (deactivateTimeout.Tick(Time.deltaTime)) {
                //Relinquishing control of the harvester
                deactivateTimeout.Reset();
                canReconnect = true;
            }

            if (capturing) {

                Vector2 rollDirection = Base.PlayerInput.ControlMaster.Hammy.Roll.ReadValue<Vector2>();

                if (Mathf.Abs(rollDirection.x) > 0.1f) {
                    if (rollDirection.x > 0) {
                        //left forward
                        //right backwards
                        wheelsRBs[0].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);
                        wheelsRBs[2].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);
                        wheelsRBs[1].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);
                        wheelsRBs[3].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);

                    } else {
                        //left backwards
                        //right forward
                        wheelsRBs[0].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);
                        wheelsRBs[2].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.x);
                        wheelsRBs[1].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);
                        wheelsRBs[3].AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * rollDirection.x);
                    }
                }
                if (Mathf.Abs(rollDirection.y) > 0.1f) {
                    //All 4 wheels drive forward or backwards
                    foreach (Rigidbody rb in wheelsRBs) {
                        rb.AddRelativeTorque(wheelTorqueAxis * wheelAngularVelocity * -rollDirection.y);
                    }
                }
            }
        }

        void HammyJumped(InputAction.CallbackContext context) {
            if (hammyJoint != null) {
                Destroy(hammyJoint);
                hammyJoint = null;
            }
            capturing = false;
            deactivateTimeout.Start();//Delay time before hammy can re capture control??
            Base.PlayerInput.ControlMaster.Hammy.Jump.performed -= HammyJumped;
            canReconnect = false;
            cameraAnchor.managed = false;
        }

        private void OnDestroy () {
            
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("HammyBall")) {
                activateTimeout.Start();
                hammy = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("HammyBall")) {
                activateTimeout.Reset();
            }
        }

        ConfigurableJoint MakeHammyJoint(GameObject go, float positionDamper, float positionSpring) {
            ConfigurableJoint cj = gameObject.AddComponent<ConfigurableJoint>();
            cj.connectedBody = go.GetComponent<Rigidbody>();

            JointDrive jd = new JointDrive();
            jd.positionDamper = positionSpring;
            jd.positionSpring = positionDamper;
            jd.maximumForce = float.MaxValue;

            cj.xDrive = jd;
            cj.yDrive = jd;
            cj.zDrive = jd;
            Vector3 localScale = transform.localScale;
            cj.anchor = new Vector3(hammyAnchorPoint.x / localScale.x, hammyAnchorPoint.y / localScale.y, hammyAnchorPoint.z / localScale.z); //* //transform.localScale;//hammyAnchorPoint.position - go.transform.position;
            cj.autoConfigureConnectedAnchor = false;
            cj.connectedAnchor = Vector3.zero;

            cj.enableCollision = true;
            return cj;
        }

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + ( transform.rotation * hammyAnchorPoint ), Vector3.one * 0.25f);
        }
    }
}