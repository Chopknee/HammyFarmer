using HammyFarming.Brian.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Scenes.HammyOpenScene {

    public class GameStarter: MonoBehaviour {
        
        void Awake () {
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.MainMenuStart.performed += DoThing;
        }

        void DoThing(InputAction.CallbackContext context) {
            //Load the scene here
            LevelManagement.Instance.LoadLevel(1);
            //Director.SetScene(1);
        }

        private void OnDestroy () {
            HammyFarming.Brian.Base.PlayerInput.ControlMaster.InputDevice.MainMenuStart.performed -= DoThing;
        }
    }
}