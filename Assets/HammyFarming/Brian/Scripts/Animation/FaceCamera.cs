using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Animation {

    public class FaceCamera: MonoBehaviour {

        private void LateUpdate () {
            if (Camera.main != null) {
                transform.LookAt(Camera.main.transform);
            }
        }
    }
}