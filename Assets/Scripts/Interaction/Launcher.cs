using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    public float force = 100;

    List<Collider> colliders = new List<Collider>();

    public void FixedUpdate () {
        foreach (Collider c in colliders) {
            if (c.GetComponent<Rigidbody>() != null) {
                c.GetComponent<Rigidbody>().AddForce(transform.up * force);
            }
        }
    }

    public void OnTriggerEnter ( Collider other ) {
        if (!colliders.Contains(other)) {
            colliders.Add(other);
        }
    }

    public void OnTriggerExit ( Collider other ) {
        if (colliders.Contains(other)) {
            colliders.Remove(other);
        }
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * (force/100f));
        Gizmos.DrawCube(transform.position + transform.up * (force/100f), Vector3.one);
    }

}
