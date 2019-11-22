using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.UI {

    public class GameStarter: MonoBehaviour {
        
        void Start () {
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.MainMenuStart.performed += DoThing;
        }

        void DoThing(InputAction.CallbackContext context) {
            //Load the scene here
            Director.SetScene(1);
        }

        private void OnDestroy () {
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.MainMenuStart.performed -= DoThing;
        }
    }
}