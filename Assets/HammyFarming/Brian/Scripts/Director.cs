using Hammy;
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

        public enum ControlDevice { Keyboard, Gamepad };

        public ControlDevice CurrentControlDevice;

        public delegate void ControlDeviceChanged ( ControlDevice device );
        public ControlDeviceChanged OnControlDeviceChanged;

        public string hammyBaseSceneName;

        static InputMaster _inputMaster;
        public static InputMaster InputMasterController {
            get {
                if (_inputMaster == null) {
                    _inputMaster = new InputMaster();
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

        public float SiloFillGoal = 100;
        public float FullGrowthScore = 5;

        void Awake () {
            //Prevent multiple director instances from existing.
            if (Instance != null) {
                Destroy(this);
                return;
            }

            //Figure out what level should be loaded
            //Try to load the basic components stuff
            StartCoroutine(LoadSceneBase());

            _inputMaster = new InputMaster();

            InputMasterController.Enable();
            CurrentControlDevice = ControlDevice.Gamepad;

            _inputMaster.InputDevice.Keyboard.performed += OnKeyboardUsed;
            _inputMaster.InputDevice.Gamepad.performed += OnGamepadUsed;
        }

        void OnKeyboardUsed(InputAction.CallbackContext context) {
            if (CurrentControlDevice != ControlDevice.Keyboard) {
                CurrentControlDevice = ControlDevice.Keyboard;
                OnControlDeviceChanged?.Invoke(CurrentControlDevice);
            }
        }

        void OnGamepadUsed(InputAction.CallbackContext context) {
            if (CurrentControlDevice != ControlDevice.Gamepad) {
                CurrentControlDevice = ControlDevice.Gamepad;
                OnControlDeviceChanged?.Invoke(CurrentControlDevice);
            }
        }

        public void Start () {
            Instance = this;
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
        }

        public static void SetControlsEnabled ( bool enabled ) {
            if (enabled) {
                InputMasterController.Hammy.Attach.Enable();
                InputMasterController.Hammy.Roll.Enable();
                InputMasterController.Hammy.Use.Enable();
                InputMasterController.Hammy.Jump.Enable();
                InputMasterController.Hammy.Zoom.Enable();
                InputMasterController.Hammy.Look.Enable();
            } else {
                InputMasterController.Hammy.Attach.Disable();
                InputMasterController.Hammy.Roll.Disable();
                InputMasterController.Hammy.Use.Disable();
                InputMasterController.Hammy.Jump.Disable();
                InputMasterController.Hammy.Zoom.Disable();
                InputMasterController.Hammy.Look.Disable();
            }
        }

        private void OnDestroy () {
            _inputMaster.InputDevice.Keyboard.performed -= OnKeyboardUsed;
            _inputMaster.InputDevice.Gamepad.performed -= OnGamepadUsed;
        }
    }
}
