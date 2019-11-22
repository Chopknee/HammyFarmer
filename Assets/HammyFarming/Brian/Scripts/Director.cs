using Hammy;
using HammyFarming.Brian.Sound;
using HammyFarming.Brian.UI;
using HammyFarming.Brian.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

namespace HammyFarming.Brian {

    public class Director: MonoBehaviour {

        public static Director Instance;

        public static GameObject HammyGameObject;

        public delegate void SiloFillChanged ( float value );
        public SiloFillChanged OnSiloFillChanged;

        public delegate void LevelStarted ();
        public LevelStarted OnLevelStarted;

        [Header("Goal and Growth Settings")]
        [Tooltip("How much does hammy need to collect for the level to be 'complete'?")]
        public float SiloFillGoal = 100;
        [Tooltip("How much is a full-grown plant worth when dropped at the solo?")]
        public float FullGrowthScore = 5;

        [Header("Background Music Settings")]

        public float musicFadeTime = 2;

        public bool fadeBackgroundMusic = true;

        public string backgroundMusicTag = "BGM";

        [Header("Other")]
        public string hammyBaseSceneName = "HammyBase";

        public string LevelMusicCredit = "FILL ME IN";



        float _siloFill;
        public float SiloFillLevel {
            get {
                return _siloFill;
            }
            set {
                _siloFill = value;
                OnSiloFillChanged?.Invoke(value);
            }
        }

        public List<SoundType> audioSources;

        //Dictionary<AudioSource, float> audioSourcesAndTargetVolumes;
        Timeout audioFadeIn;
        Timeout audioFadeOut;

        void Awake () {

            Instance = this;

            //Figure out what level should be loaded
            //Try to load the basic components stuff
            StartCoroutine(LoadSceneBase());
            
            audioSources = new List<SoundType>();
            if (fadeBackgroundMusic) {
                audioFadeIn = new Timeout(musicFadeTime, true);
                audioFadeOut = new Timeout(musicFadeTime, false);
                
                //An expensive search for all possible sound things!
                foreach (GameObject go in GameObject.FindGameObjectsWithTag(backgroundMusicTag)) {
                    SoundType st = go.GetComponent<SoundType>();
                    if (st != null) {
                        audioSources.Add(st);
                        st.FadeAmount = 0;
                    }
                }

                audioFadeIn.OnAlarm += FadeInDone;
            }
        }

        IEnumerator LoadSceneBase () {
            //This is the base secen with all content that should be included with a level
            AsyncOperation levelBaseLoader = SceneManager.LoadSceneAsync(hammyBaseSceneName, LoadSceneMode.Additive);

            while (!levelBaseLoader.isDone) {
                yield return null;
            }

            //Putting the hammy object at any object tagged with spawn point.
            HammyGameObject = GameObject.FindGameObjectWithTag("HammyBall");

            GameObject go = GameObject.FindGameObjectWithTag("SpawnPoint");
            if (go != null) {
                HammyGameObject.transform.position = go.transform.position;
            }

            OnLevelStarted?.Invoke();
        }

        private void Update () {
            audioFadeIn.Tick(Time.deltaTime);
            if (audioFadeIn.running) {
                foreach (SoundType st in audioSources) {
                    st.FadeAmount = Mathf.Lerp(0, 1, audioFadeIn.percentComplete);
                }
            }

            audioFadeOut.Tick(Time.deltaTime);
            if (audioFadeOut.running) {
                foreach (SoundType st in audioSources) {
                    st.FadeAmount = Mathf.Lerp(1, 0, audioFadeIn.percentComplete);
                }
            }
        }

        void FadeInDone(float t) {
            foreach (SoundType st in audioSources) {
                st.FadeAmount = 1;
            }
        }

        void FadeOutDone(float t) {

        }

        public static void SetScene ( int sceneIndex ) {
            GameObject go = GameObject.FindGameObjectWithTag("LevelBlackout");
            if (go != null) {
                //Do a fade of the blackout.
                sfi = go.GetComponent<SceneFadeIn>();
                if (sfi != null) {
                    sfi.StartFade();
                }
            }
            HammyFarming.Brian.Base.PlayerInput.SetHammyControlsEnabled(false);
            if (Instance != null) {
                Instance.audioFadeOut.Start();
            }

            loadSceneIndex = sceneIndex;

            if (Instance == null) {
                Instance = new GameObject("FakeDirector").AddComponent<Director>();

            }
            Instance.StartCoroutine(LoadLevel());
        }

        static int loadSceneIndex = 0;
        static SceneFadeIn sfi;

        private static IEnumerator LoadLevel() {

            if (sfi != null) {
                Debug.Log("Waiting for fadeout.");
                while (!sfi.isFinished) {
                    yield return null;
                }
            }

            AsyncOperation levelLoad = SceneManager.LoadSceneAsync(loadSceneIndex);
            levelLoad.allowSceneActivation = false;
            Debug.Log("Trying to load scene.");
            while (!levelLoad.isDone) {
                
                if (levelLoad.progress >= 0.9f) {
                    Debug.Log("Scene loaded, waiting for short period.");
                    yield return new WaitForSeconds(1f);
                    Debug.Log("Allowing scene to load!.");
                    levelLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
