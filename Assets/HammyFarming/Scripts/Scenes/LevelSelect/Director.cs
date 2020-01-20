using HammyFarming.Brian.GameManagement;
using HammyFarming.Brian.Sound;
using HammyFarming.Brian.Utils.Timing;
using UnityEngine;

namespace HammyFarming.Scenes.LevelSelect {

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

            fadeInBlackout = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/SceneFadeIn")).GetComponent<CanvasGroup>();
        }

        public override void AwakeLevel () {
            base.AwakeLevel();

            //Spawn the required level components to get things running
            SpawnPlayer();
            SpawnPauseMenu();
            SpawnCamera();

            Hammy.transform.position = transform.Find("HammySpawnPoint").transform.position;
            Hammy.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 50.0f, 0.0f);
            HammyResetPosition = Hammy.transform.position;

            //Assign the camera target to hammy
            LevelCamera.GetComponent<HammyFarming.Camera.CameraMotion>().target = Hammy.transform;

            //Disable player controls temporarily
            PlayerInput.SetHammyControlsEnabled(false);

            //Hook into the level started function
            LevelManagement.OnLevelStart += LevelStarted;

            //Start the fading for sounds
            LevelSound.Instance.FadeAudioSources(true, 1.0f);

            LevelManagement.OnLevelStart?.Invoke();
        }

        void LevelStarted() {
            fadeTicker.Run();
            //Pre-set initial velocity so it looks like hammy has some speed coming out of the tubes
            Hammy.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 50.0f, 0.0f);
        }


        void FadeTick(float a) {
            fadeInBlackout.alpha = 1 - fadeTicker.NormalizeTime;
        }

        void FadeAlarm(float aO) {
            //When done, kill the fadeout object
            Destroy(fadeInBlackout.gameObject);

            //Enable player controls
            PlayerInput.SetHammyControlsEnabled(true);

            //Target the audio listener to the player camera
            LevelSound.Instance.CurrentListenTarget = LevelCamera.transform;
        }


    }

}