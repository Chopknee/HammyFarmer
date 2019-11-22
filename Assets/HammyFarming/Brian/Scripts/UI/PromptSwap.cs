using HammyFarming.Brian;
using HammyFarming.Brian.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptSwap: MonoBehaviour {

    public Sprite KeyboardPromptSprite;
    public Sprite GamepadPromptSprite;

    bool subscribed = false;

    SpriteRenderer sr;

    private void Awake () {
        sr = GetComponent<SpriteRenderer>();
        subscribed = true;
        PlayerInput.OnControlDeviceChanged += OnControlSchemeChanged;

    }

    void Update () {
        if (!subscribed) {
            subscribed = true;
            PlayerInput.OnControlDeviceChanged += OnControlSchemeChanged;
        }
    }

    private void OnDestroy () {
        if (subscribed) {
            PlayerInput.OnControlDeviceChanged -= OnControlSchemeChanged;
        }
    }

    void OnControlSchemeChanged(PlayerInput.ControlDevice device) {
        switch (device) {
            case PlayerInput.ControlDevice.Gamepad:
                sr.sprite = GamepadPromptSprite;
                break;
            case PlayerInput.ControlDevice.Keyboard:
                sr.sprite = KeyboardPromptSprite;
                break;
        }
    }

    private void OnEnable () {
        OnControlSchemeChanged(PlayerInput.CurrentControlDevice);
    }
}
