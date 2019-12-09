using HammyFarming.Brian.GameManagement;
using HammyFarming.Brian.Sound;
using HammyFarming.Brian.Utils.Timing;
using UnityEngine;

namespace HammyFarming.Scenes.Credits00 {

    public class Director: HammyFarming.Brian.Director {

        private CanvasGroup fadeInBlackout = null;

        private Ticker fadeTicker = null;

        private Transform levelUI = null;

        protected override void Awake () {
            base.Awake();
            //Spawn in a fade to fade back out from black.
            fadeTicker = gameObject.AddComponent<Ticker>();
            fadeTicker.time = 1.5f;
            fadeTicker.OnTick += FadeTick;
            fadeTicker.OnAlarm += FadeAlarm;

            fadeInBlackout = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/SceneFadeIn")).GetComponent<CanvasGroup>();

            levelUI = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Spring01/LevelUI")).transform;

            OnSiloFillChanged += SiloFillChanged;
        }

        public override void AwakeLevel () {
            base.AwakeLevel();

            //Spawn the required level components to get things running
            SpawnPlayerUI();
            SpawnPlayer();
            SpawnPauseMenu();
            SpawnCamera();

            Hammy.transform.position = transform.Find("Hammy Spawn").transform.position;

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

        void LevelStarted () {
            fadeTicker.Run();
        }


        void FadeTick ( float a ) {
            fadeInBlackout.alpha = 1 - fadeTicker.NormalizeTime;
        }

        void FadeAlarm ( float aO ) {
            //When done, kill the fadeout object
            Destroy(fadeInBlackout.gameObject);

            //Enable player controls
            PlayerInput.SetHammyControlsEnabled(true);

            //Target the audio listener to the player camera
            LevelSound.Instance.CurrentListenTarget = LevelCamera.transform;
        }

        private bool siloFilled = false;

        private GameObject EndSequence = null;

        private void SiloFillChanged ( float fill ) {
            if (fill >= base.SiloFillGoal && !siloFilled) {
                siloFilled = true;
                
                //Level has been completed, do the ending sequence
                EndSequence = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Credits00/Ending"));
            }
        }
    }
}