using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Base.PlayerUI.PauseMenu {
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class MusicCredit : MonoBehaviour {
        
        void Awake() {
            if (Director.Instance != null) {
                GetComponent<TMPro.TextMeshProUGUI>().text = Director.Instance.LevelMusicCredit;
            }
        }
    }
}