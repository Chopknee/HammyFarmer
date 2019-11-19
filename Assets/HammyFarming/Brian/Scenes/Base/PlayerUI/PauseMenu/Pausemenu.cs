using Hammy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HammyFarming.Brian.Base.PlayerUI.PauseMenu {

    public class Pausemenu: MonoBehaviour {

        public static bool VerticalInverted { get; private set; }
        public static bool HorizontalInverted { get; private set; }
        public static float VerticalSensitivity { get; private set; }
        public static float HorizontalSensitivity { get; private set; }
        public static float MasterVolume { get; private set; }
        public static float MusicVolume { get; private set; }
        public static float SoundFXVolume { get; private set; }

        public static Pausemenu Instance { get; private set; }



        public Toggle InvertVerticalToggle;
        public Toggle InvertHorizontalToggle;
        public Toggle SMBmodeToggle;

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

            Director.InputMasterController.Hammy.Pause.performed += context => OnPausePressed();

            VerticalInverted = PlayerPrefs.GetInt("VerticalToggle", 0) == 1;
            InvertVerticalToggle.isOn = VerticalInverted;
            HorizontalInverted = PlayerPrefs.GetInt("HorizontalToggle", 0) == 1;
            InvertHorizontalToggle.isOn = HorizontalInverted;

            HorizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity", 1);
            VerticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity", 1);

            MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
            SoundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume", 1);


            InvertVerticalToggle.onValueChanged.AddListener(verticalToggleChanged);
            InvertHorizontalToggle.onValueChanged.AddListener(horizontalToggleChanged);
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

        void OnPausePressed () {
            if (!showing) {
                Show();
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Hide();
            }
        }

        void verticalToggleChanged ( bool value ) {
            PlayerPrefs.SetInt("VerticalToggle", ( value ) ? 1 : 0);
            VerticalInverted = value;
        }

        void horizontalToggleChanged ( bool value ) {
            PlayerPrefs.SetInt("HorizontalToggle", ( value ) ? 1 : 0);
            HorizontalInverted = value;
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
            Director.SetScene(SceneManager.GetActiveScene().buildIndex);
        }

        void returnHub () {
            Hide();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Director.SetScene(1);
        }

        void Show () {
            if (group == null) {
                group = GetComponent<CanvasGroup>();
            }
            Director.SetControlsEnabled(false);
            group.interactable = true;
            group.blocksRaycasts = true;
            group.alpha = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            showing = true;
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        }

        void Hide () {

            HorizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity");
            VerticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity");

            MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
            SoundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume");

            if (group == null) {
                group = GetComponent<CanvasGroup>();
            }
            Director.SetControlsEnabled(true);
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