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

        public Button quitButton;
        public Button resumeButton;
        public Button resetButton;
        public Button returnToHubButton;
        public Button resetHammyButton;

        private CanvasGroup group;
        public bool showing = false;

        private HammyFarming.Brian.Utils.Timing.Timeout transitionTimeout;
        private float direction;

        private RectTransform optionsPanel = null;
        private Vector3 optsEnabledPos;
        private Vector3 optsDisabledPos;

        private RectTransform gameTitle = null;
        private Vector3 titleEnabledPos;
        private Vector3 titleDisabledPos;

        private RectTransform levelMusicCredit = null;
        private Vector3 musicCreditEnabledPos;
        private Vector3 musicCreditDisabledPos;

        private RectTransform companyLogo = null;
        private Vector3 logoEnabledPos;
        private Vector3 logoDisabledPos;

        private RectTransform cover;

        private void Awake () {
            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Hammy.Pause.performed += OnPausePressed;

            quitButton.onClick.AddListener(quit);
            resumeButton.onClick.AddListener(resume);
            resetButton.onClick.AddListener(restart);
            returnToHubButton.onClick.AddListener(returnHub);
            resetHammyButton.onClick.AddListener(ResetHammy);

            group = GetComponent<CanvasGroup>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Set resume to being selected
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);

            transitionTimeout = new Brian.Utils.Timing.Timeout(1, false);

            cover = GetComponent<RectTransform>();

            optionsPanel = transform.Find("Options Panel").GetComponent<RectTransform>();
            optsEnabledPos = optionsPanel.localPosition;
            optsDisabledPos = new Vector3(-2000.0f, 2000.0f, 0.0f);

            gameTitle = transform.Find("GameTitle").GetComponent<RectTransform>();
            titleEnabledPos = gameTitle.localPosition;
            titleDisabledPos = new Vector3(2000.0f, 2000.0f, 0.0f);

            levelMusicCredit = transform.Find("LevelMusicCredit").GetComponent<RectTransform>();
            musicCreditEnabledPos = levelMusicCredit.localPosition;
            musicCreditDisabledPos = new Vector3(-2000.0f, -2000.0f, 0.0f);

            companyLogo = transform.Find("CompanyLogo").GetComponent<RectTransform>();
            logoEnabledPos = companyLogo.localPosition;
            logoDisabledPos = new Vector3(2000.0f, -2000.0f, 0.0f);


            //Hide();
            Hide();
            transitionTimeout.currentTime = 0.95f;
            Transition(0);
        }

        private void Update () {
            if (transitionTimeout.running) {
                float a = transitionTimeout.NormalizedTime;
                Transition(( direction == 1 ) ? a : 1 - a);

                if (transitionTimeout.Tick(Time.deltaTime)) {
                    Transition(( direction == 1 ) ? 1 : 0);
                    transitionTimeout.Reset();
                }
            }
        }

        void Transition(float a) {

            a = HammyFarming.Brian.Utils.Easing.EaseInOutCirc(a);

            optionsPanel.localPosition = Vector3.Lerp(optsDisabledPos, optsEnabledPos, a);
            gameTitle.localPosition = Vector3.Lerp(titleDisabledPos, titleEnabledPos, a);
            levelMusicCredit.localPosition = Vector3.Lerp(musicCreditDisabledPos, musicCreditEnabledPos, a);
            companyLogo.localPosition = Vector3.Lerp(logoDisabledPos, logoEnabledPos, a);
            group.alpha = a;

            if (a == 1) {
                group.interactable = true;
                EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
            } else {
                if (group.interactable)
                    group.interactable = false;
            }

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

        void ResetHammy() {
            HammyFarming.Brian.Director.Hammy.transform.position = HammyFarming.Brian.Director.Instance.HammyResetPosition;
            Hide();
        }

        void Show () {
            transitionTimeout.Start();
            direction = 1;
            if (group == null) {
                group = GetComponent<CanvasGroup>();
            }

            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(false);
            group.blocksRaycasts = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            showing = true;
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        }

        void Hide () {
            transitionTimeout.Start();
            direction = -1;
            if (group == null) {
                group = GetComponent<CanvasGroup>();
            }
            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(true);
            group.blocksRaycasts = false;
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