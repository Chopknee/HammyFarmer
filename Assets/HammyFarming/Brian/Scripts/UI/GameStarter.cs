using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.UI {

    public class GameStarter: MonoBehaviour {
        
        void Start () {
            Director.InputMasterController.InputDevice.MainMenuStart.performed += DoThing;
        }

        void DoThing(InputAction.CallbackContext context) {
            //Load the scene here
            Director.SetScene(1);
        }

        private void OnDestroy () {
            Director.InputMasterController.InputDevice.MainMenuStart.performed -= DoThing;
        }
    }
}