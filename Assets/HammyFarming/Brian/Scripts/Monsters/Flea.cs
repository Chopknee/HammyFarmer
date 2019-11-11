using HammyFarming.Brian.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Monsters {

    [RequireComponent(typeof(AudioSource))]
    public class Flea: MonoBehaviour {

        public float attemptJumpRadius;
        public float jumpForce;
        public float upAmount;
        public float jumpRestTime = 2.5f;
        public float hitByWaterRestTime = 10;

        public Transform target;

        public AudioClip hookOnSound;
        public AudioClip unhookSound;
        public AudioClip jumpSound;
        public Vector2 randomPitchRange = new Vector2(0.75f, 1.25f);

        float distSquared;
        Rigidbody rb;
        AudioSource aus;

        bool jumped = false;//If the flea has jumped
        bool hooked = false;//If the flea has hooked on to the player
        bool resting = false;//If the flea has been it by water?

        Vector3 reSpawnPoint;

        Timeout jumpRestTimeout;
        Timeout restingTimeout;

        private void Awake () {
            reSpawnPoint = transform.position;

            distSquared = attemptJumpRadius * attemptJumpRadius;

            rb = GetComponent<Rigidbody>();
            aus = GetComponent<AudioSource>();

            jumpRestTimeout = new Timeout(jumpRestTime);
            restingTimeout = new Timeout(hitByWaterRestTime);

        }

        private void Start () {
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("HammyBall").transform;
            }
        }

        void Update () {
            if (!hooked) {//When hooked, this is skipped

                if (jumpRestTimeout.Tick(Time.deltaTime)) {
                    jumped = false;
                    jumpRestTimeout.Reset();
                }

                if (restingTimeout.Tick(Time.deltaTime)) {
                    resting = false;
                    restingTimeout.Reset();
                    //This only runs after being hit by a water droplet and being stuck on the player.
                    transform.position = reSpawnPoint;
                }

                if (!jumped && target != null && !resting) {
                    Vector3 dist = target.position - transform.position;
                    //Jumpy code here
                    if (dist.sqrMagnitude < distSquared) {
                        dist.y *= upAmount;
                        rb.AddForce(dist * jumpForce);
                        jumped = true;
                        PlaySound(jumpSound, false);
                        jumpRestTimeout.Start();
                    }
                }
            }
        }

        private void OnCollisionEnter ( Collision collision ) {
            if (!hooked && !resting) {
                if (collision.gameObject.CompareTag("HammyBall")) {
                    transform.SetParent(collision.transform);
                    transform.up = ( transform.position - collision.transform.position );
                    transform.localPosition = transform.localPosition.normalized * 1.8f;
                    Destroy(rb);
                    hooked = true;
                    jumped = false;
                    PlaySound(hookOnSound, true);
                }
            }
        }

        private void OnTriggerEnter ( Collider other ) {
            if (hooked) {//Only when hooked on the player...
                if (other.gameObject.CompareTag("WaterDrop")) {
                    rb = gameObject.AddComponent<Rigidbody>();
                    transform.parent = null;
                    hooked = false;
                    resting = true;
                    PlaySound(unhookSound, true);
                    restingTimeout.Start();
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

        void PlaySound ( AudioClip cl, bool randomPitch ) {
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
}