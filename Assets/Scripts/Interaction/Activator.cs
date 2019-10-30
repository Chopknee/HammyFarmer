using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Activator : HammyInteractable {

    public GameObject messageGUI;
    public MonoBehaviour[] activationScripts;
    bool running;
    AudioSource aus;

    public AudioClip activateSound;
    public AudioClip deActivateSound;

    // Start is called before the first frame update
    public override void Start(){
        messageGUI.SetActive(false);
        base.Start();
        aus = GetComponent<AudioSource>();
    }

    public override void HammyInteracted ( GameObject hammy ) {
        //Do the thing??
        running = !running;
        foreach (MonoBehaviour script in activationScripts) {
            script.enabled = running;
        }

        if (running) {
            PlaySound(activateSound);
        } else {
            PlaySound(deActivateSound);
        }
    }

    public override void HammyEntered ( GameObject hammy ) {
        if (messageGUI != null) {
            messageGUI.SetActive(true);
        }
    }

    public override void HammyExited () {
        if (messageGUI != null) {
            messageGUI.SetActive(false);
        }
    }

    void PlaySound ( AudioClip clip ) {
        aus.Stop();
        aus.clip = clip;
        aus.Play();
    }
}
