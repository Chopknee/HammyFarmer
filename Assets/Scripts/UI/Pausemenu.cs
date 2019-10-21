using Hammy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pausemenu: MonoBehaviour {

    public static bool VerticalInverted { get; private set; }
    public static bool HorizontalInverted { get; private set; }
    public static bool SMBMode { get; private set; }
    public static Pausemenu Instance { get; private set; }

    public static InputMaster InputMasterController { get; private set; }

    public Toggle InvertVerticalToggle;
    public Toggle InvertHorizontalToggle;
    public Toggle SMBmodeToggle;

    public Button quitButton;
    public Button resumeButton;

    CanvasGroup group;
    public bool showing = false;

    private void Awake () {
        //Preventing multiple instances from existing!!
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        InputMasterController = new InputMaster();

        InputMasterController.Enable();
        InputMasterController.Hammy.Pause.performed += context => OnPausePressed();

        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        VerticalInverted = PlayerPrefs.GetInt("VerticalToggle", 0) == 1;
        InvertVerticalToggle.isOn = VerticalInverted;
        HorizontalInverted = PlayerPrefs.GetInt("HorizontalToggle", 0) == 1;
        InvertHorizontalToggle.isOn = HorizontalInverted;
        SMBMode = PlayerPrefs.GetInt("SMBMode", 0) == 1;
        SMBmodeToggle.isOn = SMBMode;


        InvertVerticalToggle.onValueChanged.AddListener(verticalToggleChanged);
        InvertHorizontalToggle.onValueChanged.AddListener(horizontalToggleChanged);
        SMBmodeToggle.onValueChanged.AddListener(SMBToggle);
        quitButton.onClick.AddListener(quit);
        resumeButton.onClick.AddListener(resume);

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

    void SMBToggle(bool value) {
        PlayerPrefs.SetInt("SMBMode", ( value ) ? 1 : 0);
        SMBMode = value;
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

    void Show() {
        SetControlsEnabled(false);
        group.interactable = true;
        group.blocksRaycasts = true;
        group.alpha = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        showing = true;
    }

    void Hide() {
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
