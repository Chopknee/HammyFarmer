using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAttatchment : MonoBehaviour {

    GameObject hammy;
    bool isHammyInside = false;

    bool isHookedIn = false;

    Vector3 relativePos;
    ConfigurableJoint cj;
    public GameObject messageGUI;

    Vector3 rbOffset;
    JointDrive xDrive;
    JointDrive yDrive;
    JointDrive zDrive;

    public AudioClip attachSound;
    AudioSource attachSoundAS;
    [Range(0, 1f)]
    public float soundVolume;

    public bool teleportWhenTooFar = false;
    public float tooFarRadius = 10;
    float tooFarSquared;

    private void Start () {
        cj = GetComponent<ConfigurableJoint>();

        rbOffset = cj.anchor;
        xDrive = cj.xDrive;
        yDrive = cj.yDrive;
        zDrive = cj.zDrive;

        Destroy(cj);

        attachSoundAS = gameObject.AddComponent<AudioSource>();
        attachSoundAS.clip = attachSound;
        attachSoundAS.playOnAwake = false;
        attachSoundAS.loop = false;
        attachSoundAS.volume = soundVolume;

        tooFarSquared = tooFarRadius * tooFarRadius;

    }

    private void Update () {

        if (!isHookedIn && isHammyInside) {
            if (!messageGUI.activeSelf) {
                messageGUI.SetActive(true);
            }
        } else {
            if (messageGUI.activeSelf) {
                messageGUI.SetActive(false);
            }
        }

        bool doneThing = false;
        if (isHammyInside) {
            if (Input.GetButtonDown("Use")) {
                if (!isHookedIn) {
                    //Reconnect it
                    isHookedIn = true;

                    RecreateJoint();
                    cj.connectedBody = hammy.transform.Find("ToolAttatchmentPoint").GetComponent<Rigidbody>();
                    cj.autoConfigureConnectedAnchor = false;
                    cj.connectedAnchor = Vector3.zero;

                    doneThing = true;
                    attachSoundAS.Play();
                }
            }
        }

        if (isHookedIn && !doneThing) {
            if (Input.GetButtonDown("Use")) {
                //Disconnect it
                isHookedIn = false;
                Destroy(cj);
                hammy = null;
            }
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
                    Destroy(cj);
                    hammy = null;
                }
            }
        }
    }

    private void OnTriggerEnter ( Collider other ) {
        if (other.tag == "HammyBall") {
            isHammyInside = true;
            hammy = other.gameObject;
        }
    }

    private void OnTriggerExit ( Collider other ) {
        if (other.tag == "HammyBall") {
            isHammyInside = false;
            //hammy = null;
        }
    }

    void RecreateJoint() {
        cj = gameObject.AddComponent<ConfigurableJoint>();
        cj.xDrive = xDrive;
        cj.yDrive = yDrive;
        cj.zDrive = zDrive;
        cj.anchor = rbOffset;
    }

    private void OnDrawGizmosSelected () {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, tooFarRadius);
    }
}
