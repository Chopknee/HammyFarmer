using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Animation {

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

        public float horizontalSmoothing = 1;
        public float verticalSmoothing = 1;
        public float zoomSmoothing = 1;

        float smoothedHorizontal;
        float smoothedVertical;
        float smoothedZoom;

        public float maxZoomDistance = 10;

        public Transform target;

        public LayerMask cameraCollisionLayers;
        public float forwardPushOnCollide = 0.1f;

        void LateUpdate () {

            if (target == null)
                return;

            //Grabbing the deltas from the input class
            Vector2 cameraDelta = Pausemenu.InputMasterController.Hammy.Look.ReadValue<Vector2>();
            float zoomDelta = Pausemenu.InputMasterController.Hammy.Zoom.ReadValue<float>();
            if (zoomDelta != 0) {
                cameraDelta.y = 0;
            }

            //The horizontal rotation of the camera
            horizontalRotation += cameraDelta.x * horizontalSensitivity * Time.deltaTime * ( ( Pausemenu.HorizontalInverted ) ? -1 : 1 );

            //The vertical rotation of the camera
            verticalRotation += cameraDelta.y * verticalSensitivity * Time.deltaTime * ( ( Pausemenu.VerticalInverted ) ? -1 : 1 );
            verticalRotation = Mathf.Clamp(verticalRotation, -89, 89);


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
            Vector3 desiredPosition = target.position + Quaternion.Euler(new Vector3(smoothedVertical, smoothedHorizontal, 0)) * ( smoothedZoom * Vector3.back );

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


        //Just for the editor
        private void OnValidate () {

        }
    }
}
