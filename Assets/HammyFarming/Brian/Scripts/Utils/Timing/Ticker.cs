using UnityEngine;

namespace HammyFarming.Brian.Utils.Timing {

    /// <summary>
    /// A simple class for performing timed actions, as opposed to using coroutines.
    /// Once set up, just run timeout on an update loop while passing the delta time.
    /// When finished, runover will hold the difference between the end time and actual time.
    /// </summary>

    public class Ticker : MonoBehaviour {
        
        /// <summary>
        /// Delagate for when the timer ends. Parameter is for the runover time.
        /// </summary>
        /// <param name="runover"></param>
        public delegate void AlarmDelegate ( float runover );
        /// <summary>
        /// A handy Alarm delegate for other objects to subscribe to the end of the alarm.
        /// </summary>
        public AlarmDelegate OnAlarm;

        public delegate void TickDelegate ( float percentComplete );

        public TickDelegate OnTick;

        public float time = 0;
        public float CurrentTime { get; private set; }

        private bool _running = false;
        public bool Running {
            get {
                return _running;
            }
            set {
                _running = value;
                enabled = value;
            }
        }


        public float Runover {
            get {
                return CurrentTime - time;
            }
        }

        public float NormalizeTime {
            get {
                return CurrentTime / time;
            }
        }

        /// <summary>
        /// Begins the timeout from whatever position it was previously.
        /// </summary>
        public void Run () {
            Running = true;
        }

        /// <summary>
        /// Stops the timeout from running.
        /// </summary>
        public void Pause () {
            Running = false;
        }

        /// <summary>
        /// Stops the timeout from running and resets the time.
        /// </summary>
        public void Res () {
            Running = false;
            CurrentTime = 0;
        }

        /// <summary>
        /// Resets the timeout and starts it running.
        /// </summary>
        public void ReStart () {
            CurrentTime = 0;
            Running = true;
        }

        /// <summary>
        /// Required to make the timeout work. Passing in the delta time is needed as well. Can pass scaled delta time too!
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        private bool Tick ( float deltaTime ) {
            if (Running) {
                CurrentTime += deltaTime;
                if (CurrentTime >= time) {
                    Running = false;
                    return true;
                }
                return false;
            }
            return false;
        }

        private void Update () {
            if (Tick(Time.deltaTime)) {
                OnAlarm?.Invoke(Runover);
            } else {
                OnTick?.Invoke(NormalizeTime);
            }
        }
    }
}