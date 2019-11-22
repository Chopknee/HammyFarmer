using HammyFarming.Brian.Base;
using TMPro;
using UnityEngine;

namespace HammyFarming.Brian.UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ControlTextChange: MonoBehaviour {

        public string GamepadText;
        public string KeyboardText;

        TextMeshProUGUI tmproGUI;

        void Start () {
            tmproGUI = GetComponent<TextMeshProUGUI>();

            SetTextPrompt(PlayerInput.CurrentControlDevice);
            PlayerInput.OnControlDeviceChanged += SetTextPrompt;
        }

        private void OnEnable () {
            if (tmproGUI != null) {
                SetTextPrompt(PlayerInput.CurrentControlDevice);
            }
        }

        private void OnDestroy () {
            PlayerInput.OnControlDeviceChanged -= SetTextPrompt;
        }

        void SetTextPrompt ( PlayerInput.ControlDevice device ) {
            switch (device) {
                case PlayerInput.ControlDevice.Gamepad:
                    tmproGUI.text = GamepadText;
                    break;
                case PlayerInput.ControlDevice.Keyboard:
                    tmproGUI.text = KeyboardText;
                    break;
            }
        }
    }
}