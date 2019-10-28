using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class controls the camera view. (There's alot going on in this class and I'm not going to describe most of it..)
public class StickVisualizer: MonoBehaviour {
    [Header("Starting Camera Position")]
    [Tooltip("The starting zoom of the camera.")]
    [Range(0f, 1f)]
    public float zoomPercent = 0.5f;
    [Tooltip("The starting rotation of the camera in radians.")]
    [Range(0, Mathf.PI * 2)]
    public float horizontalRotation;
    [Tooltip("The starting vertical position of the camera.")]
    [Range(0f, 1f)]
    public float vertical = 0.5f;//between 0 and 1
    [Header("Movement Modifiers")]
    [Tooltip("How sensitive the zoom is for controls in general. For fine tuning each controller type, find the input master and ajust there.")]
    public float zoomSensitivity = -3;
    [Tooltip("How much smoothing to apply to zoom.")]
    public float zoomSmoothing = 10;
    [Tooltip("How sensitive the horizontal axis is for controls in general. For fine tuning each controller type, find the input master and ajust there.")]
    public float horizontalSensitivity = 0.15f;
    [Tooltip("How much smoothing to apply to the horizontal look axis.")]
    public float horizontalSmoothing = 10;
    [Tooltip("How sensitive the vertical axis is for controls in general. For fine tuning each controller type, find the input master and ajust there.")]
    public float verticalSensitivity = 0.15f;
    [Tooltip("How much smoothing to apply to the vertical look axis.")]
    public float verticalSmoothing = 10f;
    [Tooltip("How much farther up should the camera be allowed to go zoomed out vs. zoomed in.")]
    public float heightZoomScalar = 2;
    [Tooltip("The base minimum and maximum height the camera can go.")]
    public Vector2 heightConstraintRange = new Vector2(0, 8.9f);
    [Tooltip("The distance the camera is allowed to go from the player.")]
    public Vector2 distanceConstraint = new Vector2(5, 6);
    [Tooltip("A curve to apply to the zooming. (May remove at some point.)")]
    public AnimationCurve zoomCurve;

    float horizontalSmoothed;
    float zoomSmoothed;
    float verticalSmoothed;
    float PI2 = Mathf.PI * 2;
    GameObject hammyCam;

    void Start () {
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

        float zoomDelta = zoom * Time.deltaTime * zoomSensitivity;
        zoomPercent += zoomDelta;
        zoomPercent = Mathf.Clamp(zoomPercent, 0, 1);
        zoomSmoothed += ( zoomPercent - zoomSmoothed ) * Time.deltaTime * zoomSmoothing;

        //Calculate the position!!
        hammyCam.transform.localPosition = Vector3.zero;
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Lerp(heightConstraintRange.x, heightConstraintRange.y + ( heightZoomScalar * zoomSmoothed ), verticalSmoothed);
        float dist = Mathf.Lerp(distanceConstraint.x, distanceConstraint.y, zoomCurve.Evaluate(zoomSmoothed));
        pos.z = -dist;

        hammyCam.transform.localPosition = pos;
        //Figure out what x rotation to apply to the camera
        //A^2 + B^2 = C^2
        hammyCam.transform.localEulerAngles = new Vector3(Mathf.Atan(pos.z / pos.y) * Mathf.Rad2Deg + 90, 0, 0);

        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 1, 0), Mathf.Rad2Deg * horizontalSmoothed);

        if (Pausemenu.SMBMode) {
            Vector2 stick = Pausemenu.InputMasterController.Hammy.Roll.ReadValue<Vector2>();
            transform.Rotate(new Vector3(-stick.y, 0, stick.x), stick.magnitude * 25f);
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
