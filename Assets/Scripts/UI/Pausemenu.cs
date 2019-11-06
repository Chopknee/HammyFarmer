using Hammy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausemenu: MonoBehaviour {

    public static bool VerticalInverted { get; private set; }
    public static bool HorizontalInverted { get; private set; }
    public static Pausemenu Instance { get; private set; }

    static InputMaster _inputMaster;
    public static InputMaster InputMasterController {
        get {
            if (_inputMaster == null) {
                _inputMaster = new InputMaster();
            }
            return _inputMaster;
        }
    }

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

        _inputMaster = new InputMaster();

        InputMasterController.Enable();
        InputMasterController.Hammy.Pause.performed += context => OnPausePressed();
    }

    void Start () {
        VerticalInverted = PlayerPrefs.GetInt("VerticalToggle", 0) == 1;
        InvertVerticalToggle.isOn = VerticalInverted;
        HorizontalInverted = PlayerPrefs.GetInt("HorizontalToggle", 0) == 1;
        InvertHorizontalToggle.isOn = HorizontalInverted;


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

    void OnPausePressed() {
        if (!showing) {
            Show();
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Hide();
        }
    }

    void verticalToggleChanged(bool value) {
        PlayerPrefs.SetInt("VerticalToggle", ( value ) ? 1 : 0);
        VerticalInverted = value;
    }

    void horizontalToggleChanged(bool value) {
        PlayerPrefs.SetInt("HorizontalToggle", ( value ) ? 1 : 0);
        HorizontalInverted = value;
    }

    void quit() {
        Application.Quit();
    }

    void resume() {
        Hide();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void restart() {
        Hide();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void returnHub() {
        Hide();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(0);
    }

    void Show() {
        if (group == null) {
            group = GetComponent<CanvasGroup>();
        }
        SetControlsEnabled(false);
        group.interactable = true;
        group.blocksRaycasts = true;
        group.alpha = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        showing = true;
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }

    void Hide() {
        if (group == null) {
            group = GetComponent<CanvasGroup>();
        }
        SetControlsEnabled(true);
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

    void SetControlsEnabled(bool enabled) {
        if (enabled) {
            InputMasterController.Hammy.Attach.Enable();
            InputMasterController.Hammy.Roll.Enable();
            InputMasterController.Hammy.Use.Enable();
            InputMasterController.Hammy.Jump.Enable();
            InputMasterController.Hammy.Zoom.Enable();
            InputMasterController.Hammy.Look.Enable();
        } else {
            InputMasterController.Hammy.Attach.Disable();
            InputMasterController.Hammy.Roll.Disable();
            InputMasterController.Hammy.Use.Disable();
            InputMasterController.Hammy.Jump.Disable();
            InputMasterController.Hammy.Zoom.Disable();
            InputMasterController.Hammy.Look.Disable();
        }
    }
}
