using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncinessSelector: MonoBehaviour {

    public GameObject ATrigger;
    public GameObject BTrigger;
    public GameObject CTrigger;

    public Light ALight;
    public Light BLight;
    public Light CLight;

    public float optionA = 1;
    public float optionB = 0.5f;
    public float optionC = 0f;

    public void Start () {
        ATrigger.GetComponent<TriggerPasser>().OnTriggerDidEnter += OnTriggered;
        BTrigger.GetComponent<TriggerPasser>().OnTriggerDidEnter += OnTriggered;
        CTrigger.GetComponent<TriggerPasser>().OnTriggerDidEnter += OnTriggered;

        ALight.intensity = 0;
        BLight.intensity = 0;
        CLight.intensity = 1;
    }

    public void OnTriggered(GameObject go, Collider other) {
        if (other.tag != "HammyBall") { return; }

        ALight.intensity = 0;
        BLight.intensity = 0;
        CLight.intensity = 0;

        if (go == ATrigger) {
            ALight.intensity = 1;
            other.material.bounciness = optionA;
        } else if (go == BTrigger) {
            BLight.intensity = 1;
            other.material.bounciness = optionB;
        } else if (go = CTrigger) {
            CLight.intensity = 1;
            other.material.bounciness = optionC;
        }
    }

}
