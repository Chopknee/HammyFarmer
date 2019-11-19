using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.Base.PauseMenu {

    public class TabPaneGroup: MonoBehaviour {

        public TabPane[] tabPanes;
        public int defaultPaneIndex = 0;
        public int selectedPaneIndex = 0;

        void Start () {
            
            for (int i = 0; i < tabPanes.Length; i++ ) {
                TabPane tp = tabPanes[i];
                tp.OnTabClicked += OnTabPaneClicked;
                if (i == defaultPaneIndex) {
                    ShowPane(i);
                } else {
                    HidePane(i);
                }
            }
            selectedPaneIndex = defaultPaneIndex;
        }

        void ShowNextPane(InputAction.CallbackContext context) {
            //For gamepad
            HidePane(selectedPaneIndex);
            selectedPaneIndex++;
            if (selectedPaneIndex >= tabPanes.Length) {
                selectedPaneIndex = 0;
            }
            ShowPane(selectedPaneIndex);
        }

        void ShowLastPane ( InputAction.CallbackContext context ) {
            //For gamepad
            HidePane(selectedPaneIndex);
            selectedPaneIndex--;
            if (selectedPaneIndex < 0) {
                selectedPaneIndex = tabPanes.Length - 1;
            }
            ShowPane(selectedPaneIndex);
        }

        void OnTabPaneClicked(TabPane tp) {
            HidePane(selectedPaneIndex);
            for (int i = 0; i < tabPanes.Length; i++) {
                if (tp == tabPanes[i]) {
                    ShowPane(i);
                    selectedPaneIndex = i;
                    break;
                }
            }
        }

        void ShowPane(int index) {
            if (tabPanes.Length > index) {
                tabPanes[index].cg.alpha = 1;
                tabPanes[index].cg.blocksRaycasts = true;
                tabPanes[index].cg.interactable = true;
                tabPanes[index].background.color = tabPanes[index].normalColor;
                tabPanes[index].ActiveTab = true;
            }
        }

        void HidePane(int index) {
            if (tabPanes.Length > index) {
                tabPanes[index].cg.alpha = 0;
                tabPanes[index].cg.blocksRaycasts = false;
                tabPanes[index].cg.interactable = false;
                tabPanes[index].background.color = tabPanes[index].dimColor;
                tabPanes[index].ActiveTab = false;
            }
        }
    }
}