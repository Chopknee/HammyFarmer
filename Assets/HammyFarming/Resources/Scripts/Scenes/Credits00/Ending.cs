using UnityEngine;

namespace HammyFarming.Scenes.Credits00 {

    public class Ending: MonoBehaviour {

        private HammyFarming.Brian.Utils.Timing.Timeout endSequenceTimeout;
        private HammyFarming.Brian.Utils.Timing.Timeout delayTimeout;

        private CanvasGroup canvasGroup;

        private Transform letterPage = null;
        private TMPro.TextMeshProUGUI txtLetterContents = null;

        private Vector3 letterPageFinalPos;
        private Vector3 letterPageStartPos;

        private string letterText;

        private CanvasGroup blackoutCanvasGroup;

        private void Awake () {

            endSequenceTimeout = new Brian.Utils.Timing.Timeout(23.0f);
            delayTimeout = new Brian.Utils.Timing.Timeout(10.0f);

            canvasGroup = GetComponent<CanvasGroup>();

            endSequenceTimeout.Start();

            HammyFarming.Brian.GameManagement.PlayerInput.SetHammyControlsEnabled(false);

            RectTransform rect = GetComponent<RectTransform>();

            canvasGroup = GetComponent<CanvasGroup>();

            letterPage = transform.Find("LetterPage");

            txtLetterContents = letterPage.Find("TxtLetterContents").GetComponent<TMPro.TextMeshProUGUI>();
            letterText = txtLetterContents.text;
            txtLetterContents.text = "";

            letterPageFinalPos = letterPage.position;
            letterPageStartPos = new Vector3(rect.sizeDelta.x / 2.0f, -( 980.0f / 2.0f ), 0.0f);
            letterPage.position = letterPageStartPos;

            blackoutCanvasGroup = transform.Find("Blackout").GetComponent<CanvasGroup>();
        }

        private void Update () {
            
            if (endSequenceTimeout.running) {

                float a = endSequenceTimeout.NormalizedTime;

                float fadeSlice = endSequenceTimeout.GetNormalizedSlice(0.0f, 0.04f);
                canvasGroup.alpha = fadeSlice;

                float slideSlice = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(endSequenceTimeout.GetNormalizedSlice(0.044f, 0.089f));
                letterPage.transform.position = Vector3.Lerp(letterPageStartPos, letterPageFinalPos, slideSlice);

                float textSlice = HammyFarming.Brian.Utils.Easing.EaseInOutSine(endSequenceTimeout.GetNormalizedSlice(0.11f, 0.9f));
                txtLetterContents.text = HammyFarming.Brian.Utils.Utility.StringFill(letterText, textSlice);

                if (endSequenceTimeout.Tick(Time.deltaTime)) {
                    txtLetterContents.text = HammyFarming.Brian.Utils.Utility.StringFill(letterText, 1.0f);
                    delayTimeout.Start();
                }
            }

            if (delayTimeout.running) {
                blackoutCanvasGroup.alpha = delayTimeout.GetNormalizedSlice(0.1f, 0.9f);
                if (delayTimeout.Tick(Time.deltaTime)) {
                    //LOAD THE LEVEL SELECT
                    HammyFarming.Brian.GameManagement.LevelManagement.Instance.LoadLevel(1);
                }
            }

        }

    }
}