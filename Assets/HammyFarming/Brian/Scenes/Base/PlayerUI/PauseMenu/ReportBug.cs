using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HammyFarming.Brian.Base.PlayerUI.PauseMenu {

    public class ReportBug : MonoBehaviour {

        public Button activateButton;
        public string URL = "";

        void Start() {
            activateButton.onClick.AddListener(OpenReportURL);
        }

        
        void OpenReportURL() {
            if (URL != "") {
                Application.OpenURL(URL);
            }
        }
    }
}