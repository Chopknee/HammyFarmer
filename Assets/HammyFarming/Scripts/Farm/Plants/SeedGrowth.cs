using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Farm.Plants {

    public class SeedGrowth: MonoBehaviour {

        public GameObject plantPrefab;

        public Sprite cantGrowIcon;
        public Sprite canGrowIcon;

        [Range(0f, 1f)]
        public float minimumTilledness;
        [Range(0f, 1f)]
        public float minimumWetness;
        public float growCheckTime = 1;
        public float nextStateTime = 1;
        public float deathTime = 20;

        public LayerMask fieldMask;

        public SpriteRenderer sr;

        float t = 0;
        float ns = 0;
        public bool growing = false;
        public bool dying = true;
        public Color colorUnderMe;

        public float crowdedRadius = 1;

        bool radiusChecked = false;

        float dt = 0;
        void Update () {
            t += Time.deltaTime;
            if (t >= growCheckTime) {
                t = 0;
                //Checking if the seed can grow.
                if (Physics.Raycast(transform.position + ( Vector3.up * 5 ), Vector3.down, out RaycastHit hit, 50, fieldMask)) {
                    colorUnderMe = hit.collider.gameObject.GetComponentInParent<HammyFarming.Farm.FarmFieldDeformation>().GetFieldValuesAt(hit.textureCoord);
                    growing = colorUnderMe.g > minimumTilledness && colorUnderMe.b > minimumWetness;
                    dying = colorUnderMe.g < minimumTilledness;
                    if (growing) {
                        if (sr != null) { sr.sprite = canGrowIcon; }
                    } else {
                        if (sr != null) { sr.sprite = cantGrowIcon; }
                    }
                }
            }

            if (growing && !radiusChecked && ns > ( nextStateTime / 3 )) {
                //Check the radius around this seed to see if it can start growing. If not, instantly destroy it.
                Collider[] otherSeeds = Physics.OverlapSphere(transform.position, crowdedRadius);
                foreach (Collider c in otherSeeds) {

                    if (c.gameObject != gameObject && ( c.CompareTag("Seed") || c.CompareTag("Plant") )) {
                        //We are indeed too close to another seed.
                        Destroy(gameObject);
                    }
                }
                radiusChecked = true;
            }

            sr.color = colorUnderMe;
            if (growing) {
                ns += Time.deltaTime;
                dt = 0;
            }

            if (dying) {
                dt += Time.deltaTime;
                ns = 0;
            }

            if (ns > nextStateTime) {
                Instantiate(plantPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (dt > deathTime) {
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, crowdedRadius);
        }
    }
}