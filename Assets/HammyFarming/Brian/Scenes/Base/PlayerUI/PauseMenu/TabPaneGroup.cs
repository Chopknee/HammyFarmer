using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.Base.PlayerUI.PauseMenu {

    public class TabPaneGroup: MonoBehaviour {

        public TabPane[] tabPanes;
        public int defaultPaneIndex = 0;
        public int selectedPaneIndex = 0;

        void Start () {
            Initialize();
            Director.InputMasterController.UI.NextTab.performed += ShowNextPane;
            Director.InputMasterController.UI.PreviousTab.performed += ShowPreviousPane;
        }

        private void OnEnable() {
            Initialize();
        }

        private void OnDestroy() {
            Director.InputMasterController.UI.NextTab.performed -= ShowNextPane;
            Director.InputMasterController.UI.PreviousTab.performed -= ShowPreviousPane;
        }

        //Set up all of the tabs and select the default one.
        void Initialize() {
            for (int i = 0; i < tabPanes.Length; i++) {
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
            selectedPaneIndex = (selectedPaneIndex >= tabPanes.Length) ? 0 : selectedPaneIndex;
            ShowPane(selectedPaneIndex);
        }

        void ShowPreviousPane ( InputAction.CallbackContext context ) {
            //For gamepad
            HidePane(selectedPaneIndex);
            selectedPaneIndex--;
            selectedPaneIndex = (selectedPaneIndex < 0) ? selectedPaneIndex = tabPanes.Length - 1 : selectedPaneIndex;
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