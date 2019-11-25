using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Base {

    public class CursorCapturing: MonoBehaviour {


        bool cursorIsCaptured = false;
        void Start () {
            Cursor.visible = false;
        }

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
}
