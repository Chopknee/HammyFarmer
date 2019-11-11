using HammyFarming.Brian;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silo : MonoBehaviour {

    public float fillPerSecond = 0.1f;
    Harvester harvester;


    private void Update () {
        if (harvester != null) {
            if (harvester.fill > 0) {
                float amount = fillPerSecond * Time.deltaTime;
                if (harvester.fill < amount) {
                    amount = harvester.fill;
                }
                harvester.fill -= amount;
                Director.Instance.SiloFillLevel += amount;
            }
        }
    }

    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Harvester")) {
            //Offload the stuff.
            Debug.Log("ASS");
            harvester = other.GetComponent<Harvester>();
        }
    }

    private void OnTriggerExit ( Collider other ) {
        if (other.CompareTag("Harvester")) {
            harvester = null;
        }
    }
}
