using HammyFarming.Brian.Utils.Timing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Scenes.HammyOpenScene {

    public class AlphaDisclaimer : MonoBehaviour {

        public float holdTime = 0.15f;
        public float fadeoutTime = 0.75f;

        UnityEngine.UI.Image fillImage;
        CanvasGroup cg;

        Timeout holdTimeout;
        Timeout fadeTimeout;

        void Awake() {
            fillImage = transform.Find("Panel/FillImage").GetComponent<UnityEngine.UI.Image>();
            fillImage.fillAmount = 1;

            cg = GetComponent<CanvasGroup>();

            holdTimeout = new Timeout(holdTime, false);
            fadeTimeout = new Timeout(fadeoutTime, false);

            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.MainMenuStart.Disable();
        }

        private void Update () {
            float btn1 = HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.GamepadAnyButton.ReadValue<float>();
            float btn2 = HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.KeyboardAnyButton.ReadValue<float>();

            //For holding the button
            if (btn1 > 0.0f || btn2 > 0.0f && !holdTimeout.running) {
                holdTimeout.Start();
            } else {
                if (!fadeTimeout.running) {
                    holdTimeout.Reset();
                    fillImage.fillAmount = 1.0f;
                }
            }

            if (holdTimeout.running) {
                fillImage.fillAmount = 1.0f - holdTimeout.NormalizedTime;
            }

            if (holdTimeout.Tick(Time.deltaTime)) {
                fadeTimeout.Start();
            }

            //For fading the screen out.
            if (fadeTimeout.running) {
                cg.alpha = 1 - fadeTimeout.NormalizedTime;
            }

            if (fadeTimeout.Tick(Time.deltaTime)) {
                gameObject.SetActive(false);
                HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.MainMenuStart.Enable();
            }
        }
    }

}