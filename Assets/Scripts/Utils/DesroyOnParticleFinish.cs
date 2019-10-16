using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DesroyOnParticleFinish: MonoBehaviour {
    float duration;
    // Start is called before the first frame update
    void Start () {
        duration = GetComponent<ParticleSystem>().main.duration;
        Invoke("dest", duration);
    }

    void dest() {
        Destroy(gameObject);
    }
}
