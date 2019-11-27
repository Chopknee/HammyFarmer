using UnityEngine;

namespace HammyFarming.Brian.Animation {

    public class FaceCamera: MonoBehaviour {

        private void LateUpdate () {
            if (UnityEngine.Camera.main != null) {
                transform.LookAt(UnityEngine.Camera.main.transform);
            }
        }
    }
}