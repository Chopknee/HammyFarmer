using Hammy;
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

        public delegate void ControlDeviceChanged ( ControlDevice device );
        public static ControlDeviceChanged OnControlDeviceChanged;

        public delegate void LevelStarted ();
        public LevelStarted OnLevelStarted;

        public enum ControlDevice { Keyboard, Gamepad };

        [Header("Goal and Growth Settings")]
        [Tooltip("How much does hammy need to collect for the level to be 'complete'?")]
        public float SiloFillGoal = 100;
        [Tooltip("How much is a full-grown plant worth when dropped at the solo?")]
        public float FullGrowthScore = 5;

        [Header("Background Music Settings")]

        public float musicFadeTime = 2;

        public bool fadeBackgroundMusic = true;

        public string backgroundMusicTag = "BGM";

        public static ControlDevice CurrentControlDevice;

        [Header("Other")]
        public string hammyBaseSceneName;

        public string LevelMusicCredit = "FILL ME IN";

        private static InputMaster _inputMaster;
        public static InputMaster InputMasterController {
            get {
                if (_inputMaster == null) {
                    _inputMaster = new InputMaster();
                    _inputMaster.Enable();
                    _inputMaster.InputDevice.KeyboardAny.performed += OnKeyboardUsed;
                    _inputMaster.InputDevice.GamepadAny.performed += OnGamepadUsed;
                }
                return _inputMaster;
            }
        }

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

        Dictionary<AudioSource, float> audioSourcesAndTargetVolumes;
        Timeout audioFadeIn;
        Timeout audioFadeOut;

        void Awake () {

            //Figure out what level should be loaded
            //Try to load the basic components stuff
            StartCoroutine(LoadSceneBase());
            CurrentControlDevice = ControlDevice.Gamepad;

            if (fadeBackgroundMusic) {
                audioFadeIn = new Timeout(musicFadeTime, true);
                audioFadeOut = new Timeout(musicFadeTime, false);
                audioSourcesAndTargetVolumes = new Dictionary<AudioSource, float>();

                foreach (GameObject go in GameObject.FindGameObjectsWithTag(backgroundMusicTag)) {
                    AudioSource aus = go.GetComponent<AudioSource>();
                    audioSourcesAndTargetVolumes.Add(aus, aus.volume);
                    aus.volume = 0;
                }

                audioFadeIn.OnAlarm += FadeInDone;
            }

            Instance = this;
        }

        static void OnKeyboardUsed(InputAction.CallbackContext context) {
            if (CurrentControlDevice != ControlDevice.Keyboard) {
                CurrentControlDevice = ControlDevice.Keyboard;
                OnControlDeviceChanged?.Invoke(CurrentControlDevice);
            }
        }

        static void OnGamepadUsed(InputAction.CallbackContext context) {
            if (CurrentControlDevice != ControlDevice.Gamepad) {
                CurrentControlDevice = ControlDevice.Gamepad;
                OnControlDeviceChanged?.Invoke(CurrentControlDevice);
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

        public static void SetControlsEnabled ( bool enabled ) {
            if (enabled) {
                InputMasterController.Hammy.Attach.Enable();
                InputMasterController.Hammy.Roll.Enable();
                InputMasterController.Hammy.Use.Enable();
                InputMasterController.Hammy.Jump.Enable();
                InputMasterController.Camera.Zoom.Enable();
                InputMasterController.Camera.Look.Enable();
            } else {
                InputMasterController.Hammy.Attach.Disable();
                InputMasterController.Hammy.Roll.Disable();
                InputMasterController.Hammy.Use.Disable();
                InputMasterController.Hammy.Jump.Disable();
                InputMasterController.Camera.Zoom.Disable();
                InputMasterController.Camera.Look.Disable();
            }
        }

        private void Update () {
            audioFadeIn.Tick(Time.deltaTime);
            if (audioFadeIn.running) {
                foreach (KeyValuePair<AudioSource, float> auses in audioSourcesAndTargetVolumes) {
                    auses.Key.volume = Mathf.Lerp(0, auses.Value, audioFadeIn.percentComplete);
                }
            }

            audioFadeOut.Tick(Time.deltaTime);
            if (audioFadeOut.running) {
                foreach (KeyValuePair<AudioSource, float> auses in audioSourcesAndTargetVolumes) {
                    auses.Key.volume = Mathf.Lerp(auses.Value, 0, audioFadeOut.percentComplete);
                }
            }
        }

        void FadeInDone(float t) {
            foreach (KeyValuePair<AudioSource, float> auses in audioSourcesAndTargetVolumes) {
                auses.Key.volume = auses.Value;
            }
        }

        void FadeOutDone(float t) {

        }

        private void OnDestroy () {
            InputMasterController.InputDevice.KeyboardAny.performed -= OnKeyboardUsed;
            InputMasterController.InputDevice.GamepadAny.performed -= OnGamepadUsed;
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
            SetControlsEnabled(false);
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
