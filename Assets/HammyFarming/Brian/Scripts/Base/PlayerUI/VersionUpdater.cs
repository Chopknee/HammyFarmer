using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HammyFarming.Brian.Base.PlayerUI {

    public class VersionUpdater: MonoBehaviour {

        void Awake () {
            GetComponent<TextMeshProUGUI>().text = Application.version;
        }
    }
}