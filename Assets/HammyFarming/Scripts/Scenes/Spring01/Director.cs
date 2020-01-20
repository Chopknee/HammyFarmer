using HammyFarming.Brian.GameManagement;
using HammyFarming.Brian.Sound;
using UnityEngine;

namespace HammyFarming.Scenes.Spring01 {

    public class Director: HammyFarming.Brian.Director {

        private Transform levelUI = null;

        private HammyFarming.Scenes.Spring01.StartLetter startLetter = null;
        private CanvasGroup letterBlocker = null;

        private HammyFarming.Brian.Utils.Timing.Timeout backgroundFadeout = null;

        protected override void Awake () {
            base.Awake();

            levelUI = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Spring01/LevelUI")).transform;
            levelUI.gameObject.SetActive(false);

            startLetter = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Spring01/StartLetter")).GetComponent<HammyFarming.Scenes.Spring01.StartLetter>();
            startLetter.OnFinished += StartLevel;
            startLetter.Init();

            letterBlocker = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Spring01/LetterBlocker")).GetComponent<CanvasGroup>();
            letterBlocker.alpha = 1.0f;

            backgroundFadeout = new HammyFarming.Brian.Utils.Timing.Timeout(1.0f);

            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Hammy.Jump.performed += OnSkipPressed;

            OnSiloFillChanged += SiloFillChanged;
        }

        public void OnSkipPressed ( UnityEngine.InputSystem.InputAction.CallbackContext context ) {
            startLetter.Skip();
        }

        public override void AwakeLevel () {
            base.AwakeLevel();
            startLetter.Play();
        }

        private void StartLevel() {

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
            PlayerInput.SetHammyControlsEnabled(false);

            //Hook into the level started function
            LevelManagement.OnLevelStart += LevelStarted;

            //Start the fading for sounds
            LevelSound.Instance.FadeAudioSources(true, 1.0f);

            //Tell the level to actually start
            LevelManagement.OnLevelStart?.Invoke();
        }

        private void LevelStarted() {
            backgroundFadeout.Start();
        }

        public override void Update() {
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
            PlayerInput.SetHammyControlsEnabled(true);

            //Target the audio listener to the player camera
            LevelSound.Instance.CurrentListenTarget = LevelCamera.transform;

            //Set the level ui to active
            levelUI.gameObject.SetActive(true);

        }

        private bool siloFilled = false;
        private GameObject EndSequence = null;

        private void SiloFillChanged(float fill) {
            if (fill >= base.SiloFillGoal && !siloFilled) {
                siloFilled = true;
                //Level has been completed, do the ending sequence
                EndSequence = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Spring01/Ending"));
            }
        }
    }
}