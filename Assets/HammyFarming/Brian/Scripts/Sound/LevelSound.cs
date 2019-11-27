using HammyFarming.Brian.GameManagement;
using HammyFarming.Brian.Utils.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Sound {
    [RequireComponent(typeof(AudioListener))]
    public class LevelSound: MonoBehaviour {

        public static LevelSound Instance;

        private Transform _CurrentListenTarget;
        public Transform CurrentListenTarget {
            get {
                return _CurrentListenTarget;
            }
            set {
                _CurrentListenTarget = value;
                transform.SetParent(value);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }


        [HideInInspector]
        public List<SoundType> audioSources;

        private string backgroundMusicTag = "BGM";

        Timeout fadeTimeout;
        bool fadeIn = false;

        public void Awake () {
            Instance = this;
            audioSources = new List<SoundType>();
            fadeTimeout = new Timeout(1, false);
        }

        public void FadeAudioSources(bool fadeIn, float time) {
            this.fadeIn = fadeIn;
            fadeTimeout.timeoutTime = time;
            fadeTimeout.Reset();
            fadeTimeout.Start();
            SetSourcesVolume(( fadeIn ) ? 0 : 1);
        }

        private void Update () {
            if (fadeTimeout.running) {

                float a = fadeTimeout.NormalizedTime;

                if (!fadeIn) {
                    a = 1 - a;
                }

                if (fadeTimeout.Tick(Time.deltaTime)) {
                    a = ( fadeIn ) ? 1 : 0;
                    fadeTimeout.Reset();
                }

                SetSourcesVolume(a);
            }
        }

        public void SetSourcesVolume(float volume) {
            foreach (SoundType st in audioSources) {
                st.FadeAmount = volume;
            }
        }
    }
}