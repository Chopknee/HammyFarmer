using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

    public Transform cameraAnchor;
    public float forceMultiplier = 10;
    public float torqueMultiplier = 10;
    public float jumpForce = 10;
    Rigidbody rb;

    public GameObject ragdoll;
    public float reteleportRadius = 1;

    List<GameObject> collectedItems = new List<GameObject>();

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void FixedUpdate() {
        rb.AddForce(dir);
        rb.AddTorque(rot);

        if (ragdoll != null) {
            if (( ragdoll.transform.position - transform.position ).sqrMagnitude > reteleportRadius * reteleportRadius) {
                //ragdoll.transform.SetParent(transform);
                //ragdoll.transform.position = Vector3.zero;
                //ragdoll.transform.SetParent(null);
                //ragdoll.transform.position = transform.position;
            }
        }
    }

    Vector3 dir = new Vector3(0, 0, 0);
    Vector3 rot = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Update () {
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        Vector3 forDir = forward * cameraAnchor.forward;
        Vector3 rightDir = right * cameraAnchor.right;

        dir = forDir + rightDir;
        dir.Normalize();
        dir *= forceMultiplier;

        rot = (( right * -1 * cameraAnchor.forward ) + ( forward * cameraAnchor.right )).normalized * torqueMultiplier;

        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector3.up * jumpForce);
        }
	}

    public void reportCollection(GameObject item) {

        collectedItems.Add(item);
        item.transform.SetParent(this.transform);

    }
}
