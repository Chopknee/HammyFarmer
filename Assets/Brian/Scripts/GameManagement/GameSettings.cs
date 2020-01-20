using UnityEngine;

namespace HammyFarming.Brian.GameManagement {

    public class GameSettings {

        public delegate void SettingsChanged ();
        public static SettingsChanged OnSettingsChanged;

        private static bool _VerticalInverted;
        public static bool VerticalInverted {
            get {
                return _VerticalInverted;
            }
            set {
                _VerticalInverted = value;
                PlayerPrefs.SetInt("VerticalToggle", ( value ) ? 1 : 0);
                OnSettingsChanged?.Invoke();
            }
        }

        private static bool _HorizontalInverted;
        public static bool HorizontalInverted {
            get {
                return _HorizontalInverted;
            }
            set {
                _HorizontalInverted = value;
                PlayerPrefs.SetInt("HorizontalToggle", ( value ) ? 1 : 0);
                OnSettingsChanged?.Invoke();
            }
        }

        private static float _VerticalSensitivity;
        public static float VerticalSensitivity {
            get {
                return _VerticalSensitivity;
            }
            set {
                _VerticalSensitivity = value;
                PlayerPrefs.SetFloat("VerticalSensitivity", value);
                OnSettingsChanged?.Invoke();
                
            }
        }

        private static float _HorizontalSensitivity;
        public static float HorizontalSensitivity {
            get {
                return _HorizontalSensitivity;
            }
            set {
                _HorizontalSensitivity = value;
                PlayerPrefs.SetFloat("HorizontalSensitivity", value);
                OnSettingsChanged?.Invoke();

            }
        }

        private static float _MasterVolume;
        public static float MasterVolume {
            get {
                return _MasterVolume;
            }
            set {
                _MasterVolume = value;
                PlayerPrefs.SetFloat("MasterVolume", value);
                OnSettingsChanged?.Invoke();

            }
        }

        private static float _MusicVolume;
        public static float MusicVolume {
            get {
                return _MusicVolume;
            }
            set {
                _MusicVolume = value;
                PlayerPrefs.SetFloat("MusicVolume", value);
                OnSettingsChanged?.Invoke();

            }
        }

        private static float _SFXVolume;
        public static float SFXVolume {
            get {
                return _SFXVolume;
            }
            set {
                _SFXVolume = value;
                PlayerPrefs.SetFloat("SFXVolume", value);
                OnSettingsChanged?.Invoke();

            }
        }

        public static bool FirstPlay { get; private set; }//Not settable.

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize () {
            Debug.Log("Loading player preferences...");
            //Pre loading all settings. This is where the defaults are located!
            _VerticalInverted = PlayerPrefs.GetInt("VerticalToggle", 1) == 1;
            _HorizontalInverted = PlayerPrefs.GetInt("HorizontalToggle", 0) == 1;
            _VerticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity", 1);
            _HorizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity", 1);
            _MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
            _MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
            _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);

            //First time playing bool!
            FirstPlay = PlayerPrefs.GetInt("FirstPlay", 0) == 1;
            PlayerPrefs.SetInt("FirstPlay", 1);
        }

        public static void SetPref(string pref, object value) {
            switch (pref) {
                case "HorizontalSensitivity":
                    HorizontalSensitivity = (float) value;
                    break;
                case "VerticalSensitivity":
                    VerticalSensitivity = (float) value;
                    break;
                case "MasterVolume":
                    MasterVolume = (float) value;
                    break;
                case "MusicVolume":
                    MusicVolume = (float) value;
                    break;
                case "SFXVolume":
                    SFXVolume = (float) value;
                    break;
                default:
                    Debug.Log("Unkown key was set: " + pref);
                    break;
            }
        }
    }
}

