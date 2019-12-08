using UnityEngine;

namespace HammyFarming.Brian.Animation {
    public class FaceCamera: MonoBehaviour {
        private void LateUpdate () {
            if (Director.LevelCamera) {
                transform.LookAt(Director.LevelCamera.transform);
            }
        }
    }
}