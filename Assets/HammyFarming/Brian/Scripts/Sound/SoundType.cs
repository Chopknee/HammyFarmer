using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Sound {

    [RequireComponent(typeof(AudioSource))]
    public class SoundType : MonoBehaviour {

        public enum Type { Master, Music, SFX };

        public Type soundType;

        Dictionary<AudioSource, float> ausesAndVolumes;

        float _fd = 1;
        public float FadeAmount {
            get {
                return _fd;
            }
            set {
                _fd = value;
                UpdateVolume();
            }
        }

        private void Awake() {
            HammyFarming.Brian.GameManagement.GameSettings.OnSettingsChanged += UpdateVolume;
            ausesAndVolumes = new Dictionary<AudioSource, float>();
            foreach (AudioSource aus in GetComponents<AudioSource>()) {
                ausesAndVolumes.Add(aus, aus.volume);
            }

            UpdateVolume();

            HammyFarming.Brian.GameManagement.LevelManagement.OnLevelAwake += LevelAwake;
            
            if (!HammyFarming.Brian.GameManagement.LevelManagement.IsLevelLoaded) {
                FadeAmount = 0;
            }

        }

        void LevelAwake() {
            LevelSound.Instance.audioSources.Add(this);
        }

        private void OnDestroy () {
            HammyFarming.Brian.GameManagement.GameSettings.OnSettingsChanged -= UpdateVolume;
        }

        void UpdateVolume() {
            float targetVolume = 0;
            switch (soundType) {
                case Type.Master:
                    targetVolume = HammyFarming.Brian.GameManagement.GameSettings.MasterVolume;
                    break;
                case Type.Music:
                    targetVolume = HammyFarming.Brian.GameManagement.GameSettings.MusicVolume * HammyFarming.Brian.GameManagement.GameSettings.MasterVolume;
                    break;
                case Type.SFX:
                    targetVolume = HammyFarming.Brian.GameManagement.GameSettings.SFXVolume * HammyFarming.Brian.GameManagement.GameSettings.MasterVolume;
                    break;
            }

            //
            if (ausesAndVolumes != null) {
                foreach (KeyValuePair<AudioSource, float> kvPair in ausesAndVolumes) {
                    kvPair.Key.volume = targetVolume * _fd * kvPair.Value;
                }
            }
        }
    }
}