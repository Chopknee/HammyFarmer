using HammyFarming.Brian.Utils.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.UI {
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneFadeIn: MonoBehaviour {

        public AnimationCurve curve;
        public float animationTime = 1f;
        CanvasGroup cg;


        Timeout fadeTimeout;

        [HideInInspector]
        public bool isFinished = false;
        public bool startOnAwake = true;
        public bool setControls = true;
        public bool disableWhenFinished = false;

        private void Awake () {

            cg = GetComponent<CanvasGroup>();
            fadeTimeout = new Timeout(animationTime, startOnAwake);
        }

        public void StartFade() {
            fadeTimeout.Start();
        }

        void Update () {

            if (fadeTimeout.Tick(Time.deltaTime)) {
                cg.alpha = curve.Evaluate(Mathf.Lerp(0, 1, 1));
                isFinished = true;
                if (setControls) {
                    HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(true);
                }
                if (disableWhenFinished) {
                    gameObject.SetActive(false);
                }
            }

            if (fadeTimeout.running) {
                cg.alpha = curve.Evaluate(Mathf.Lerp(0, 1, fadeTimeout.NormalizedTime));
            }
        }
    }
}