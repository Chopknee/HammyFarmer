using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemDispenser: HammyInteractable {

    public GameObject prefab;

    public Vector3 dispensePosition;

    
    public float reuseDelay = 10;
    public int spawnLimit = 0;
    public int spawnCount = 0;

    public GameObject iconObject;

    Vector3 spawnPos {
        get {
            return ( transform.rotation * dispensePosition ) + transform.position;
        }
    }

    float delay = 0;
    bool hasSpawned = false;

    public override void Update () {
        if (hasSpawned) {
            delay += Time.deltaTime;
            if (delay >= reuseDelay) {
                delay = 0;
                hasSpawned = false;
            }
        }

        base.Update();
    }

    public override void HammyInteracted ( GameObject hammy ) {
        if (spawnCount < spawnLimit && !hasSpawned) {
            hasSpawned = true;
            Instantiate(prefab, spawnPos, Quaternion.identity);
            spawnCount++;
            GetComponent<AudioSource>().Play();
        }
    }

    public override void HammyEntered ( GameObject hammy ) {
        iconObject.SetActive(true);
    }

    public override void HammyExited () {
        iconObject.SetActive(false);
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.blue;
        Vector3 pos = transform.rotation * dispensePosition;
        Gizmos.DrawWireCube(spawnPos, Vector3.one);
    }
}
