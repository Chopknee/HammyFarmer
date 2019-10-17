using Chopknee.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bomb: HammyInteractable {

    public GameObject debrisPrefab;
    public int debrisCount;
    public float debrisRadius;

    public GameObject destructionPrefab;

    public float timeToExplosion;

    private bool exploding = false;

    public float explosionForce = 100f;
    [Range(0f, 1f)]
    public float upForcePercentage = 0.25f;
    float t = 0;

    public GameObject messageGUI;

    public void Update () {
        if (exploding) {
            t += Time.deltaTime;
            if (t >= timeToExplosion) {
                foreach (Vector3 vec in Utility.FibonacciSphereDistro(debrisCount, debrisRadius)) {
                    GameObject sd = Instantiate(debrisPrefab, transform.position + vec, Random.rotation);
                    sd.GetComponent<Rigidbody>().AddForce(
                        ( transform.position - ( transform.position + vec ) ) * (explosionForce * (1 - upForcePercentage)) + 
                        (Vector3.up * (explosionForce * upForcePercentage)));
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
            SetOff();
            GetComponent<AudioSource>().Play();
        }
    }

    public void SetOff() {
        exploding = true;
    }
}
