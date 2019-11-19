using HammyFarming.Brian.Base.PauseMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Sound {

    [RequireComponent(typeof(AudioSource))]
    public class SoundType : MonoBehaviour {

        public enum Type { Master, Music, SFX };

        public Type soundType;
        AudioSource[] auses;
        float vol = 0;
        private void Awake() {
            auses = GetComponents<AudioSource>();
        }

        float targetVolume = 0;

        private void Update() {
            switch (soundType) {
                case Type.Master:
                    targetVolume = Mathf.Log10(Pausemenu.MasterVolume);
                    break;
                case Type.Music:
                    targetVolume = Mathf.Log10(Pausemenu.MusicVolume * Pausemenu.MasterVolume);
                    break;
                case Type.SFX:
                    targetVolume = Mathf.Log10(Pausemenu.SoundFXVolume * Pausemenu.MasterVolume);
                    break;
            }

            if (targetVolume != vol) {
                vol = targetVolume;
                foreach (AudioSource aus in auses) {
                    aus.volume = vol;
                }
            }
        }
    }
}