using Hammy;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.GameManagement {

    public class PlayerInput {

        private static InputMaster _inputMaster;
        public static InputMaster ControlMaster {
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

        public enum ControlDevice { Keyboard, Gamepad };
        public static ControlDevice CurrentControlDevice;

        public delegate void ControlDeviceChanged ( ControlDevice device );
        public static ControlDeviceChanged OnControlDeviceChanged;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        void Initialize () {
            ControlMaster.InputDevice.KeyboardAny.performed += OnKeyboardUsed;
            ControlMaster.InputDevice.GamepadAny.performed += OnGamepadUsed;
            CurrentControlDevice = ControlDevice.Gamepad;
        }

        public static void SetHammyControlsEnabled ( bool enabled ) {
            if (enabled) {
                ControlMaster.Hammy.Attach.Enable();
                ControlMaster.Hammy.Roll.Enable();
                ControlMaster.Hammy.Use.Enable();
                ControlMaster.Hammy.Jump.Enable();
                ControlMaster.Camera.Zoom.Enable();
                ControlMaster.Camera.Look.Enable();
            } else {
                ControlMaster.Hammy.Attach.Disable();
                ControlMaster.Hammy.Roll.Disable();
                ControlMaster.Hammy.Use.Disable();
                ControlMaster.Hammy.Jump.Disable();
                ControlMaster.Camera.Zoom.Disable();
                ControlMaster.Camera.Look.Disable();
            }
        }

        static void OnKeyboardUsed ( InputAction.CallbackContext context ) {
            if (CurrentControlDevice != ControlDevice.Keyboard) {
                CurrentControlDevice = ControlDevice.Keyboard;
                OnControlDeviceChanged?.Invoke(CurrentControlDevice);
            }
        }

        static void OnGamepadUsed ( InputAction.CallbackContext context ) {
            if (CurrentControlDevice != ControlDevice.Gamepad) {
                CurrentControlDevice = ControlDevice.Gamepad;
                OnControlDeviceChanged?.Invoke(CurrentControlDevice);
            }
        }
    }
}
