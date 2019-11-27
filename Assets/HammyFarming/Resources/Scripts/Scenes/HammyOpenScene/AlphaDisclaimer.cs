using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Scenes.HammyOpenScene {

    public class AlphaDisclaimer : MonoBehaviour {

        static bool hasShown = false;

        public float holdTime = 0.15f;

        void Start() {
            if (hasShown) {
                gameObject.SetActive(false);
                hasShown = true;
                return;
            }

            hasShown = true;
            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.MainMenuStart.Disable();
        }

        void AnyActionPerformed(InputAction.CallbackContext context) {
            gameObject.SetActive(false);
        }

        float t = 0.0f;

        private void Update () {
            float btn1 = HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.GamepadAnyButton.ReadValue<float>();
            float btn2 = HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.KeyboardAnyButton.ReadValue<float>();

            if (btn1 > 0.0f || btn2 > 0.0f) {
                t+= Time.deltaTime;
                if (t > holdTime) {
                    //Switch to the next scene
                    gameObject.SetActive(false);
                    HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.MainMenuStart.Enable();
                }
            } else {
                t = 0.0f;
            }
        }
    }

}