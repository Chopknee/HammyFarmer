using HammyFarming.Brian.GameManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HammyFarming.Brian.Interaction {

    public class ExitTube: MonoBehaviour {

        public int sceneToLoad = -1;
        public bool reloadSceneIfNotSet;

        public GameObject tubeBlocker;

        public Color fadeColor;
        public float fadeTime;
        public AnimationCurve fadeCurve;

        public void Start () {
            if (tubeBlocker != null) {
                if (sceneToLoad == -1 && !reloadSceneIfNotSet) {
                    tubeBlocker.SetActive(true);
                    ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
                    if (ps != null) {
                        ps.Stop();
                    }
                } else {
                    tubeBlocker.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter ( Collider other ) {
            if (other.gameObject.CompareTag("HammyBall")) {
                if (sceneToLoad != -1 || reloadSceneIfNotSet) {
                    if (sceneToLoad != -1) {
                        //Director.SetScene(sceneToLoad);
                        LevelManagement.Instance.LoadLevel(sceneToLoad);
                    } else {
                        //Director.SetScene(SceneManager.GetActiveScene().buildIndex);
                        LevelManagement.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
        }

        void OnFadeComplete () {


        }
    }
}