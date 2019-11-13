using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.Interaction {

    /// <summary>
    /// This is a class for generic hammy interactions.
    /// It exposes functions for both general interaction, or attatchment.
    /// </summary>
    public class HammyInteractable: MonoBehaviour {

        protected bool isHammyInside = false;
        protected GameObject hammy;

        //bool controlsHooked = false;

        public virtual void Start () {
            Director.InputMasterController.Hammy.Attach.performed += OnHammyHook;
            Director.InputMasterController.Hammy.Use.performed += OnHammyInteract;
        }


        public void OnHammyHook ( InputAction.CallbackContext context ) {
            if (isHammyInside) {
                HammyHookedIn(hammy);
            }
        }

        public void OnHammyInteract ( InputAction.CallbackContext context ) {
            if (isHammyInside) {
                HammyInteracted(hammy);
            }
        }

        private void OnTriggerEnter ( Collider other ) {
            if (other.tag == "HammyBall") {
                isHammyInside = true;
                hammy = other.gameObject;
                HammyEntered(hammy);
            }
        }

        private void OnTriggerExit ( Collider other ) {
            if (other.tag == "HammyBall") {
                isHammyInside = false;
                HammyExited();
            }
        }

        private void OnDestroy () {
            Director.InputMasterController.Hammy.Attach.performed -= OnHammyHook;
            Director.InputMasterController.Hammy.Use.performed -= OnHammyInteract;
        }

        //Override any of these to get the desired functionality
        public virtual void HammyEntered ( GameObject hammy ) { }
        public virtual void HammyExited () { }
        public virtual void HammyHookedIn ( GameObject hammy ) { }
        public virtual void HammyInteracted ( GameObject hammy ) { }

    }
}