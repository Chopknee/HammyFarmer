using HammyFarming.Brian.Utils.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour {

    public Transform respawnPoint;
    public float respawnRepeatDelay = 2;
    List<GameObject> respawns;
    Timeout respawnTimeout;

    public void Awake () {
        respawns = new List<GameObject>();
        respawnTimeout = new Timeout(respawnRepeatDelay);
        respawnTimeout.Start();
    }

    private void Update () {
        if (respawnTimeout.Tick(Time.deltaTime)) {
            if (respawns.Count > 0) {
                GameObject other = respawns[0];
                respawns.Remove(other);
                if (other.GetComponent<Rigidbody>() != null) {
                    if (respawnPoint != null) {
                        other.transform.position = respawnPoint.position;
                    } else {
                        other.transform.position = Vector3.zero;
                    }
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
            respawnTimeout.ReStart();
        }
    }

    public void OnTriggerEnter ( Collider other ) {
        respawns.Add(other.gameObject);
    }
}
