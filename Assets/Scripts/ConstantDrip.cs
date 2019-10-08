using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantDrip: MonoBehaviour {

    public GameObject prefab;
    public float delay;
    public bool active;

    float del = 0;
    void Update () {
        if (active) {
            del += Time.deltaTime;
            if (del > delay) {
                del = 0;
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.3f);
    }
}
