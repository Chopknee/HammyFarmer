using Hammy;
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

    GameObject cameraTracker;
    Transform cameraTrackTransform;


    void AY() {
        Debug.Log("AAY WAS PRESSED");
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        cameraAnchor = Camera.main.transform;

        cameraTracker = new GameObject("CameraTracker");
        cameraTracker.transform.SetParent(transform);
        cameraTrackTransform = cameraTracker.transform;

        Pausemenu.InputMasterController.Hammy.Jump.performed += context => Jump();

    }

    private void FixedUpdate() {
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        rb.AddForce(dir * ((jumped || !onGround)? airControlDivider : 1));
        rb.AddTorque(rot * ((jumped || !onGround) ? airControlDivider : 1 ));

        if (ragdoll != null) {
            if (( ragdoll.transform.position - transform.position ).sqrMagnitude > reteleportRadius * reteleportRadius) {
                //ragdoll.transform.SetParent(transform);
                //ragdoll.transform.position = Vector3.zero;
                //ragdoll.transform.SetParent(null);
                //ragdoll.transform.position = transform.position;
            }
        }
    }

    void Jump() {
        if (onGround && !jumped) {
            rb.AddForce(Vector3.up * jumpForce);
            jumped = true;
        }
    }

    public float forward;
    public float right;

    public bool onGround = false;
    public bool jumped = false;
    float jumpDel = 0;

    Vector3 dir = new Vector3(0, 0, 0);
    Vector3 rot = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Update () {

        onGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundHitDistance, GroundLayers);

        Vector2 axes = Pausemenu.InputMasterController.Hammy.Roll.ReadValue<Vector2>();
        forward = axes.y;
        right = axes.x;

        cameraTrackTransform.rotation = Quaternion.identity;
        cameraTrackTransform.position = new Vector3(cameraAnchor.transform.position.x, transform.position.y, cameraAnchor.transform.position.z);
        cameraTrackTransform.forward = ( transform.position - cameraTracker.transform.position );

        Vector3 forDir = forward * cameraTrackTransform.forward;
        Vector3 rightDir = right * cameraTrackTransform.right;

        dir = forDir + rightDir;
        dir.Normalize();
        dir *= forceMultiplier;

        rot = (( right * -1 * cameraTrackTransform.forward ) + ( forward * cameraTrackTransform.right )).normalized * torqueMultiplier;

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
