using HammyFarming.Brian.Base;
using HammyFarming.Brian.Sound;
using HammyFarming.Brian.Utils.Timing;
using UnityEngine;

namespace HammyFarming.Scenes.Spring01 {

    public class Director: HammyFarming.Brian.Director {

        CanvasGroup fadeInBlackout = null;

        Ticker fadeTicker;

        protected override void Awake () {
            base.Awake();
            //Spawn in a fade to fade back out from black.
            fadeTicker = gameObject.AddComponent<Ticker>();
            fadeTicker.time = 1.5f;
            fadeTicker.OnTick += FadeTick;
            fadeTicker.OnAlarm += FadeAlarm;

            fadeInBlackout = Instantiate(Resources.Load<GameObject>("Base/SceneFadeIn")).GetComponent<CanvasGroup>();
        }

        public override void AwakeLevel () {
            base.AwakeLevel();
            //For this, we just spawn the player.
            SpawnPlayer();
            PlayerInput.SetHammyControlsEnabled(false);
            LevelManagement.OnLevelStart += LevelStarted;

            LevelSound.Instance.FadeAudioSources(true, 1.0f);
        }

        void LevelStarted () {
            fadeTicker.Run();
        }


        void FadeTick ( float a ) {
            fadeInBlackout.alpha = 1 - fadeTicker.NormalizeTime;
        }

        void FadeAlarm ( float aO ) {
            fadeInBlackout.alpha = 0;
            PlayerInput.SetHammyControlsEnabled(true);
            LevelSound.Instance.CurrentListenTarget = Camera.main.gameObject.transform;
        }
    }

}