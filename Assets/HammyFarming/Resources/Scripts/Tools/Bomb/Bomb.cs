using Chopknee.Utility;
using UnityEngine;

namespace HammyFarming.Tools.Bomb {

    [RequireComponent(typeof(AudioSource))]
    public class Bomb: HammyFarming.Brian.HammyInteractable {

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

        public override void Start () {
            base.Start();
        }

        public void Update () {
            if (exploding) {
                t += Time.deltaTime;
                if (t >= timeToExplosion) {

                    SpawnDebris();

                    ThrowObjects();

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

        public void SpawnDebris() {
            if (debrisCount > 0) {
                foreach (Vector3 vec in Utility.FibonacciSphereDistro(debrisCount, debrisRadius)) {
                    GameObject sd = Instantiate(debrisPrefab, transform.position + vec, Random.rotation);
                    sd.GetComponent<Rigidbody>().AddForce(
                        ( transform.position - ( transform.position + vec ) ) * ( debrisSpawnForce * ( 1 - debrisSpawnUpForce ) ) +
                        ( Vector3.up * ( debrisSpawnForce * debrisSpawnUpForce ) ));
                }
            }
        }

        public void ThrowObjects() {
            if (explosionRadius > 0) {
                Collider[] objectsInExplosionRadius = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach (Collider objectInRadius in objectsInExplosionRadius) {
                    GameObject go = objectInRadius.gameObject;

                    if (go.CompareTag("Destructable")) {
                        IDestructable ds = go.GetComponent<IDestructable>();
                        if (ds != null) {
                            ds.Break();
                        } else {
                            Destroy(go);
                        }
                        continue;
                    }

                    Vector3 positionDifference = go.transform.position - transform.position;
                    float explosionPower = ( explosionRadius - positionDifference.magnitude ) * explosionForceMultiplier;
                    Rigidbody rb = go.GetComponent<Rigidbody>();
                    if (rb != null) {
                        rb.AddForce(positionDifference.normalized * explosionPower * rb.mass);
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
}