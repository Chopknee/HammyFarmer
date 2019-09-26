using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPasser : MonoBehaviour {

    public delegate void TriggerEntered (GameObject caller, Collider other);
    public TriggerEntered OnTriggerDidEnter;


    public void OnTriggerEnter ( Collider other ) {
        OnTriggerDidEnter(gameObject, other);
    }

}
