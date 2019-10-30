using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerSpin: MonoBehaviour {

    public float spinSpeed = Mathf.PI * 0.25f;//
    public Vector3 axis = new Vector3(0, 1, 0);

    void Update () {
        float dst = spinSpeed * Time.deltaTime * Mathf.Rad2Deg;
        transform.Rotate(axis.x * dst, axis.y * dst, axis.z * dst, Space.Self);
    }
}
