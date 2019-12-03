using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Monsters.Bee {

    public class Bee : MonoBehaviour {

        Animator animator;

        Rigidbody rigidBody;

        public int state = 0;

        public LayerMask GroundLayers;

        public float MinRelativeAltitude = 2.0f;
        public float MaxRelativeAltitude = 5.0f;

        public float MinMoveDistance = 2.0f;
        public float MaxMoveDistance = 5.0f;

        Vector3 spawnPosition = Vector3.zero;

        Vector3 startPosition = Vector3.zero;
        Vector3 targetPosition = Vector3.zero;

        Vector3 startForward = Vector3.zero;
        Vector3 endForward = Vector3.zero;

        float currentFlappingSpeed = 8.5f;
        float startFlapSpeed = 0;
        float endFlapSpeed = 0;

        public float collisionCheckRadius;

        float groundHitDistance = 1.0f;

        HammyFarming.Brian.Utils.Timing.Timeout timer;

        public float flySpeed = 1.0f;

        public bool landed = false;
        [Range(0.0f, 1.0f)]
        public float chanceOfLanding = 0.25f;

        void Awake() {

            animator = GetComponent<Animator>();

            spawnPosition = transform.position;
            
            //Register with the level start listener
            HammyFarming.Brian.GameManagement.LevelManagement.OnLevelStart += LevelStarted;

            //enabled = false;

            timer = new Brian.Utils.Timing.Timeout(1, false);
        }

        void LevelStarted() {
            //Start the flying around.
            enabled = true;
        }

        float a = 0;

        void Update() {

            a = timer.NormalizedTime;

            if (state == 0) {
                //This is the 'fallback' state for when something goes wrong. Resets all variables and teleports back to starting position.
                transform.position = spawnPosition;
                if (rigidBody != null) {
                    Destroy(rigidBody);
                }

                //Determine if bee is on ground, or in air
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundHitDistance, GroundLayers)) {
                    //We are on the ground. Begin flying. Takeoff mode engage!
                    state = 10;
                    landed = true;
                } else {
                    //We are not on the ground. Already flying, do the standard flying motions
                    state = 20;
                    landed = false;
                }
            }
            if (state == 1) {
                //The deciding if we want to land or fly state
                if (landed) {
                    //A chance of taking back off again...
                    state = 10;//For now, just take back off
                } else {
                    //A chance of landing
                    if (Random.value < chanceOfLanding)
                        state = 30;
                    else
                        state = 20;
                }
            }

            //When the bee is taking off
            if (state == 10) {
                //On Ground state
                //Determine the target altitude
                targetPosition = transform.position + new Vector3(0.0f, Random.Range(MinRelativeAltitude, MaxRelativeAltitude), 0.0f);
                startPosition = transform.position;
                startFlapSpeed = currentFlappingSpeed;
                endFlapSpeed = Random.Range(8.0f, 9.0f);
                timer.timeoutTime = 2.5f;
                timer.Start();
                state++;
                landed = false;
            }
            if (state == 11) {
                //Ascention state
                if (timer.running) {
                    a = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(a);
                    transform.position = Vector3.Slerp(startPosition, targetPosition, a);
                    float slice = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(timer.GetNormalizedSlice(0, 0.50f));
                    currentFlappingSpeed = Mathf.Lerp(startFlapSpeed, endFlapSpeed, slice);
                    //Ascend!
                    if (timer.Tick(Time.deltaTime)) {
                        //Target altitude reached, go to flying mode
                        transform.position = targetPosition;
                        timer.Reset();
                        state = 20;
                    }
                } else
                    state = 0;
            }

            //When the bee is flying
            if (state == 20) {
                //In air state
                Vector3 travelDirection = transform.forward;
                travelDirection += transform.right * Random.Range(-40.0f, 40.0f);
                travelDirection += transform.up * Random.Range(-40.0f, 40.0f);
                travelDirection.Normalize();
                float dist = Random.Range(MinMoveDistance, MaxMoveDistance);

                

                if (!Physics.Raycast(transform.position, travelDirection, dist, GroundLayers)) {
                    //We have a clear path, move to the next stage
                    targetPosition = transform.position + ( travelDirection * dist );

                    //if the bee is colliding with the ground at the target position



                    if (Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hit, MaxRelativeAltitude, GroundLayers)) {
                        if (Physics.Raycast(targetPosition, Vector3.down, groundHitDistance, GroundLayers))
                            targetPosition = new Vector3(targetPosition.x, targetPosition.y + MinRelativeAltitude, targetPosition.z);
                    } else {
                        targetPosition = new Vector3(targetPosition.x, transform.position.y - 0.5f, targetPosition.z);
                    }

                    startPosition = transform.position;
                    startForward = transform.forward;
                    endForward = ( targetPosition - startPosition ).normalized;
                    timer.timeoutTime = (targetPosition - startPosition).magnitude / flySpeed;
                    timer.Start();
                    state++;
                }
            }
            if (state == 21) {
                if (timer.running) {
                    a = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(a);
                    transform.position = Vector3.Slerp(startPosition, targetPosition, a);
                    float slice = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(timer.GetNormalizedSlice(0.0f, 0.45f));
                    transform.forward = Vector3.Slerp(startForward, endForward, slice);
                    if (timer.Tick(Time.deltaTime)) {
                        transform.position = targetPosition;
                        transform.forward = endForward;
                        timer.Reset();
                        state = 1;
                    }
                } else
                    state = 0;
            }

            //When the bee is landing
            if (state == 30) {
                //Initiate landing
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, MaxRelativeAltitude, GroundLayers)) {

                    targetPosition = hit.point;
                    startPosition = transform.position;
                    timer.timeoutTime = ( targetPosition - startPosition ).magnitude / flySpeed;

                    startForward = transform.forward;
                    endForward = new Vector3(startForward.x, 0, startForward.y).normalized;

                    timer.Start();
                    landed = true;
                    state++;
                    startFlapSpeed = currentFlappingSpeed;
                    endFlapSpeed = Random.Range(0.5f, 1.5f);

                } else
                    state = 1;
            }
            if (state == 31) {
                //Running the landing sequence
                if (timer.running) {
                    a = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(a);
                    transform.position = Vector3.Slerp(startPosition, targetPosition, a);
                    float forwardSlice = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(timer.GetNormalizedSlice(0.0f, 0.45f));
                    transform.forward = Vector3.Slerp(startForward, endForward, forwardSlice);
                    float flappingSlice = HammyFarming.Brian.Utils.Easing.EaseInOutCubic(timer.GetNormalizedSlice(0.50f, 1));
                    currentFlappingSpeed = Mathf.Lerp(startFlapSpeed, endFlapSpeed, flappingSlice);
                    if (timer.Tick(Time.deltaTime)) {
                        transform.position = targetPosition;
                        transform.forward = endForward;
                        timer.Reset();
                        timer.timeoutTime = Random.Range(4.0f, 8.0f);
                        timer.Start();
                        state = 32;
                    }
                } else
                    state = 0;
            }
            if (state == 32) {
                //Waiting for a short period before continuing
                if (timer.running) {
                    if (timer.Tick(Time.deltaTime)) {
                        timer.Reset();
                        state = 1;
                    }
                } else
                    state = 0;
            }

            if (state == 40) {
                //Attacking state??
            }

            animator.SetFloat("FlapSpeed", currentFlappingSpeed);
        }

        private void OnDrawGizmosSelected () {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + transform.up * MinRelativeAltitude, transform.position + transform.up * MaxRelativeAltitude);
        }
    }
}