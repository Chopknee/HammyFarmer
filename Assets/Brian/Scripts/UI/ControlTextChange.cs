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

            SetTextPrompt(HammyFarming.Brian.GameManagement.PlayerInput.CurrentControlDevice);
            HammyFarming.Brian.GameManagement.PlayerInput.OnControlDeviceChanged += SetTextPrompt;
        }

        private void OnEnable () {
            if (tmproGUI != null) {
                SetTextPrompt(HammyFarming.Brian.GameManagement.PlayerInput.CurrentControlDevice);
            }
        }

        private void OnDestroy () {
            HammyFarming.Brian.GameManagement.PlayerInput.OnControlDeviceChanged -= SetTextPrompt;
        }

        void SetTextPrompt ( HammyFarming.Brian.GameManagement.PlayerInput.ControlDevice device ) {
            switch (device) {
                case HammyFarming.Brian.GameManagement.PlayerInput.ControlDevice.Gamepad:
                    tmproGUI.text = GamepadText;
                    break;
                case HammyFarming.Brian.GameManagement.PlayerInput.ControlDevice.Keyboard:
                    tmproGUI.text = KeyboardText;
                    break;
            }
        }
    }
}