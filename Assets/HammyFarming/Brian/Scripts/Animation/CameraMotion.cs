using HammyFarming.Brian.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Brian.Animation {

    public class CameraMotion: MonoBehaviour {

        //This is just for the target camera position, not the camera it'self
        //It is assumed that this is part of a parent game object.
        [Header("Manual Camera Settings")]
        [Range(0, 360)]
        public float horizontalRotation = 0;
        [Range(-90, 90)]
        public float verticalRotation = 0;
        public float zoom = 0;

        public float horizontalSensitivity = 1;
        public float verticalSensitivity = 1;
        public float zoomSensitivity = 1;

        public float horizontalSmoothing = 1;
        public float verticalSmoothing = 1;
        public float zoomSmoothing = 1;

        public float maxZoomDistance = 10;

        [Header("Camera Asisst Settings")]
        public Vector2 followDistanceConstraint = new Vector2(2f, 4f);
        public float asisstModeVerticalOffset = 2f;
        public bool cameraAsisstMode = true;

        float smoothedHorizontal;
        float smoothedVertical;
        float smoothedZoom;

        
        [Header("Other Settings")]
        public Transform target;

        public LayerMask cameraCollisionLayers;
        public float forwardPushOnCollide = 0.1f;

        private void Awake () {

            //Pre-initialize position for camera asisst mode
            targetedPosition = target.position + Quaternion.Euler(new Vector3(verticalRotation, horizontalRotation, 0)) * ( zoom * Vector3.back );
            Director.InputMasterController.Hammy.ActivateCameraAsisst.performed += ActivateCameraAsisst;
        }

        Vector3 targetedPosition;


        void ActivateCameraAsisst(InputAction.CallbackContext context) {
            if (!cameraAsisstMode) {
                cameraAsisstMode = true;//Don't totally care about fixing it up.
                targetedPosition = transform.position;
            }
        }

        void LateUpdate () {

            if (target == null)
                return;

            //Grabbing the deltas from the input class
            Vector2 cameraDelta = Director.InputMasterController.Hammy.Look.ReadValue<Vector2>();
            float zoomDelta = Director.InputMasterController.Hammy.Zoom.ReadValue<float>();
            if (zoomDelta != 0) {
                cameraDelta.y = 0;
            }


            if (cameraAsisstMode) {
                //Check if the player has attempted to move the camera manually
                if (cameraDelta != Vector2.zero || zoomDelta != 0) {
                    //Player wants to control camera manually
                    cameraAsisstMode = false;
                    //Calculate the horizontal, vertical, and zoom values
                    Vector3 diff = transform.position - target.position;
                    zoom = diff.magnitude;
                    smoothedZoom = zoom;

                    Vector3 from = target.position - transform.position;
                    horizontalRotation = Vector3.SignedAngle(from, transform.forward, Vector3.up) + 180;
                    smoothedHorizontal = horizontalRotation;
                    verticalRotation = Mathf.Acos(( target.position.y + asisstModeVerticalOffset ) / zoom);
                    smoothedVertical = verticalRotation;
                }
            }

            Vector3 desiredPosition = targetedPosition;

            if (cameraAsisstMode) {

                Vector3 diff = desiredPosition - target.position;
                float dist = diff.magnitude;

                diff.y = 0;
                if (dist < followDistanceConstraint.x) {
                    desiredPosition -= diff.normalized * (dist-followDistanceConstraint.x);
                } else if (dist > followDistanceConstraint.y) {
                    desiredPosition -= diff.normalized * (dist-followDistanceConstraint.y);
                }

                targetedPosition = new Vector3(desiredPosition.x, target.position.y + asisstModeVerticalOffset, desiredPosition.z);

            } else {

                //The horizontal rotation of the camera
                horizontalRotation += cameraDelta.x * horizontalSensitivity * Time.deltaTime * ( ( Pausemenu.HorizontalInverted ) ? -1 : 1 );

                //The vertical rotation of the camera
                verticalRotation += cameraDelta.y * verticalSensitivity * Time.deltaTime * ( ( Pausemenu.VerticalInverted ) ? -1 : 1 );
                verticalRotation = Mathf.Clamp(verticalRotation, -80, 80);


                //Zoom Control
                zoom += -1 * zoomDelta * zoomSensitivity * Time.deltaTime;
                zoom = Mathf.Clamp(zoom, 2.25f, maxZoomDistance);


                if (Time.deltaTime < 0.036f) {//Checking if the 
                    smoothedHorizontal += ( horizontalRotation - smoothedHorizontal ) * Time.deltaTime * horizontalSmoothing;
                    smoothedVertical += ( verticalRotation - smoothedVertical ) * Time.deltaTime * verticalSmoothing;
                    smoothedZoom += ( zoom - smoothedZoom ) * Time.deltaTime * zoomSmoothing;
                } else {
                    smoothedHorizontal = horizontalRotation;
                    smoothedVertical = verticalRotation;
                    smoothedZoom = zoom;
                }

                //
                desiredPosition = target.position + Quaternion.Euler(new Vector3(smoothedVertical, smoothedHorizontal, 0)) * ( smoothedZoom * Vector3.back );
            }

            //Detecting if there is anything obstructing the camera from reaching the desired position.

            bool collided = false;
            Vector3 direction = desiredPosition - target.position;

            if (Physics.Raycast(target.position, direction.normalized, out RaycastHit hit, direction.magnitude, cameraCollisionLayers)) {
                //If something is obstructing the camera, put the camera at that point
                desiredPosition = hit.point;
                collided = true;
            }

            transform.position = desiredPosition;
            transform.LookAt(target);

            if (collided) {
                transform.position += transform.forward * forwardPushOnCollide;
            }
        }

        private void OnValidate () {
            //Keep the constraints sensible.
            followDistanceConstraint.x = Mathf.Clamp(followDistanceConstraint.x, 0f, followDistanceConstraint.y - 0.01f);
            followDistanceConstraint.y = Mathf.Clamp(followDistanceConstraint.y, followDistanceConstraint.x + 0.01f, int.MaxValue);
        }

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, followDistanceConstraint.x);
            Gizmos.DrawWireSphere(transform.position, followDistanceConstraint.y);
        }
    }
}
