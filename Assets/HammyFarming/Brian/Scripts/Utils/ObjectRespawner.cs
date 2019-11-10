using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour {

    public Transform respawnPoint;

    public void OnTriggerEnter ( Collider other ) {
        if (other.GetComponent<Rigidbody>() != null) {
            if (respawnPoint != null) {
                other.transform.position = respawnPoint.position;
            } else {
                other.transform.position = Vector3.zero;
            }
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}
