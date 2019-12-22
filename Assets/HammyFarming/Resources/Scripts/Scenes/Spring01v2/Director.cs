using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Scenes.Spring01v2
{
    public class Director : HammyFarming.Brian.Director {

        private CanvasGroup fadeCanvasGroup = null;
        private HammyFarming.Brian.Utils.Timing.Timeout fadeTimeout = null;

        protected override void Awake()
        {
            base.Awake();

            //Fade the Scene In
            fadeCanvasGroup = Instantiate(Resources.Load<GameObject>("Prefabs/Scenes/Spring01v2/Fade")).GetComponent<CanvasGroup>();
            fadeCanvasGroup.alpha = 1.0f;
            fadeTimeout = new Brian.Utils.Timing.Timeout(1.0f);

            Debug.Log("[Your irritation relieving message here]");

        }


        public override void AwakeLevel()
        {
            base.AwakeLevel();

            //Spawn Level Requirements
            SpawnPlayerUI();
            SpawnPauseMenu();
            SpawnPlayer();
            SpawnCamera();

            //Make Hammy Spawn at His Spawn Point
            Hammy.transform.position = transform.Find("HammySpawnPoint").transform.position;

            //Make Camera Follow Hammy
            LevelCamera.GetComponent<HammyFarming.Camera.CameraMotion>().target = Hammy.transform;

            //Make Sounds and Music Fade In
            HammyFarming.Brian.Sound.LevelSound.Instance.FadeAudioSources(true, 1.0f);

            //Make Sounds Play at the Camera
            HammyFarming.Brian.Sound.LevelSound.Instance.CurrentListenTarget = LevelCamera.transform;

            HammyFarming.Brian.GameManagement.LevelManagement.OnLevelStart += StartLevel;

            //Disable Controls so Player Cannnot Control Hammy During Fade/Loading/Etc
            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(false);

            //Starts the Fade In
            fadeTimeout.Start();
        }

        public void StartLevel()
        {
            //Enables Controls for Player After Everything is Ready
            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(true);
        }

        public override void Update()
        {
            base.Update();

            if (fadeTimeout.running)
            {
                //This will take what ever value you have for the time and generate a number between 0 and 1 depending on how far along the time is.
                float a = fadeTimeout.NormalizedTime;
                fadeCanvasGroup.alpha = 1.0f - a;

                if (fadeTimeout.Tick(Time.deltaTime))
                {
                    fadeCanvasGroup.alpha = 0.0f;
                    //Makes Everything Start
                    HammyFarming.Brian.GameManagement.LevelManagement.OnLevelStart?.Invoke();
                }
            }
        }

    }
}


