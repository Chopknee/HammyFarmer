using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace HammyFarming.Brian {

    public class DebugLog: MonoBehaviour {

        private static DebugLog Instance = null;

        private ScrollRect scrollView = null;
        private Font debugFont = null;
        private int fontSize = 24;
        private float lineSpacing = 1.0f;
        private Color fontColor = Color.white;
        private Color normalColor = Color.green;
        private Color warningColor = Color.yellow;
        private Color errorColor = Color.red;
        private int MaxLogMessages = 500;

        CanvasGroup cg;

        List<GameObject> logMessages;

        // Start is called before the first frame update
        void Awake () {
            debugFont = Resources.Load<Font>("Fonts/cour");

            Debug.Log("Debug Log Spawned!");
            scrollView = transform.Find("DebugView").gameObject.GetComponent<ScrollRect>();

            logMessages = new List<GameObject>();

            //Just in case multiple instances get spawned.
            if (Instance == null) {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            } else {
                Destroy(gameObject);
                return;
            }

            Application.logMessageReceived += LogLogged;
            cg = GetComponent<CanvasGroup>();
            cg.interactable = false;
            cg.blocksRaycasts = false;
            cg.alpha = 0;

            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Hammy.Debug.performed += OpenDebug;
        }

        void OpenDebug ( InputAction.CallbackContext context ) {
            if (cg.interactable) {
                cg.interactable = false;
                cg.blocksRaycasts = false;
                cg.alpha = 0;
            } else {
                cg.interactable = true;
                cg.blocksRaycasts = true;
                cg.alpha = 1;
            }
        }

        void LogLogged ( string logString, string stackTrace, LogType type ) {
            string message = logString;
            Color c = normalColor;

            if (type == LogType.Exception || type == LogType.Error || type == LogType.Warning) {
                message += "\n" + stackTrace;
                c = errorColor;
                if (type == LogType.Warning) {
                    c = warningColor;
                }
            }
            GameObject go = CreateDebugMessage(c, type.ToString(), message);
            go.transform.SetParent(scrollView.content);
            logMessages.Add(go);

            if (logMessages.Count > MaxLogMessages) {
                Destroy(logMessages[0]);
                logMessages.RemoveAt(0);//Remove the oldest message from the log
            }

            Invoke("thing", 0.1f);
        }

        void thing () {
            scrollView.verticalScrollbar.value = 0f;
        }

        public GameObject CreateDebugMessage ( Color typeColor, string sender, string message ) {
            GameObject go = new GameObject("Chat Message " + sender);
            go.AddComponent<CanvasRenderer>();
            go.AddComponent<RectTransform>();
            Text tx = go.AddComponent<Text>();
            tx.font = debugFont;
            tx.fontSize = fontSize;
            tx.lineSpacing = lineSpacing;
            tx.color = fontColor;
            tx.text = string.Format("<color=#{0}>{1}</Color>: {2} ", ColorUtility.ToHtmlStringRGB(typeColor), sender, message);
            ContentSizeFitter csf = go.AddComponent<ContentSizeFitter>();
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;//I know this is technically not valid, but eff it.
            return go;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize () {
            //Spawn an instance of the debug log prefab.
            Instantiate(Resources.Load<GameObject>("Prefabs/DebugLog")).AddComponent<HammyFarming.Brian.DebugLog>();
        }
    }
}
