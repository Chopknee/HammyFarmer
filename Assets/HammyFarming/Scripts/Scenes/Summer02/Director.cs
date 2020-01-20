using UnityEngine;

namespace HammyFarming.Scenes.Summer02 {
    public class Director: HammyFarming.Brian.Director {


        Transform levelUI;

        private HammyFarming.Scenes.Summer02.StartLetter startLetter = null;
        private CanvasGroup letterBlocker = null;

        private HammyFarming.Brian.Utils.Timing.Timeout backgroundFadeout = null;

        protected override void Awake () {
            base.Awake();

            levelUI = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Summer02/LevelUI")).transform;
            levelUI.gameObject.SetActive(false);

            startLetter = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Summer02/StartLetter")).GetComponent<HammyFarming.Scenes.Summer02.StartLetter>();
            startLetter.OnFinished += StartLevel;
            startLetter.Init();

            letterBlocker = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Summer02/LetterBlocker")).GetComponent<CanvasGroup>();
            letterBlocker.alpha = 1;

            backgroundFadeout = new HammyFarming.Brian.Utils.Timing.Timeout(1);

            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Hammy.Jump.performed += OnSkipPressed;
        }

        public void OnSkipPressed ( UnityEngine.InputSystem.InputAction.CallbackContext context ) {
            startLetter.Skip();
        }

        public override void AwakeLevel () {
            base.AwakeLevel();
            startLetter.Play();
        }

        private void StartLevel () {

            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Hammy.Jump.performed -= OnSkipPressed;
            //Spawn the required level components to get things running
            SpawnPlayerUI();
            SpawnPlayer();
            SpawnPauseMenu();
            SpawnCamera();

            Hammy.transform.position = transform.Find("HammySpawnPoint").transform.position;
            Hammy.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 50.0f, 0.0f);
            HammyResetPosition = Hammy.transform.position;

            //Assign the camera target to hammy
            LevelCamera.GetComponent<HammyFarming.Camera.CameraMotion>().target = Hammy.transform;

            //Disable player controls temporarily
            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(false);

            //Hook into the level started function
            HammyFarming.Brian.GameManagement.LevelManagement.OnLevelStart += LevelStarted;

            //Start the fading for sounds
            HammyFarming.Brian.Sound.LevelSound.Instance.FadeAudioSources(true, 1.0f);

            //Tell the level to actually start
            HammyFarming.Brian.GameManagement.LevelManagement.OnLevelStart?.Invoke();
        }

        private void LevelStarted () {
            backgroundFadeout.Start();
        }

        public override void Update () {
            base.Update();

            if (backgroundFadeout.running) {

                letterBlocker.alpha = 1 - backgroundFadeout.NormalizedTime;

                if (backgroundFadeout.Tick(Time.deltaTime)) {
                    letterBlocker.alpha = 0;
                    StartPlaying();
                }
            }
        }

        private void StartPlaying () {
            //When done, kill the fadeout object
            Destroy(letterBlocker.gameObject);
            Destroy(startLetter.gameObject);

            //Enable player controls
            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(true);

            //Target the audio listener to the player camera
            HammyFarming.Brian.Sound.LevelSound.Instance.CurrentListenTarget = LevelCamera.transform;

            //Set the level ui to active
            levelUI.gameObject.SetActive(true);

        }

    }

}