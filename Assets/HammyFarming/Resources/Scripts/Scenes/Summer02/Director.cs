using HammyFarming.Brian.GameManagement;
using HammyFarming.Brian.Sound;
using HammyFarming.Brian.Utils.Timing;
using UnityEngine;

namespace HammyFarming.Scenes.Summer02 {

    public class Director: HammyFarming.Brian.Director {

        CanvasGroup fadeInBlackout = null;

        Ticker fadeTicker;

        Transform levelUI;

        protected override void Awake () {
            base.Awake();
            //Spawn in a fade to fade back out from black.
            fadeTicker = gameObject.AddComponent<Ticker>();
            fadeTicker.time = 1.5f;
            fadeTicker.OnTick += FadeTick;
            fadeTicker.OnAlarm += FadeAlarm;

            fadeInBlackout = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/SceneFadeIn")).GetComponent<CanvasGroup>();

            levelUI = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Spring01/LevelUI")).transform;
        }

        public override void AwakeLevel () {
            base.AwakeLevel();
            
            //Spawn the required level components to get things running
			SpawnPlayerUI();
            SpawnPlayer();
            SpawnPauseMenu();
            SpawnCamera();

            Hammy.transform.position = transform.Find("HammySpawnPoint").transform.position;
            Hammy.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 50.0f, 0.0f);

            //Assign the camera target to hammy
            LevelCamera.GetComponent<HammyFarming.Camera.CameraMotion>().target = Hammy.transform;

            //Disable player controls temporarily
            PlayerInput.SetHammyControlsEnabled(false);

            //Hook into the level started function
            LevelManagement.OnLevelStart += LevelStarted;

            //Start the fading for sounds
            LevelSound.Instance.FadeAudioSources(true, 1.0f);

            //Tell the level to actually start
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

            //Set the level ui to active
            levelUI.gameObject.SetActive(true);
        }
    }

}