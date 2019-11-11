using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HammyFarming.Brian.UI {

    public class VersionUpdater: MonoBehaviour {

        void Start () {
            GetComponent<TextMeshProUGUI>().text = Application.version;
        }
    }
}