using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HammyFarming.PlayerUI {

    public class FrameRateCounter: MonoBehaviour {

        TextMeshProUGUI text;

        const float measurePeriod = 0.5f;
        int accum;
        float next;
        string disp = "{0} FPS";


        // Start is called before the first frame update
        void Start () {
            text = GetComponent<TextMeshProUGUI>();
            next = Time.realtimeSinceStartup + measurePeriod;
        }

        // Update is called once per frame
        void Update () {
            if (text == null)
                return;

            accum++;

            if (Time.realtimeSinceStartup > next) {
                int current = (int) ( accum / measurePeriod );
                text.text = string.Format(disp, current);
                accum = 0;
                next = Time.realtimeSinceStartup + measurePeriod;
            }
        }
    }
}