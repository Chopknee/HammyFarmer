using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Farm.Plants {

    public class PlantGrowth: MonoBehaviour {


        public float growTime = 10;
        public float soilCheckTime = 1;

        private HammyFarming.Brian.Utils.Timing.Timeout growTimer;
        private HammyFarming.Brian.Utils.Timing.Timeout soilCheckTimer;

        private bool growing;
        public bool isGrowing { get { return growing; } }

        public Color colorUnderMe;

        [Range(0f, 1f)]
        public float minimumTilledness;
        [Range(0f, 1f)]
        public float minimumWetness;
        public LayerMask fieldMask;

        public float growPercent {
            get {
                return growTimer.NormalizedTime;
            }
        }


        public virtual void Awake () {
            growTimer = new HammyFarming.Brian.Utils.Timing.Timeout(growTime);
            soilCheckTimer = new HammyFarming.Brian.Utils.Timing.Timeout(soilCheckTime);
            soilCheckTimer.Start();
        }

        public virtual void Update () {

            if (growTimer.Tick(Time.deltaTime)) {
                //Debug.Log("Raddish has grown!");
                //counter.Reset();
                //counter.Start();
            }

            if (soilCheckTimer.Tick(Time.deltaTime)) {
                //Do the soil check
                soilCheckTimer.ReStart();

                if (Physics.Raycast(transform.position + ( Vector3.up * 5 ), Vector3.down, out RaycastHit hit, 50, fieldMask)) {
                    //Only collides with the field.
                    //Now checking the color under the seed. (probably gonna do an average under the seed at some point.
                    colorUnderMe = hit.collider.gameObject.GetComponent<FarmFieldDeformation>().GetFieldValuesAt(hit.textureCoord);
                    growing = colorUnderMe.g >= minimumTilledness && colorUnderMe.b >= minimumWetness;

                    GrowthState(growing);

                    if (growing && !growTimer.running) {
                        growTimer.Start();
                    } else if (growTimer.running) {
                        growTimer.Pause();
                    }

                } else {
                    growing = false;
                    growTimer.Pause();
                    colorUnderMe = Color.black;
                    growTimer.Pause();
                    GrowthState(false);
                }
            }

            if (growing) {
                OnGrowing();
            }
        }

        bool lastGrowing = false;

        private void GrowthState(bool state) {
            if (lastGrowing != isGrowing) {
                OnGrowthStateChanged(state);
                lastGrowing = state;
            }
        }

        public virtual void OnGrowing () {}

        public virtual void OnGrowthStateChanged(bool isGrowing) {}

        public virtual void OnCollected () {}

        public virtual void OnUprooted () {
            enabled = false;
        }

        private void OnTriggerEnter ( Collider other ) {
            if (other.CompareTag("Harvester")) {
                HammyFarming.Tools.Harvester.Collector collector = other.GetComponent<HammyFarming.Tools.Harvester.Collector>();
                if (collector) {
                    if (collector.Harvest(this))
                        OnCollected();
                    else
                        OnUprooted();
                }
            }
        }

        public virtual void OnValidate () {
            growTime = Mathf.Clamp(growTime, 0.001f, 3600);
        }

    }
}