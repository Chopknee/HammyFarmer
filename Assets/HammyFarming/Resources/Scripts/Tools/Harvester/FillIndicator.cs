using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Tools.Harvester {
    public class FillIndicator: MonoBehaviour {

        private UnityEngine.UI.Image fillLevel;
        private HammyFarming.Tools.Harvester.Collector collector;
        private CanvasGroup harvestFillIndicator = null;

        private HammyFarming.Tools.Harvester.Driving driving;

        private HammyFarming.Brian.Utils.Timing.Timeout fadeInTimeout;
        private HammyFarming.Brian.Utils.Timing.Timeout fadeOutTimeout;

        public void Awake () {

            harvestFillIndicator = transform.Find("HarvesterFillIndicator").GetComponent<CanvasGroup>();
            fillLevel = harvestFillIndicator.transform.Find("Fill").GetComponent<UnityEngine.UI.Image>();
            collector = transform.Find("HarvestingPart").GetComponent<HammyFarming.Tools.Harvester.Collector>();
            collector.OnFillChanged += OnFillChanged;
            driving = GetComponent<HammyFarming.Tools.Harvester.Driving>();
            driving.OnActivated += OnActivated;
            driving.OnDeactivated += OnDeactivated;

            fadeInTimeout = new Brian.Utils.Timing.Timeout(1);
            fadeOutTimeout = new Brian.Utils.Timing.Timeout(1);

        }

        void OnFillChanged(float level) {
            fillLevel.fillAmount = level /  collector.MaxFill;
        }

        void OnActivated () {
            if (fadeOutTimeout.running) {
                fadeInTimeout.NormalizedTime = fadeOutTimeout.NormalizedTime;
                fadeInTimeout.Reset();
            }
            fadeInTimeout.Start();
        }

        void OnDeactivated() {
            if (fadeInTimeout.running) {
                fadeOutTimeout.NormalizedTime = fadeInTimeout.NormalizedTime;
                fadeInTimeout.Reset();
            }
            fadeOutTimeout.Start();
        }

        private void Update () {

            if (fadeInTimeout.running) {
                harvestFillIndicator.alpha = fadeInTimeout.NormalizedTime;
                if (fadeInTimeout.Tick(Time.deltaTime)) {
                    harvestFillIndicator.alpha = 1.0f;
                    fadeInTimeout.Reset();
                }
            }

            if (fadeOutTimeout.running) {
                harvestFillIndicator.alpha = 1.0f - fadeOutTimeout.NormalizedTime;
                if (fadeOutTimeout.Tick(Time.deltaTime)) {
                    harvestFillIndicator.alpha = 0.0f;
                    fadeOutTimeout.Reset();
                }
            }

        }


    }
}