using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCapturing: MonoBehaviour {


    bool cursorIsCaptured = false;
    void Start () {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update () {
        if (Application.isFocused) {
            if (!cursorIsCaptured) {
                Cursor.lockState = CursorLockMode.Locked;
                cursorIsCaptured = true;
            }
        } else {
            if (cursorIsCaptured) {
                Cursor.lockState = CursorLockMode.None;
                cursorIsCaptured = false;
            }
        }
    }
}
