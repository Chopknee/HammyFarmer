using HammyFarming.Brian.Sound;
using HammyFarming.Brian.Utils.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HammyFarming.Brian.GameManagement {

    public class LevelManagement: MonoBehaviour {

        public static LevelManagement Instance { get; private set; }

        private string LoadingBlackoutAssetPath = "Prefabs/Scenes/LoadingBlackout";

        private int loadSceneIndex = 0;

        private Timeout levelLoadTimeout;

        //This runs after the level loads, but before the player is spawned
        public delegate void LevelAwakeDelegate ();
        public static LevelAwakeDelegate OnLevelAwake;

        //This runs after the level load and after on level awake runs
        public delegate void LevelAwakeLateDelegate ();
        public static LevelAwakeLateDelegate OnLevelAwakeLate;

        //This runs after the level is started
        public delegate void LevelStartDelegate ();
        public static LevelStartDelegate OnLevelStart;

        public static bool IsLevelLoaded { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize() {
            Debug.Log("Level management initialized.");
            Instance = new GameObject("LevelManager").AddComponent<LevelManagement>();
        }

        private void Awake () {
            DontDestroyOnLoad(gameObject);
            levelLoadTimeout = new Timeout(1);
        }

        public void LoadLevel ( int sceneIndex ) {

            OnLevelAwake = null;
            OnLevelAwakeLate = null;
            OnLevelStart = null;

            GameObject loadingBlackout = Instantiate(Resources.Load<GameObject>(LoadingBlackoutAssetPath));
            loadingBlackout.transform.SetParent(transform);
            loadingBlackoutCG = loadingBlackout.GetComponent<CanvasGroup>();
            

            loadSceneIndex = sceneIndex;

            StartCoroutine(LoadLevelOperation());
        }

        CanvasGroup loadingBlackoutCG;

        private IEnumerator LoadLevelOperation () {
            IsLevelLoaded = false;
            Debug.Log("Waiting for fade to complete...");

            levelLoadTimeout.Start();

            while (!levelLoadTimeout.Tick(Time.deltaTime)) {
                loadingBlackoutCG.alpha = levelLoadTimeout.NormalizedTime;
                if (LevelSound.Instance != null) {
                    LevelSound.Instance.SetSourcesVolume(1 - levelLoadTimeout.NormalizedTime);
                }
                yield return null;
            }

            levelLoadTimeout.Reset();
            loadingBlackoutCG.alpha = 1;

            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(loadSceneIndex);
            levelLoad.allowSceneActivation = false;
            Debug.Log("Trying to load scene.");
            while (!levelLoad.isDone) {

                if (levelLoad.progress >= 0.9f) {
                    //Debug.Log("Scene loaded, waiting for short period.");

                    yield return null;

                    //Debug.Log("Allowing scene to load!.");
                    levelLoad.allowSceneActivation = true;
                }
            }

            yield return null;
            //Wait a frame for hopefully everything in the level to run it's awake.

            levelLoadTimeout.Start();
            while (!levelLoadTimeout.Tick(Time.deltaTime)) {
                loadingBlackoutCG.alpha = 1 - levelLoadTimeout.NormalizedTime;
                yield return null;
            }

            levelLoadTimeout.Reset();

            Destroy(loadingBlackoutCG.gameObject);

            //Wait a frame for everything to awake.
            yield return new WaitForEndOfFrame();

            OnLevelAwake?.Invoke();
            OnLevelAwakeLate?.Invoke();

            IsLevelLoaded = true;

        }

    }
}
