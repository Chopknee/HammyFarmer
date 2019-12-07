using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.PlayerUI {
    public class Overlay: MonoBehaviour {

        private CanvasGroup attachedCanvasGroup = null;
        private CanvasGroup detachedCanvasGroup = null;

        private GameObject imgAttach = null;
        private GameObject imgLink = null;

        private Transform toolStateContent = null;

        private HammyFarming.Hammy.ToolConnections hammyToolConnections = null;

        private HammyFarming.Brian.Utils.Timing.Timeout toolAttachFadein;
        private HammyFarming.Brian.Utils.Timing.Timeout toolDetachFadeout;

        //Need to detect when the player has hooked into and disconnected from tools.
        void Awake() {

            toolStateContent = transform.Find("ToolState");

            attachedCanvasGroup = toolStateContent.Find("Attached").GetComponent<CanvasGroup>();

            imgAttach = attachedCanvasGroup.transform.Find("ImgAttach").gameObject;
            imgLink = attachedCanvasGroup.transform.Find("ImgLink").gameObject;
            imgLink.SetActive(false);
            attachedCanvasGroup.alpha = 0;

            detachedCanvasGroup = toolStateContent.Find("Detached").GetComponent<CanvasGroup>();
            detachedCanvasGroup.alpha = 0;

            HammyFarming.Brian.GameManagement.LevelManagement.OnLevelStart += LevelStarted;

            toolAttachFadein = new Brian.Utils.Timing.Timeout(2);
            toolDetachFadeout = new Brian.Utils.Timing.Timeout(4);
        }

        private void Update () {
            
            if (toolAttachFadein.running) {
                float a = toolAttachFadein.GetNormalizedSlice(0.0f, 0.25f);
                attachedCanvasGroup.alpha = a;
                imgAttach.SetActive(toolAttachFadein.NormalizedTime < 0.75f);
                imgLink.SetActive(toolAttachFadein.NormalizedTime >= 0.75f);
                if (toolAttachFadein.Tick(Time.deltaTime)) {
                    //Finished!
                    toolAttachFadein.Reset();
                }
            }

            if (toolDetachFadeout.running) {
                float a = toolDetachFadeout.NormalizedTime;
                detachedCanvasGroup.alpha = 1 - a;
                if (toolDetachFadeout.Tick(Time.deltaTime)) {
                    //Finished
                    toolDetachFadeout.Reset();
                }
            }

        }

        void LevelStarted() {
            hammyToolConnections = HammyFarming.Brian.Director.Hammy.GetComponent<HammyFarming.Hammy.ToolConnections>();
            hammyToolConnections.OnHammyConnected += OnToolConnected;
            hammyToolConnections.OnHammyDisconnected += OnToolDisconnected;
        }

        void OnToolDisconnected( HammyFarming.Brian.ToolAttachment tool ) {
            toolAttachFadein.Reset();
            detachedCanvasGroup.alpha = 1;
            attachedCanvasGroup.alpha = 0;
            toolDetachFadeout.Reset();
            toolDetachFadeout.Start();
        }

        void OnToolConnected(HammyFarming.Brian.ToolAttachment tool) {
            toolDetachFadeout.Reset();
            detachedCanvasGroup.alpha = 0;
            attachedCanvasGroup.alpha = 0;
            toolAttachFadein.Reset();
            toolAttachFadein.Start();
            imgLink.SetActive(false);
            imgAttach.SetActive(true);
        }

    }
}
