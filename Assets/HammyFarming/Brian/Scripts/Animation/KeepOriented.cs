using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Animation {

    public class KeepOriented: MonoBehaviour {

        [Tooltip("Axis to keep the up aligned with.")]
        public Vector3 up = new Vector3(0, 1, 0);

        private void FixedUpdate () {
            transform.up = up;
        }
    }
}
