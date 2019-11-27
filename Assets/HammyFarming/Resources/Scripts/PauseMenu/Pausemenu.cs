using Hammy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HammyFarming.PauseMenu {

    public class Pausemenu: MonoBehaviour {

        public static Pausemenu Instance { get; private set; }

        public Button quitButton;
        public Button resumeButton;
        public Button resetButton;
        public Button returnToHubButton;

        CanvasGroup group;
        public bool showing = false;

        private void Awake () {
            //Preventing multiple instances from existing!!
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        void Start () {

            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Hammy.Pause.performed += OnPausePressed;

            quitButton.onClick.AddListener(quit);
            resumeButton.onClick.AddListener(resume);
            resetButton.onClick.AddListener(restart);
            returnToHubButton.onClick.AddListener(returnHub);

            group = GetComponent<CanvasGroup>();
            Hide();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Set resume to being selected
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);

        }

        private void OnDestroy () {
            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Hammy.Pause.performed -= OnPausePressed;
        }

        void OnPausePressed (InputAction.CallbackContext context) {
            if (!showing) {
                Show();
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Hide();
            }
        }

        void quit () {
            Application.Quit();
        }

        void resume () {
            Hide();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void restart () {
            Hide();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Director.SetScene(SceneManager.GetActiveScene().buildIndex);
            HammyFarming.Brian.GameManagement.LevelManagement.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        void returnHub () {
            Hide();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            HammyFarming.Brian.GameManagement.LevelManagement.Instance.LoadLevel(1);
        }

        void Show () {
            if (group == null) {
                group = GetComponent<CanvasGroup>();
            }

            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(false);
            group.interactable = true;
            group.blocksRaycasts = true;
            group.alpha = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            showing = true;
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        }

        void Hide () {
            if (group == null) {
                group = GetComponent<CanvasGroup>();
            }
            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(true);
            group.interactable = false;
            group.blocksRaycasts = false;
            group.alpha = 0;
            showing = false;
        }

        private void OnApplicationFocus ( bool focus ) {
            if (!showing) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}