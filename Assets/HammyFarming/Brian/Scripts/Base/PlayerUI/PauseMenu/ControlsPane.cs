using UnityEngine;
using UnityEngine.UI;

namespace HammyFarming.Brian.Base.PlayerUI.PauseMenu {

    public class ControlsPane: MonoBehaviour {

        public Slider VerticalSensit;
        public Slider HorizontalSensit;
        public Toggle VerticalInvertedToggle;
        public Toggle HorizontalInvertedToggle;

        void Start () {
            //Preload the values
            VerticalInvertedToggle.isOn = HammyFarming.Brian.Base.GameSettings.VerticalInverted;
            HorizontalInvertedToggle.isOn = HammyFarming.Brian.Base.GameSettings.HorizontalInverted;
            VerticalSensit.value = HammyFarming.Brian.Base.GameSettings.VerticalSensitivity;
            HorizontalSensit.value = HammyFarming.Brian.Base.GameSettings.HorizontalSensitivity;

            VerticalSensit.onValueChanged.AddListener(VerticalChanged);
            HorizontalSensit.onValueChanged.AddListener(HorizontalChanged);
            VerticalInvertedToggle.onValueChanged.AddListener(VerticalToggled);
            HorizontalInvertedToggle.onValueChanged.AddListener(HorizontalToggled);
        }

        private void OnDestroy () {
            VerticalSensit.onValueChanged.RemoveListener(VerticalChanged);
            HorizontalSensit.onValueChanged.RemoveListener(HorizontalChanged);
            VerticalInvertedToggle.onValueChanged.RemoveListener(VerticalToggled);
            HorizontalInvertedToggle.onValueChanged.RemoveListener(HorizontalToggled);
        }

        void VerticalChanged(float value) {
            HammyFarming.Brian.Base.GameSettings.VerticalSensitivity = value;
        }

        void HorizontalChanged(float value) {
            HammyFarming.Brian.Base.GameSettings.HorizontalSensitivity = value;
        }

        void VerticalToggled(bool value) {
            HammyFarming.Brian.Base.GameSettings.VerticalInverted = value;
        }

        void HorizontalToggled(bool value) {
            HammyFarming.Brian.Base.GameSettings.HorizontalInverted = value;
        }
    }
}
