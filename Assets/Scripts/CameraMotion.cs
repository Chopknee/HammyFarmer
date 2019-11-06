using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion: MonoBehaviour {

    //This is just for the target camera position, not the camera it'self
    //It is assumed that this is part of a parent game object.
    [Range(0, 360)]
    public float horizontalRotation = 0;
    [Range(-90, 90)]
    public float verticalRotation = 0;
    public float zoom = 0;

    public float horizontalSensitivity = 1;
    public float verticalSensitivity = 1;
    public float zoomSensitivity = 1;

    public Transform target;

    void Start () {
    }

    void LateUpdate () {

        if (target == null)
            return;

        Vector2 cameraDelta = Pausemenu.InputMasterController.Hammy.Look.ReadValue<Vector2>();
        float zoomDelta = Pausemenu.InputMasterController.Hammy.Zoom.ReadValue<float>();
        if (zoomDelta != 0) {
            cameraDelta.y = 0;
        }

        horizontalRotation += cameraDelta.x * horizontalSensitivity * Time.deltaTime;
        verticalRotation += cameraDelta.y * verticalSensitivity * Time.deltaTime;
        zoom += zoomDelta * zoomSensitivity * Time.deltaTime;

        if (verticalRotation > 89) {
            verticalRotation = 89;
        } else if (verticalRotation < -89) {
            verticalRotation = -89;
        }

        

        transform.position = target.position + Quaternion.Euler(new Vector3(verticalRotation, horizontalRotation, 0)) * (zoom * Vector3.back);
        transform.LookAt(target);

    }


    //Just for the editor
    private void OnValidate () {

    }
}
