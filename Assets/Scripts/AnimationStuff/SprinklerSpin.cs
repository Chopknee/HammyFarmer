using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerSpin: MonoBehaviour {

    public float spinSpeed = Mathf.PI * 0.25f;//

    void Update () {
        float dst = spinSpeed * Time.deltaTime;
        transform.Rotate(0, dst * Mathf.Rad2Deg, 0, Space.Self);
    }
}
