using HammyFarming.Brian.GameManagement;
using HammyFarming.Brian.Utils.Timing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HammyFarming.Camera {

    public class CameraMotion: MonoBehaviour {

        [Header("Manual Camera Settings")]
        [Range(0, 360)]
        public float horizontalRotation = 0;
        [Range(-80, 80)]
        public float verticalRotation = 0;
        public float zoom = 0;

        public float horizontalSensitivity = 1;
        public float verticalSensitivity = 1;
        public float zoomSensitivity = 1;

        public float horizontalSmoothing = 1;
        public float verticalSmoothing = 1;
        public float zoomSmoothing = 1;
        public float minZoomDistance = 2.25f;
        public float maxZoomDistance = 10;

        [Header("Camera Asisst Settings")]
        public Vector2 followDistanceConstraint = new Vector2(2f, 4f);
        public float asisstModeVerticalOffset = 2f;
        public bool cameraAsisstMode = true;

        float smoothedHorizontal;
        float smoothedVertical;
        float smoothedZoom;


        private Transform _target;
        public Transform target {
            get {
                return _target;
            }
            set {
                _target = value;

                enabled = target != null;

                if (enabled)
                    assistFixedPosition = target.position + Quaternion.Euler(new Vector3(verticalRotation, horizontalRotation, 0)) * ( zoom * Vector3.back );
            }
        }

        [Header("Other Settings")]

        public LayerMask cameraCollisionLayers;
        public float forwardPushOnCollide = 0.1f;

        private bool _managed = false;
        [HideInInspector]
        public bool managed {
            get { return _managed; }
            set {
                if (value != _managed)
                    DoTransition();

                _managed = value;
                //This should trigger a smoothed transition???
            }
        }

        Vector3 assistFixedPosition = Vector3.zero;
        Timeout transitionTimeout;
        Vector3 transitionStartPosition = Vector3.zero;

        [HideInInspector]
        public Transform managedTargetTransform;

        private void Awake () {
            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Camera.ActivateCameraAsisst.performed += ActivateCameraAsisst;
            transitionTimeout = new Timeout(0.10f);

            if (target == null) {
                enabled = false;
            }

        }

        void ActivateCameraAsisst(InputAction.CallbackContext context) {
            if (!cameraAsisstMode) {
                cameraAsisstMode = true;
                assistFixedPosition = transform.position;
                DoTransition();
            }
        }

        private void OnDestroy () {
            HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Camera.ActivateCameraAsisst.performed -= ActivateCameraAsisst;
        }

        void LateUpdate () {

            if (target == null)
                return;

            //Grabbing the deltas from the input class
            Vector2 cameraDelta = HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Camera.Look.ReadValue<Vector2>();
            float zoomDelta = HammyFarming.Brian.GameManagement.PlayerInput.ControlMaster.Camera.Zoom.ReadValue<float>();
            if (zoomDelta != 0) {
                cameraDelta.y = 0;
            }


            if (cameraAsisstMode) {
                //Check if the player has attempted to move the camera manually
                if (cameraDelta != Vector2.zero || zoomDelta != 0) {
                    DoTransition();
                    //Player wants to control camera manually
                    cameraAsisstMode = false;
                    //Calculate the horizontal, vertical, and zoom values
                    Vector3 diff = transform.position - target.position;
                    zoom = diff.magnitude;
                    smoothedZoom = zoom;

                    horizontalRotation = -Mathf.Atan(diff.z / diff.x) * Mathf.Rad2Deg - 90;
                    if (diff.x < 0) { horizontalRotation += 180; }
                    smoothedHorizontal = horizontalRotation;
                    verticalRotation = Mathf.Rad2Deg * Mathf.Asin(( target.position.y + asisstModeVerticalOffset ) / (zoom + minZoomDistance));
                    smoothedVertical = verticalRotation;
                    
                    if (float.IsNaN(smoothedHorizontal)) {
                        horizontalRotation = 0;
                        smoothedHorizontal = 0;
                    }
                    if (float.IsNaN(smoothedVertical)) {
                        verticalRotation = 0;
                        smoothedVertical = 0;
                    }

                    if (float.IsNaN(smoothedZoom)) {
                        zoom = 0;
                        smoothedZoom = 0;
                    }
                }
            }

            Vector3 desiredPosition = transform.position;


            if (managed) {
                desiredPosition = GetManagedPosition(desiredPosition);
            } else {
                //Camera assist mode
                if (cameraAsisstMode) {
                    desiredPosition = GetAssistPosition();
                } else {
                    desiredPosition = GetManualPosition(desiredPosition, cameraDelta, zoomDelta);
                }
            }

            //Detecting if there is anything obstructing the camera from reaching the desired position.

            bool collided = false;
            Vector3 direction = desiredPosition - target.position;

            if (Physics.Raycast(target.position, direction.normalized, out RaycastHit hit, direction.magnitude, cameraCollisionLayers)) {
                //If something is obstructing the camera, put the camera at that point
                desiredPosition = hit.point;
                collided = true;
            }

            if (transitionTimeout.running) {
                transform.position = Vector3.Slerp(transitionStartPosition, desiredPosition, transitionTimeout.NormalizedTime);
            } else {
                transform.position = desiredPosition;
            }

            transform.LookAt(target);

            if (collided) {
                transform.position += transform.forward * forwardPushOnCollide;
            }

            if (transitionTimeout.Tick(Time.deltaTime)) {
                transitionTimeout.Reset();
            }
        }

        private Vector3 GetManagedPosition(Vector3 inPosition) {
            //In managed mode, it is assumed that there is a target position given. We are simply trying to lerp to that position.
            //If given a to target, the camera will try it's best to go there.
            if (managedTargetTransform == null) {//If no target was set, just go back to normal mode
                managed = false;
                return inPosition;
            }

            Vector3 delta = inPosition - managedTargetTransform.position;

            float mag = delta.magnitude;

            return Vector3.Slerp(inPosition, managedTargetTransform.position, mag * Time.deltaTime);
        }

        private Vector3 GetAssistPosition() {
            //Difference between our current and desired position
            Vector3 awayFromTarget = assistFixedPosition - target.position;
            float magnitude = awayFromTarget.magnitude;

            awayFromTarget.y = 0;
            //If we are too close
            if (magnitude < followDistanceConstraint.x) {
                //Attempt at smoothing the sudden bump from the boundaries

                float underage = magnitude - followDistanceConstraint.x;
                Vector3 target = assistFixedPosition - ( awayFromTarget.normalized * underage );
                assistFixedPosition = Vector3.Slerp(assistFixedPosition, target, underage * Time.deltaTime);

                //assistFixedPosition -= awayFromTarget.normalized * ( underage );
            //If we are too far away
            } else if (magnitude > followDistanceConstraint.y) {

                float overage = magnitude - followDistanceConstraint.y;
                Vector3 target = assistFixedPosition - ( awayFromTarget.normalized * overage );
                assistFixedPosition = Vector3.Slerp(assistFixedPosition, target, overage * Time.deltaTime);

                //assistFixedPosition -= awayFromTarget.normalized * ( overage );
            }

            assistFixedPosition = new Vector3(assistFixedPosition.x, target.position.y + asisstModeVerticalOffset, assistFixedPosition.z);

            return assistFixedPosition;
        }

        private Vector3 GetManualPosition(Vector3 inPosition, Vector2 cameraDelta, float zoomDelta) {
            //Manual camera mode

            //The horizontal rotation of the camera
            horizontalRotation += cameraDelta.x *
                horizontalSensitivity * Time.deltaTime *
                ( ( HammyFarming.Brian.GameManagement.GameSettings.HorizontalInverted ) ? -1 : 1 ) *
                HammyFarming.Brian.GameManagement.GameSettings.HorizontalSensitivity;

            //The vertical rotation of the camera
            verticalRotation += cameraDelta.y * verticalSensitivity * Time.deltaTime *
                ( ( HammyFarming.Brian.GameManagement.GameSettings.VerticalInverted ) ? -1 : 1 ) *
                HammyFarming.Brian.GameManagement.GameSettings.VerticalSensitivity;

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

            return target.position + Quaternion.Euler(new Vector3(smoothedVertical, smoothedHorizontal, 0)) * ( smoothedZoom * Vector3.back );
        }

        private void DoTransition() {
            transitionStartPosition = transform.position;
            transitionTimeout.ReStart();
        }

        private void OnValidate () {
            //Keep the constraints sensible.
            followDistanceConstraint.x = Mathf.Clamp(followDistanceConstraint.x, 0f, followDistanceConstraint.y - 0.01f);
            followDistanceConstraint.y = Mathf.Clamp(followDistanceConstraint.y, followDistanceConstraint.x + 0.01f, int.MaxValue);

            minZoomDistance = Mathf.Clamp(minZoomDistance, 0.5f, maxZoomDistance - 0.01f);
            maxZoomDistance = Mathf.Clamp(maxZoomDistance, minZoomDistance + 0.01f, int.MaxValue);
        }

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, followDistanceConstraint.x);
            Gizmos.DrawWireSphere(transform.position, followDistanceConstraint.y);
        }
    }
}
