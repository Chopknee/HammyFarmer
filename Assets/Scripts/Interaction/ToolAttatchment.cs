using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAttatchment : HammyInteractable {

    bool isHookedIn = false;

    Vector3 relativePos;
    ConfigurableJoint attachmentJoint;
    public GameObject messageGUI;

    public Vector3 rbOffset = new Vector3(0, 1.61f, -3.41f);
    public float positionSpring = 250;
    public float positionDamper = 2;

    public AudioClip attachSound;
    AudioSource attachSoundAS;
    [Range(0, 1f)]
    public float soundVolume;

    public bool teleportWhenTooFar = false;
    public float tooFarRadius = 10;
    float tooFarSquared;

    private void Start () {
        attachSoundAS = gameObject.AddComponent<AudioSource>();
        attachSoundAS.clip = attachSound;
        attachSoundAS.playOnAwake = false;
        attachSoundAS.loop = false;
        attachSoundAS.volume = soundVolume;

        tooFarSquared = tooFarRadius * tooFarRadius;

    }

    bool hasChangedState = false;

    public override void Update () {
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

        if (isHookedIn) {
            if (Input.GetButtonDown("Use")) {
                hasChangedState = true;
                //Disconnect it
                isHookedIn = false;
                Destroy(attachmentJoint);
            }
        }

        base.Update();
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
                }
            }
        }
    }
    
    public override void HammyInteracted(GameObject hammy) {
        if (!isHookedIn && !hasChangedState) {
            isHookedIn = true;
            RecreateJoint();
            attachmentJoint.connectedBody = hammy.transform.Find("ToolAttatchmentPoint").GetComponent<Rigidbody>();
            attachmentJoint.autoConfigureConnectedAnchor = false;
            attachmentJoint.connectedAnchor = Vector3.zero;
            attachSoundAS.Play();
        }
    }

    void RecreateJoint() {
        attachmentJoint = gameObject.AddComponent<ConfigurableJoint>();
        JointDrive jd = new JointDrive();
        attachmentJoint.anchor = rbOffset;
        jd.positionDamper = positionDamper;
        jd.positionSpring = positionSpring;
        jd.maximumForce = float.MaxValue;
        attachmentJoint.xDrive = jd;
        attachmentJoint.yDrive = jd;
        attachmentJoint.zDrive = jd;
    }

    private void OnDrawGizmosSelected () {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, tooFarRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(( transform.rotation * transform.position ) + rbOffset, Vector3.one * 0.25f);
    }
}
