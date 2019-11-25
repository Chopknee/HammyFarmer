using HammyFarming.Brian;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Base.PlayerUI {

    public class LevelUI: MonoBehaviour {

        void Start () {
            if (Director.Instance.SiloFillGoal == 0) {
                Destroy(gameObject);
                return;
            }
        }
    }
}