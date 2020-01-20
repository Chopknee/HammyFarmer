using UnityEngine;
using UnityEngine.UI;

namespace HammyFarming.PauseMenu {

    public class AudioPane: MonoBehaviour {

        public Slider MasterSlider;
        public Slider MusicSlider;
        public Slider SFXSlider;

        void Start () {
            MasterSlider.value = HammyFarming.Brian.GameManagement.GameSettings.MasterVolume;
            MusicSlider.value = HammyFarming.Brian.GameManagement.GameSettings.MusicVolume;
            SFXSlider.value = HammyFarming.Brian.GameManagement.GameSettings.SFXVolume;

            MasterSlider.onValueChanged.AddListener(MasterChanged);
            MusicSlider.onValueChanged.AddListener(MusicChanged);
            SFXSlider.onValueChanged.AddListener(SFXChanged);
        }

        private void OnDestroy () {
            MasterSlider.onValueChanged.RemoveListener(MasterChanged);
            MusicSlider.onValueChanged.RemoveListener(MusicChanged);
            SFXSlider.onValueChanged.RemoveListener(SFXChanged);
        }

        void MasterChanged(float value) {
            HammyFarming.Brian.GameManagement.GameSettings.MasterVolume = value;
        }

        void MusicChanged(float value) {
            HammyFarming.Brian.GameManagement.GameSettings.MusicVolume = value;
        }

        void SFXChanged(float value) {
            HammyFarming.Brian.GameManagement.GameSettings.SFXVolume = value;
        }
    }
}