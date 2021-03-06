﻿using UnityEngine;
using UnityEngine.UI;

namespace HammyFarming.PauseMenu {
    [RequireComponent(typeof(Slider))]
    public class SliderSetting: MonoBehaviour {

        public string PlayerPrefName = "UnknownSliderValue";
        public float minValue = 0;
        public float maxValue = 1;
        public float defaultValue = 1;

        Slider mySlider;

        void Start () {
            mySlider = GetComponent<Slider>();
            mySlider.onValueChanged.AddListener(ValueChanged);
            mySlider.value = PlayerPrefs.GetFloat(PlayerPrefName, defaultValue);
        }


        private void OnEnable () {
            mySlider.value = PlayerPrefs.GetFloat(PlayerPrefName, defaultValue);
        }

        void ValueChanged(float value) {
            PlayerPrefs.SetFloat(PlayerPrefName, value);
        }

        private void OnValidate () {
            HammyFarming.Brian.Utils.Utility.ClampMinmax(ref minValue, ref maxValue, 0, 2);
            if (mySlider == null) {
                mySlider = GetComponent<Slider>();
            }
            mySlider.minValue = minValue;
            mySlider.maxValue = maxValue;
        }
    }
}
