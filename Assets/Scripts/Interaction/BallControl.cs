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

    public LayerMask GroundLayers;
    public float groundHitDistance;
    public float jumpSleepTime = 1;
    [Range(0.001f, 1)]
    public float airControlDivider;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void FixedUpdate() {
        rb.AddForce(dir * ((jumped)? airControlDivider : 1));
        rb.AddTorque(rot * ( ( jumped ) ? airControlDivider : 1 ));

        if (ragdoll != null) {
            if (( ragdoll.transform.position - transform.position ).sqrMagnitude > reteleportRadius * reteleportRadius) {
                //ragdoll.transform.SetParent(transform);
                //ragdoll.transform.position = Vector3.zero;
                //ragdoll.transform.SetParent(null);
                //ragdoll.transform.position = transform.position;
            }
        }
    }

    public bool onGround = false;
    bool jumped = false;
    float jumpDel = 0;

    Vector3 dir = new Vector3(0, 0, 0);
    Vector3 rot = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Update () {

        onGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundHitDistance, GroundLayers);

        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        Vector3 forDir = forward * cameraAnchor.forward;
        Vector3 rightDir = right * cameraAnchor.right;

        dir = forDir + rightDir;
        dir.Normalize();
        dir *= forceMultiplier;

        rot = (( right * -1 * cameraAnchor.forward ) + ( forward * cameraAnchor.right )).normalized * torqueMultiplier;

        if (Input.GetButtonDown("Jump") && onGround && !jumped) {
            rb.AddForce(Vector3.up * jumpForce);
            jumped = true;
        }

        if (jumped) {
            jumpDel += Time.deltaTime;
            if (jumpDel > jumpSleepTime) {
                jumpDel = 0;
                jumped = false;
            }
        }

	}

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundHitDistance);
    }
}
