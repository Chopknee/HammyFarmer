using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.UI {

    public class AlphaDisclaimer : MonoBehaviour {

        static bool hasShown = false;

        void Start() {
            if (hasShown) {
                gameObject.SetActive(false);
                hasShown = true;
                return;
            }

            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.GamepadAnyButton.performed += AnyActionPerformed;
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.KeyboardAnyButton.performed += AnyActionPerformed;
            hasShown = true;
            HammyFarming.Brian.Base.PlayerInput.SetHammyControlsEnabled(false);
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.MainMenuStart.Disable();
        }

        void AnyActionPerformed(InputAction.CallbackContext context) {
            gameObject.SetActive(false);
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.GamepadAnyButton.performed -= AnyActionPerformed;
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.KeyboardAnyButton.performed -= AnyActionPerformed;
            HammyFarming.Brian.Base.PlayerInput.SetHammyControlsEnabled(true);
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.MainMenuStart.Enable();
        }
    }

}