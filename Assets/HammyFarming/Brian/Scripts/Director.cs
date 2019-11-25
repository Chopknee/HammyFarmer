using HammyFarming.Brian.Base;
using HammyFarming.Brian.Utils.Timing;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HammyFarming.Brian {

    public class Director: MonoBehaviour {

        public static Director Instance;

        public static GameObject HammyGameObject;

        [Header("Goal and Growth Settings")]
        [Tooltip("How much does hammy need to collect for the level to be 'complete'?")]
        public float SiloFillGoal = 100;
        [Tooltip("How much is a full-grown plant worth when dropped at the silo?")]
        public float FullGrowthScore = 5;

        [Header("Other")]
        public string hammyBaseSceneName = "HammyBase";

        public string LevelMusicCredit = "MUSIC CREDIT: UNKNOWN HAMSTER";

        public delegate void SiloFillChanged ( float value );
        public SiloFillChanged OnSiloFillChanged;

        float _siloFill;
        public float SiloFillLevel {
            get {
                return _siloFill;
            }
            set {
                _siloFill = value;
                OnSiloFillChanged?.Invoke(value);
            }
        }

        Timeout autoStartTimeout;

        protected virtual void Awake () {
            Instance = this;
            //If the level manager does not run awake level (because the scene was run from the editor) this will make it do so automatically
            autoStartTimeout = new Timeout(2.5f, true);
        }

        public virtual void AwakeLevel() {
            LevelManagement.OnLevelAwake?.Invoke();
            autoStartTimeout.Reset();
        }

        public void SpawnPlayer() {
            StartCoroutine(LoadSceneBase());
        }

        public void Update () {
            if (autoStartTimeout.Tick(Time.deltaTime)) {
                autoStartTimeout.Reset();
                AwakeLevel();
            }
        }

        //The scene which contains all parts pertaining to gameplay.
        private IEnumerator LoadSceneBase () {

            AsyncOperation levelBaseLoader = SceneManager.LoadSceneAsync(hammyBaseSceneName, LoadSceneMode.Additive);

            while (!levelBaseLoader.isDone) {
                yield return null;
            }

            //Putting the hammy object at any object tagged with spawn point.
            HammyGameObject = GameObject.FindGameObjectWithTag("HammyBall");

            GameObject go = GameObject.FindGameObjectWithTag("SpawnPoint");
            if (go != null) {
                HammyGameObject.transform.position = go.transform.position;
            }

            LevelManagement.OnLevelStart?.Invoke();
        }
    }
}
