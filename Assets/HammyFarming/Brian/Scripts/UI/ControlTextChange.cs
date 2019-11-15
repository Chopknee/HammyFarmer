using HammyFarming.Brian;
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

            SetTextPrompt(Director.CurrentControlDevice);
            Director.OnControlDeviceChanged += SetTextPrompt;
        }

        private void OnEnable () {
            if (tmproGUI != null) {
                SetTextPrompt(Director.CurrentControlDevice);
            }
        }

        private void OnDestroy () {
            Director.OnControlDeviceChanged -= SetTextPrompt;
        }

        void SetTextPrompt ( Director.ControlDevice device ) {
            switch (device) {
                case Director.ControlDevice.Gamepad:
                    tmproGUI.text = GamepadText;
                    break;
                case Director.ControlDevice.Keyboard:
                    tmproGUI.text = KeyboardText;
                    break;
            }
        }
    }
}