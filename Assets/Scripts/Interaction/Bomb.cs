using Chopknee.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bomb : HammyInteractable {

    public GameObject destructionPrefab;

    public float timeToExplosion;

    private bool exploding = false;

    public float explosionRadius = 0;
    public float explosionForceMultiplier = 10;

    public GameObject debrisPrefab;
    public int debrisCount;
    public float debrisRadius;

    public float debrisSpawnForce = 100f;
    [Range(0f, 1f)]
    public float debrisSpawnUpForce = 0.25f;
    float t = 0;

    public GameObject messageGUI;

    public override void Start() {
        base.Start();
    }

    public void Update () {
        if (exploding) {
            t += Time.deltaTime;
            if (t >= timeToExplosion) {
                foreach (Vector3 vec in Utility.FibonacciSphereDistro(debrisCount, debrisRadius)) {
                    GameObject sd = Instantiate(debrisPrefab, transform.position + vec, Random.rotation);
                    sd.GetComponent<Rigidbody>().AddForce(
                        ( transform.position - ( transform.position + vec ) ) * (debrisSpawnForce * (1 - debrisSpawnUpForce)) + 
                        (Vector3.up * (debrisSpawnForce * debrisSpawnUpForce)));
                }

                if (explosionRadius > 0) {
                    Collider[] objectsInExplosionRadius = Physics.OverlapSphere(transform.position, explosionRadius);
                    foreach (Collider objectInRadius in objectsInExplosionRadius) {
                        GameObject go = objectInRadius.gameObject;
                        //Do an inverse square for the explosion power

                        Vector3 positionDifference = go.transform.position - transform.position;
                        float explosionPower = ( explosionRadius - positionDifference.magnitude) * explosionForceMultiplier;
                        Debug.Log(explosionPower);
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        if (rb != null) {
                            rb.AddForce(positionDifference.normalized * explosionPower);
                        }
                    }
                }

                Destroy(gameObject);

                if (destructionPrefab != null) {
                    Instantiate(destructionPrefab, transform.position, Quaternion.identity);
                }
                GetComponent<AudioSource>().Stop();
                
            }
        }

        if (messageGUI != null) {
            if (isHammyInside) {
                if (!messageGUI.activeSelf) {
                    messageGUI.SetActive(true);
                }
            } else {
                if (messageGUI.activeSelf) {
                    messageGUI.SetActive(false);
                }
            }
        }
    }

    public override void HammyInteracted ( GameObject hammy ) {
        if (!exploding) {
            exploding = true;
            GetComponent<AudioSource>().Play();
        }
    }

    public void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
