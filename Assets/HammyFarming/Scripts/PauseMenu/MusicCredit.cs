using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.PauseMenu {
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class MusicCredit : MonoBehaviour {
        
        void Awake() {
            if (HammyFarming.Brian.Director.Instance != null) {
                GetComponent<TMPro.TextMeshProUGUI>().text = HammyFarming.Brian.Director.Instance.LevelMusicCredit;
            }
        }
    }
}