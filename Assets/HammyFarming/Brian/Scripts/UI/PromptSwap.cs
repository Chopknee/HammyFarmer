using HammyFarming.Brian;
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

        if (Director.Instance != null) {
            subscribed = true;
            Director.Instance.OnControlDeviceChanged += OnControlSchemeChanged;
        }

    }

    void Update () {
        if (!subscribed && Director.Instance != null) {
            subscribed = true;
            Director.Instance.OnControlDeviceChanged += OnControlSchemeChanged;
        }
    }

    private void OnDestroy () {
        if (subscribed) {
            Director.Instance.OnControlDeviceChanged -= OnControlSchemeChanged;
        }
    }

    void OnControlSchemeChanged(Director.ControlDevice device) {
        switch (device) {
            case Director.ControlDevice.Gamepad:
                sr.sprite = GamepadPromptSprite;
                break;
            case Director.ControlDevice.Keyboard:
                sr.sprite = KeyboardPromptSprite;
                break;
        }
    }

    private void OnEnable () {
        if (Director.Instance != null) {
            OnControlSchemeChanged(Director.Instance.CurrentControlDevice);
        }
    }
}
