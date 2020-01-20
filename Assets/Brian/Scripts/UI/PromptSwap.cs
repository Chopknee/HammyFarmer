using UnityEngine;

public class PromptSwap: MonoBehaviour {

    public Sprite KeyboardPromptSprite;
    public Sprite GamepadPromptSprite;

    bool subscribed = false;

    SpriteRenderer sr;

    private void Awake () {
        sr = GetComponent<SpriteRenderer>();
        subscribed = true;
        HammyFarming.Brian.GameManagement.PlayerInput.OnControlDeviceChanged += OnControlSchemeChanged;

    }

    void Update () {
        if (!subscribed) {
            subscribed = true;
            HammyFarming.Brian.GameManagement.PlayerInput.OnControlDeviceChanged += OnControlSchemeChanged;
        }
    }

    private void OnDestroy () {
        if (subscribed) {
            HammyFarming.Brian.GameManagement.PlayerInput.OnControlDeviceChanged -= OnControlSchemeChanged;
        }
    }

    void OnControlSchemeChanged( HammyFarming.Brian.GameManagement.PlayerInput.ControlDevice device) {
        switch (device) {
            case HammyFarming.Brian.GameManagement.PlayerInput.ControlDevice.Gamepad:
                sr.sprite = GamepadPromptSprite;
                break;
            case HammyFarming.Brian.GameManagement.PlayerInput.ControlDevice.Keyboard:
                sr.sprite = KeyboardPromptSprite;
                break;
        }
    }

    private void OnEnable () {
        OnControlSchemeChanged(HammyFarming.Brian.GameManagement.PlayerInput.CurrentControlDevice);
    }
}
