using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour {

    public Transform respawnPoint;

    public void OnTriggerEnter ( Collider other ) {
        if (other.GetComponent<Rigidbody>() != null) {
            other.transform.position = respawnPoint.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
}
