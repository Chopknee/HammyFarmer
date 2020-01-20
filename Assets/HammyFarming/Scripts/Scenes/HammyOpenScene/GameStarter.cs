using HammyFarming.Brian.GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Scenes.HammyOpenScene {

    public class GameStarter: MonoBehaviour {
        
        void Awake () {
            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.MainMenuStart.performed += DoThing;
        }

        void DoThing(InputAction.CallbackContext context) {
            //Load the scene here
            LevelManagement.Instance.LoadLevel(1);
            //Director.SetScene(1);
        }

        private void OnDestroy () {
            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.InputDevice.MainMenuStart.performed -= DoThing;
        }
    }
}