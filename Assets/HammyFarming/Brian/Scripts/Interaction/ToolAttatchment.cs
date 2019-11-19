using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Interaction {
    [RequireComponent(typeof(AudioSource))]
    public class ToolAttatchment: HammyInteractable {

        bool isHookedIn = false;

        Vector3 relativePos;
        ConfigurableJoint attachmentJoint;
        public GameObject messageGUI;

        public Vector3 rbOffset = new Vector3(0, 1.61f, -3.41f);
        public float positionSpring = 250;
        public float positionDamper = 2;

        public AudioClip attachSound;
        public AudioClip disconnectSound;
        public AudioClip breakConnectionSound;
        AudioSource attachSoundAS;
        [Range(0, 1f)]
        public float soundVolume;

        public bool teleportWhenTooFar = false;
        public float tooFarRadius = 10;
        float tooFarSquared;

        Rigidbody rb;

        private void Awake () {
            rb = GetComponent<Rigidbody>();
        }

        public override void Start () {
            attachSoundAS = GetComponent<AudioSource>();
            attachSoundAS.clip = attachSound;
            attachSoundAS.playOnAwake = false;
            attachSoundAS.loop = false;
            attachSoundAS.volume = soundVolume;

            tooFarSquared = tooFarRadius * tooFarRadius;

            base.Start();
            Director.InputMasterController.Hammy.Attach.performed += context => OnAttachPushed();
        }

        bool hasChangedState = false;

        public void Update () {
            hasChangedState = false;
            if (messageGUI != null) {
                if (!isHookedIn && isHammyInside) {
                    if (!messageGUI.activeSelf) {
                        messageGUI.SetActive(true);
                    }
                } else {
                    if (messageGUI.activeSelf) {
                        messageGUI.SetActive(false);
                    }
                }
            }
        }

        void OnAttachPushed () {
            if (isHookedIn && !hasChangedState) {
                //Disconnect it
                isHookedIn = false;
                Destroy(attachmentJoint);
                Destroy(hammy.GetComponent<ConfigurableJoint>());
                PlaySound(disconnectSound);
            }
        }

        private void FixedUpdate () {
            //Check if the distance between hammy ball and tool is too far.
            if (isHookedIn) {
                if (( hammy.transform.position - transform.position ).sqrMagnitude > tooFarSquared) {
                    if (teleportWhenTooFar) {
                        //Just tp to the hammy
                        transform.position = hammy.transform.position;
                    } else {
                        //Disconnect
                        isHookedIn = false;
                        Destroy(attachmentJoint);
                        Destroy(hammy.GetComponent<ConfigurableJoint>());
                        PlaySound(breakConnectionSound);
                    }
                }
            }
        }

        public override void HammyHookedIn ( GameObject hammy ) {
            if (!isHookedIn) {
                hasChangedState = true;
                isHookedIn = true;
                attachmentJoint = RecreateJoint(gameObject, hammy.GetComponent<Rigidbody>());
                attachmentJoint.anchor = rbOffset;
                attachmentJoint.autoConfigureConnectedAnchor = false;
                attachmentJoint.connectedAnchor = Vector3.zero;

                //ConfigurableJoint hbcj = RecreateJoint(hammy, rb);
                //hbcj.autoConfigureConnectedAnchor = false;
                //hbcj.connectedAnchor = Vector3.zero;

                PlaySound(attachSound);
            }
        }

        void PlaySound ( AudioClip clip ) {
            attachSoundAS.Stop();
            attachSoundAS.clip = clip;
            attachSoundAS.Play();
        }

        ConfigurableJoint RecreateJoint (GameObject go, Rigidbody connected) {
            ConfigurableJoint cj = go.AddComponent<ConfigurableJoint>();
            JointDrive jd = new JointDrive();
            
            jd.positionDamper = positionDamper;
            jd.positionSpring = positionSpring;
            jd.maximumForce = float.MaxValue;
            cj.xDrive = jd;
            cj.yDrive = jd;
            cj.zDrive = jd;
            cj.connectedBody = connected;
            return cj;
        }

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, tooFarRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + ( transform.rotation * rbOffset ), Vector3.one * 0.25f);
        }
    }
}