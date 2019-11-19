using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HammyFarming.Brian.Base.PauseMenu {

    public class TabPane: MonoBehaviour, ISelectHandler, IDeselectHandler {

        Button myButton;

        [HideInInspector]
        public Image background;

        public Color normalColor;
        public Color dimColor;
        public Color selectedColor;

        public CanvasGroup cg;

        public delegate void TabClicked(TabPane tp);
        public TabClicked OnTabClicked;

        public Selectable firstTabNavObject;

        bool _ActiveTab = false;
        public bool ActiveTab {
            get {
                return _ActiveTab;
            }
            set {
                _ActiveTab = value;
                if (value) {
                    originalNav = myNavigation.selectOnDown;
                    myNavigation.selectOnDown = firstTabNavObject;
                    myButton.navigation = myNavigation;
                } else {
                    myNavigation.selectOnDown = originalNav;
                    myButton.navigation = myNavigation;
                }
            }
        }

        Selectable originalNav;

        Navigation myNavigation;

        void Start () {
            myButton = GetComponent<Button>();
            myNavigation = myButton.navigation;

            background = GetComponent<Image>();
            myButton.onClick.AddListener(TabClickedd);
        }

        void TabClickedd() {
            OnTabClicked?.Invoke(this);
        }

        public void OnSelect ( BaseEventData eventData ) {
            background.color = selectedColor;
        }

        public void OnDeselect ( BaseEventData eventData ) {
            if (ActiveTab) {
                background.color = normalColor;
            } else {
                background.color = dimColor;
            }
        }
    }
}