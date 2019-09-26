using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour {

    public Vector3 respawnPoint;

    public void OnTriggerEnter ( Collider other ) {
        other.transform.position = respawnPoint;
    }


    public void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(respawnPoint, Vector3.one);
    }
}
