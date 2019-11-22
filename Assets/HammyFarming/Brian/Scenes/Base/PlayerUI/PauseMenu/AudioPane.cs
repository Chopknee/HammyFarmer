using UnityEngine;
using UnityEngine.UI;

namespace HammyFarming.Brian.Base.PlayerUI.PauseMenu {

    public class AudioPane: MonoBehaviour {

        public Slider MasterSlider;
        public Slider MusicSlider;
        public Slider SFXSlider;

        void Start () {
            MasterSlider.value = HammyFarming.Brian.Base.GameSettings.MasterVolume;
            MusicSlider.value = HammyFarming.Brian.Base.GameSettings.MusicVolume;
            SFXSlider.value = HammyFarming.Brian.Base.GameSettings.SFXVolume;

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
            HammyFarming.Brian.Base.GameSettings.MasterVolume = value;
        }

        void MusicChanged(float value) {
            HammyFarming.Brian.Base.GameSettings.MusicVolume = value;
        }

        void SFXChanged(float value) {
            HammyFarming.Brian.Base.GameSettings.SFXVolume = value;
        }
    }
}