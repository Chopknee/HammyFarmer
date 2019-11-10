using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionUpdater : MonoBehaviour {

    void Start() {
        GetComponent<TextMeshProUGUI>().text = Application.version;
    }
}
