using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour {

    [Tooltip("How much force will push the ball relative to the player's desired direction of travel.")]
    public float forceMultiplier = 10;
    [Tooltip("How much torque will be applied to the ball relative to the player's desired direction of travel.")]
    public float torqueMultiplier = 10;
    [Tooltip("How much upward force will be applied when the player jumps.")]
    public float jumpForce = 10;
    [Tooltip("What layers count as ground here?")]
    public LayerMask GroundLayers;
    [Tooltip("How far down to look for the ground.")]
    public float groundHitDistance;
    [Tooltip("The delay between jumps.")]
    public float jumpSleepTime = 1;
    [Range(0.001f, 1)]
    [Tooltip("What percent of the force and torque will be available when in the air.")]
    public float airControlDivider;
    [Tooltip("The transform of the camera used to follow the player.")]
    public Transform associatedCameraTransform;
    [Header("Debug")]
    [Tooltip("Exposed only for debugging purposes, just ignore this.")]
    public bool onGround = false;
    [Tooltip("Exposed only for debugging purposes, just ignore this.")]
    public bool jumped = false;

    public static Transform CameraTrackTransform;
    
    Rigidbody rb;

    int numAlternateGroundChecks = 8;
    public float groundCheckHeightAdjustment = -0.5f;
    Vector3[] groundCheckPoints;

    void Start () {

        rb = GetComponent<Rigidbody>();
        //Create a gameobject that tracks the camera position, but the y position is set to the ball's y position
        GameObject cameraTracker = new GameObject("CameraTracker");
        cameraTracker.transform.SetParent(transform);
        CameraTrackTransform = cameraTracker.transform;
        //Associate the jump with the jump control.
        Pausemenu.InputMasterController.Hammy.Jump.performed += Jump;

        //Generate a set of points used later for checking if the ball is on the ground.
        groundCheckPoints = new Vector3[numAlternateGroundChecks + 1];
        for (int i = 0; i < numAlternateGroundChecks; i++) {
            float x = Mathf.Cos(i / (float)numAlternateGroundChecks * Mathf.PI * 2);
            float y = Mathf.Sin(i / (float)numAlternateGroundChecks * Mathf.PI * 2);
            groundCheckPoints[i] = ( new Vector3(x, groundCheckHeightAdjustment, y) + Vector3.down ).normalized;
        }
        //An extra point that checks straight down
        groundCheckPoints[numAlternateGroundChecks] = Vector3.down;
    }

    //The jump action
    void Jump (InputAction.CallbackContext context) {
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        if (onGround && !jumped) {
            rb.AddForce(Vector3.up * jumpForce);
            jumped = true;
        }
    }

    //Applying the forces during the fixed update so everything is synched with physics time
    private void FixedUpdate() {
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        //Applying the forces, but also applying the air control divider if the hammy is not on the ground or has jumped
        rb.AddForce(forwardForce * ((jumped || !onGround)? airControlDivider : 1));
        rb.AddTorque(rotationalForce * ((jumped || !onGround) ? airControlDivider : 1 ));
    }

    //Tracking for when the player is allowed to jump next
    float jumpDel = 0;
    //Variables for storing if the player is wanting to move at all
    Vector3 forwardForce = new Vector3(0, 0, 0);
    Vector3 rotationalForce = new Vector3(0, 0, 0);

    void Update () {
        //Selecting if we should use a fixed camera transform, or dynamically find it from the currently active camera
        Transform camTransform;
        if (associatedCameraTransform == null) {
            camTransform = Camera.main.transform;
        } else {
            camTransform = associatedCameraTransform;
        }

        //Checking if the ball is on the ground. Uses the points generated earlier to check a small radius under the player
        onGround = false;
        foreach (Vector3 point in groundCheckPoints) {
            onGround |= Physics.Raycast(transform.position, point, out RaycastHit hit, groundHitDistance, GroundLayers);
        }

        //Read the roll axis from the player input.
        Vector2 axes = Pausemenu.InputMasterController.Hammy.Roll.ReadValue<Vector2>();

        //Reset the camera track transform.
        CameraTrackTransform.rotation = Quaternion.identity;
        //Move the tracker to the camera's x and z position, but the current y position
        CameraTrackTransform.position = new Vector3(camTransform.transform.position.x, transform.position.y, camTransform.transform.position.z);
        //Point the tracker at the player
        CameraTrackTransform.forward = ( transform.position - CameraTrackTransform.transform.position );

        //Grabbing the forward and right axes of the tracker
        Vector3 forDir = axes.y * CameraTrackTransform.forward;
        Vector3 rightDir = axes.x * CameraTrackTransform.right;
        //Calculate the forward force vector
        forwardForce = forDir + rightDir;
        forwardForce.Normalize();
        forwardForce *= forceMultiplier;
        //Calculate the rotational force vector
        rotationalForce = (( axes.x * -1 * CameraTrackTransform.forward ) + ( axes.y * CameraTrackTransform.right )).normalized * torqueMultiplier;

        //Timer for resetting the jump
        if (jumped) {
            jumpDel += Time.deltaTime;
            if (jumpDel > jumpSleepTime) {
                jumpDel = 0;
                jumped = false;
            }
        }
    }

    private void OnDrawGizmosSelected () {
        //Draw the ray that looks for the ground.
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundHitDistance);
        if (groundCheckPoints != null) {
            foreach (Vector3 point in groundCheckPoints) {
                Gizmos.DrawLine(transform.position, transform.position + point * groundHitDistance);
            }
        }
    }

    private void OnDestroy () {
        Pausemenu.InputMasterController.Hammy.Jump.performed -= Jump;
    }
}
