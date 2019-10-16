using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour {

    public Transform respawnPoint;

    public void OnTriggerEnter ( Collider other ) {
        other.transform.position = respawnPoint.position;
    }
}
