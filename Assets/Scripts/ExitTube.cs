using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (sceneToLoad != -1 || reloadSceneIfNotSet) {
            UIFade.DoFade(fadeTime, OnFadeComplete, fadeColor, fadeCurve);
        }
    }

    void OnFadeComplete() {
        
        if (sceneToLoad != -1) {
            SceneManager.LoadScene(sceneToLoad);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
