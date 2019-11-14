using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.UI {

    public class AlphaDisclaimer : MonoBehaviour {

        static bool hasShown = false;

        void Start() {
            if (hasShown || !Director.Instance.showAlphaDisclaimer) {
                gameObject.SetActive(false);
                hasShown = true;
                return;
            }

            Director.InputMasterController.InputDevice.GamepadAnyButton.performed += AnyActionPerformed;
            Director.InputMasterController.InputDevice.KeyboardAnyButton.performed += AnyActionPerformed;
            hasShown = true;
            Director.SetControlsEnabled(false);
        }

        void AnyActionPerformed(InputAction.CallbackContext context) {
            gameObject.SetActive(false);
            Director.InputMasterController.InputDevice.GamepadAnyButton.performed -= AnyActionPerformed;
            Director.InputMasterController.InputDevice.KeyboardAnyButton.performed -= AnyActionPerformed;
            Director.SetControlsEnabled(true);
        }
    }

}