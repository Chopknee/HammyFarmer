using Hammy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;

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
    public static Transform CameraTrackTransform;

    void Start () {
        rb = GetComponent<Rigidbody>();
        cameraAnchor = Camera.main.transform;

        cameraTracker = new GameObject("CameraTracker");
        cameraTracker.transform.SetParent(transform);
        CameraTrackTransform = cameraTracker.transform;

        Pausemenu.InputMasterController.Hammy.Jump.performed += context => Jump();

    }

    private void FixedUpdate() {
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        rb.AddForce(dir * ((jumped || !onGround)? airControlDivider : 1));
        rb.AddTorque(rot * ((jumped || !onGround) ? airControlDivider : 1 ));
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

    void Update () {

        onGround = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundHitDistance, GroundLayers);

        Vector2 axes = Pausemenu.InputMasterController.Hammy.Roll.ReadValue<Vector2>();
        forward = axes.y;
        right = axes.x;

        CameraTrackTransform.rotation = Quaternion.identity;
        CameraTrackTransform.position = new Vector3(cameraAnchor.transform.position.x, transform.position.y, cameraAnchor.transform.position.z);
        CameraTrackTransform.forward = ( transform.position - cameraTracker.transform.position );

        Vector3 forDir = forward * CameraTrackTransform.forward;
        Vector3 rightDir = right * CameraTrackTransform.right;

        dir = forDir + rightDir;
        dir.Normalize();
        dir *= forceMultiplier;

        rot = (( right * -1 * CameraTrackTransform.forward ) + ( forward * CameraTrackTransform.right )).normalized * torqueMultiplier;

        if (jumped) {
            jumpDel += Time.deltaTime;
            if (jumpDel > jumpSleepTime) {
                jumpDel = 0;
                jumped = false;
            }
        }
        //Pausemenu.InputMasterController.devices.Value[0].
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundHitDistance);
    }
}
