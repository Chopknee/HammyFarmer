using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Scenes.Summer02 {
    public class StartLetter: MonoBehaviour {

        private HammyFarming.Brian.Utils.Timing.Timeout slideInTimeout = null;
        private HammyFarming.Brian.Utils.Timing.Timeout delayTimeout = null;
        private HammyFarming.Brian.Utils.Timing.Timeout slideOutTimeOut = null;

        private Transform content = null;
        private Transform letterPage = null;
        private TMPro.TextMeshProUGUI txtLetterContents = null;

        private Vector3 letterPageFinalPos;
        private Vector3 letterPageStartPos;

        private CanvasGroup canvasGroup;

        private string letterText;

        public delegate void Finished ();
        public Finished OnFinished;

        private RectTransform levelImage = null;
        private Vector2 levelImageStartSize = Vector2.zero;
        private Vector2 levelImageEndSize = new Vector2(656.0f, 136.0f);
        private Vector3 levelImageStartPos = Vector3.zero;
        private Vector3 levelImageEndPos = Vector3.zero;

        public void Awake () {
            //Init();
            //Play();
        }

        public void Init () {

            RectTransform rect = GetComponent<RectTransform>();

            canvasGroup = GetComponent<CanvasGroup>();

            content = transform.Find("Content");
            letterPage = content.Find("LetterPage");

            levelImage = content.Find("levelImage").GetComponent<RectTransform>();
            levelImageStartSize = levelImage.sizeDelta;
            levelImageStartPos = levelImage.position;
            //levelImageEndPos = 

            txtLetterContents = letterPage.Find("TxtLetterContents").GetComponent<TMPro.TextMeshProUGUI>();
            letterText = txtLetterContents.text;
            txtLetterContents.text = "";

            letterPageFinalPos = letterPage.position;
            letterPageStartPos  = new Vector3(rect.sizeDelta.x/2.0f, -(980.0f/2.0f), 0.0f);
            letterPage.position = letterPageStartPos;

            slideInTimeout = new Brian.Utils.Timing.Timeout(22.5f);
            delayTimeout = new Brian.Utils.Timing.Timeout(5.0f);
            slideOutTimeOut = new Brian.Utils.Timing.Timeout(1.5f);
        }

        public void Play() {
            slideInTimeout.Start();
        }

        public void Update () {
            
            if (slideInTimeout.running) {

                float a = slideInTimeout.NormalizedTime;

                float slideSlice = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(slideInTimeout.GetNormalizedSlice(0.044f, 0.089f));
                letterPage.transform.position = Vector3.Lerp(letterPageStartPos, letterPageFinalPos, slideSlice);
                levelImage.sizeDelta = Vector2.Lerp(levelImageStartSize, levelImageEndSize, slideSlice);

                float textSlice = HammyFarming.Brian.Utils.Easing.EaseInOutSine(slideInTimeout.GetNormalizedSlice(0.11f, 1.0f));
                txtLetterContents.text = HammyFarming.Brian.Utils.Utility.StringFill(letterText, textSlice);

                if (slideInTimeout.Tick(Time.deltaTime)) {
                    //Begin waiting for the player to press skip.
                    txtLetterContents.text = HammyFarming.Brian.Utils.Utility.StringFill(letterText, 1.0f);
                    delayTimeout.Start();
                }
            }

            if (delayTimeout.Tick(Time.deltaTime)) {
                slideOutTimeOut.Start();
            }

            if (slideOutTimeOut.running) {
                float a = slideOutTimeOut.NormalizedTime;

                canvasGroup.alpha = 1.0f - a;
                letterPage.transform.position = Vector3.Lerp(letterPageFinalPos, letterPageStartPos, a);

                if (slideOutTimeOut.Tick(Time.deltaTime)) {
                    OnFinished?.Invoke();
                }
            }
        }

        public void Skip() {

            if (slideInTimeout.running) {
                slideInTimeout.NormalizedTime = 1.0f;
            }

            if (delayTimeout.running) {
                delayTimeout.NormalizedTime = 1.0f;
            }

        }
    }
}