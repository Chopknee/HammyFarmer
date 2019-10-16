using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Flea: MonoBehaviour {

    public float attemptJumpRadius;
    public float jumpForce;
    public float upAmount;
    public float jumpRestTime = 2.5f;

    public Transform target;

    public AudioClip hookOnSound;
    public AudioClip unhookSound;
    public AudioClip jumpSound;
    public Vector2 randomPitchRange = new Vector2(0.75f, 1.25f);

    float distSquared;
    Rigidbody rb;
    AudioSource aus;

    public bool jumped = false;
    public bool hooked = false;
    public bool resting = false;

    void Start () {
        if (target == null) {
            target = GameObject.FindGameObjectWithTag("HammyBall").transform;
        }

        distSquared = attemptJumpRadius * attemptJumpRadius;
        rb = GetComponent<Rigidbody>();
        aus = GetComponent<AudioSource>(); 
    }

    float t = 0;
    float tt = 0;
    void Update () {
        if (!hooked) {
            if (jumped) {
                t += Time.deltaTime;
                if (t >= jumpRestTime) {
                    jumped = false;
                    t = 0;
                }
            }

            if (resting) {
                tt += Time.deltaTime;
                if (tt >= 10) {
                    tt = 0;
                    resting = false;
                }
            }

            if (!jumped && target != null && !resting) {
                Vector3 dist = target.position - transform.position;
                if (dist.sqrMagnitude < distSquared) {
                    dist.y *= upAmount;
                    //Attempt to jump on the player.
                    rb.AddForce(dist * jumpForce);
                    jumped = true;
                    PlaySound(jumpSound, false);
                }
            }
        }
    }

    private void OnCollisionEnter ( Collision collision ) {
        if (!hooked && !resting) {
            if (collision.gameObject.CompareTag("HammyBall")) {
                //Stick to the player! (IDK HOW...)
                transform.parent = collision.transform;
                Destroy(rb);
                hooked = true;
                t = 0;
                jumped = false;
                PlaySound(hookOnSound, true);
            }
        }
    }

    private void OnTriggerEnter ( Collider other ) {
        if (hooked) {
            if (other.gameObject.CompareTag("WaterDrop")) {
                rb = gameObject.AddComponent<Rigidbody>();
                transform.parent = null;
                hooked = false;
                resting = true;
                PlaySound(unhookSound, true);
            }
        }
    }

    private void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attemptJumpRadius);
    }

    private void OnValidate () {
        upAmount = Mathf.Max(1, upAmount);
        jumpForce = Mathf.Max(1, jumpForce);
        randomPitchRange.x = Mathf.Max(0.001f, randomPitchRange.x);
        randomPitchRange.y = Mathf.Max(randomPitchRange.x + 0.001f, randomPitchRange.y);
    }

    void PlaySound(AudioClip cl, bool randomPitch) {
        aus.Stop();
        aus.clip = cl;
        if (randomPitch) {
            aus.pitch = Random.Range(randomPitchRange.x, randomPitchRange.y);
        } else {
            aus.pitch = 1;
        }
        aus.Play();
    }
}
