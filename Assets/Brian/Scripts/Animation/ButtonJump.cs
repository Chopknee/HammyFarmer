using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.UI {

    public class ButtonJump : MonoBehaviour {

        public AnimationCurve curve;
        public float animationTime = 1f;
        float t = 0;
        public float amplitude = 10f;
        public float startingOffset = 0;

        float sy;
        Vector3 pos;

        private void Awake() {
            sy = transform.position.y;
            pos = transform.position;
            t = startingOffset;
        }

        void Update() {
            t += Time.deltaTime;

            pos.y = sy + amplitude * curve.Evaluate(t / animationTime);
            transform.position = pos;

            if (t >= animationTime) {
                t = 0;
            }
        }

        private void OnValidate () {
            startingOffset = Mathf.Clamp(startingOffset, 0, animationTime);
        }
    }
}