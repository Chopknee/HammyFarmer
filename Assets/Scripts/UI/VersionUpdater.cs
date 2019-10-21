using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionUpdater : MonoBehaviour {

    void Start() {
        GetComponent<Text>().text = Application.version;
    }
}
