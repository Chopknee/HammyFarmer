using UnityEngine;

namespace HammyFarming.Brian.Animation {

    public class KeepUpright: MonoBehaviour {

        Rigidbody rb;
        public float verticalAttractorTime = 1;
        public float attractStartAngle = 15;

        private void Start () {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate () {
            // Get our local Y+ vector (relative to world)
            Vector3 localUp = transform.up;
            // Now build a rotation taking us from our current Y+ vector towards the actual world Y+
            Quaternion vertical = Quaternion.FromToRotation(localUp, Vector3.up) * rb.rotation;
            // How far are we tilted off the ideal Y+ world rotation?
            float angleVerticalDiff = Quaternion.Angle(rb.rotation, vertical);
            if (angleVerticalDiff > attractStartAngle) // Greater than 30 degrees, start the vertical attractor
            {
                // Slerp blend based on our current rotation
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, vertical, Time.deltaTime * verticalAttractorTime));
            }
        }
    }
}
