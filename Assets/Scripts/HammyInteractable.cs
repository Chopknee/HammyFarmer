using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammyInteractable: MonoBehaviour {

    protected bool isHammyInside = false;
    protected GameObject hammy;

    public virtual void Start() {
        Pausemenu.InputMasterController.Hammy.Attach.performed += context => OnInteracted();
    }

    public void OnInteracted() {
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
            //hammy = null;
            HammyExited();
        }
    }

    public virtual void HammyEntered(GameObject hammy) {}
    public virtual void HammyExited () {}
    public virtual void HammyInteracted (GameObject hammy) {}

}
