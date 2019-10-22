using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//So basically, this takes over the main camera and does the stuff..
public class StickVisualizer: MonoBehaviour {

    Transform parent;

    public static Transform trans;

    bool lastSMBMode;

    public float zoomSensitivity = -3;
    public float zoomPercent = 0.5f;
    float zoomSpeed;

    public float horizontalSensitivity = 0.15f;
    public float horizontalSmoothing = 10;
    float horizontalSmoothed;
    float horizontalRotation;

    public float zoomDrag = 4.15f;

    public float verticalSensitivity = 0.15f;
    public float verticalSmoothing = 10f;
    public float vertical = 0.5f;//between 0 and 1
    float verticalSmoothed;
    public float heightZoomScalar = 2;

    public Vector2 heightConstraintRange = new Vector2(0, 8.9f);
    public Vector2 distanceConstraint = new Vector2(5, 6);
    public AnimationCurve zoomCurve;

    float PI2 = Mathf.PI * 2;

    GameObject hammyCam;

    void Start () {
        if (Pausemenu.SMBMode) {
            parent = transform.parent;
            trans = transform;
        }
        hammyCam = GetComponentInChildren<Camera>().gameObject;
    }

    void LateUpdate () {
        Vector2 cameraAxis = Pausemenu.InputMasterController.Hammy.Look.ReadValue<Vector2>();
        float zoom = Pausemenu.InputMasterController.Hammy.Zoom.ReadValue<float>();
        if (zoom != 0) {
            cameraAxis.y = 0;
        }

        int vDir = ( Pausemenu.VerticalInverted ) ? -1 : 1;
        float verticalDelta = cameraAxis.y * Time.deltaTime * verticalSensitivity * vDir;
        vertical += verticalDelta;
        vertical = Mathf.Clamp(vertical, 0, 1);
        verticalSmoothed += ( vertical - verticalSmoothed ) * Time.deltaTime * verticalSmoothing;

        //Rotational control over the tube thing.
        int hDir = ( Pausemenu.HorizontalInverted ) ? -1 : 1;
        float horizontalDelta = cameraAxis.x * Time.deltaTime * horizontalSensitivity * hDir;

        horizontalRotation += horizontalDelta;
        horizontalSmoothed += ( horizontalRotation - horizontalSmoothed ) * Time.deltaTime * horizontalSmoothing;
        // Keeping rotation within the 0 to 2 * PI range
        if (horizontalRotation >= PI2) {
            horizontalRotation -= PI2;
            horizontalSmoothed -= PI2;
        } else if (horizontalRotation < 0) {
            horizontalRotation += PI2;
            horizontalSmoothed += PI2;
        }


        //Update speed (scroll wheel)
        zoomSpeed += zoom * zoomSensitivity * Time.deltaTime;
        zoomSpeed += -zoomSpeed * zoomDrag * Time.deltaTime;
        //Update the zoom amount
        zoomPercent = Mathf.Clamp01(zoomPercent + zoomSpeed);
        //Zero out values that are extremely low
        if (zoomSpeed > -0.001f && zoomSpeed < 0.001f) {
            zoomSpeed = 0;
        }

        //Calculate the position!!
        hammyCam.transform.localPosition = Vector3.zero;
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Lerp(heightConstraintRange.x, heightConstraintRange.y + ( heightZoomScalar * zoomPercent ), verticalSmoothed);
        float dist = Mathf.Lerp(distanceConstraint.x, distanceConstraint.y, zoomCurve.Evaluate(zoomPercent));
        pos.z = -dist;

        hammyCam.transform.localPosition = pos;
        //Figure out what x rotation to apply to the camera
        //A^2 + B^2 = C^2
        //hammyCam.transform.localRotation.eulerAngles = 
        hammyCam.transform.localEulerAngles = new Vector3(Mathf.Atan(pos.z / pos.y) * Mathf.Rad2Deg + 90, 0, 0);

        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 1, 0), Mathf.Rad2Deg * horizontalSmoothed);

        if (Pausemenu.SMBMode) {
            Vector2 stick = Pausemenu.InputMasterController.Hammy.Roll.ReadValue<Vector2>();
            //transform.forward = BallControl.CameraTrackTransform.forward;
            transform.Rotate(new Vector3(-stick.y, 0, stick.x), stick.magnitude * 25f);
            //transform.up += new Vector3(stick.x, 1, stick.y).normalized;
        }

        Physics.gravity = (transform.up).normalized * -9.81f;
    }

    private void OnValidate () {
        if (!Application.isPlaying) {
            //Height constraint x is the min, must be above 0.
            heightConstraintRange.x = Mathf.Max(0, heightConstraintRange.x);
            //Height constrain y is the max, it must be above the min
            heightConstraintRange.y = Mathf.Max(heightConstraintRange.x + 0.1f, heightConstraintRange.y);

            distanceConstraint.x = Mathf.Max(0, distanceConstraint.x);
            distanceConstraint.y = Mathf.Max(distanceConstraint.x + 0.1f, distanceConstraint.y);
        }
    }
}
