using HammyFarming.Brian.GameManagement;
using HammyFarming.Brian.Utils.Timing;
using UnityEngine;

namespace HammyFarming.Brian {

    public class Director: MonoBehaviour {

        public static Director Instance;

        public static GameObject Hammy;
        public static GameObject LevelCamera;
        public static GameObject PauseMenu;

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
            Hammy = Instantiate(Resources.Load<GameObject>("Prefabs/Hammy/HammyBall"));
        }

        public void SpawnCamera() {
            LevelCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Camera/LevelCamera"));
        }

        public void SpawnPauseMenu() {
            PauseMenu = Instantiate(Resources.Load<GameObject>("Prefabs/PauseMenu/PauseMenu"));
        }

        public void Update () {
            if (autoStartTimeout.Tick(Time.deltaTime)) {
                autoStartTimeout.Reset();
                AwakeLevel();
            }
        }

        public void OnDestroy () {
            Hammy = null;
            LevelCamera = null;
            PauseMenu = null;
        }
    }
}
