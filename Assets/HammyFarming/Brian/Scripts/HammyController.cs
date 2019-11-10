using HammyFarming.Brian.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Not something for adjusting. Don't mess with this. It is a huge work in progress.
public class HammyController: MonoBehaviour {

    Rigidbody rb;
    Animator hammyAnimator;

    public AnimationCurve AccelerateCurve;

    void Start () {
        rb = GetComponentInParent<Rigidbody>();
        hammyAnimator = GetComponentInChildren<Animator>();
        lastForward = Vector3.zero; 
    }

    float t = 0;

    Vector3 lastForward = Vector3.zero;

    void LateUpdate () {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float speed = flatVelocity.magnitude;

        t += Time.deltaTime * speed;
        float a = (Mathf.Sin(t) + 1) / 2;

        hammyAnimator.SetFloat("Runspeed", a);
        hammyAnimator.SetFloat("Animation State", 1 - Mathf.Clamp(rb.velocity.magnitude, 0, 1));

        Vector3 forward = transform.forward;
        Vector2 stick = Pausemenu.InputMasterController.Hammy.Roll.ReadValue<Vector2>();
        if (stick.magnitude > 0.1f) {
            forward = stick.y * BallControl.CameraTrackTransform.forward + stick.x * BallControl.CameraTrackTransform.right;
            forward.Normalize();
            transform.forward = Vector3.Lerp(lastForward, forward, Time.deltaTime * speed);
            lastForward = transform.forward;
        } else {
            lastForward = transform.forward;
        }
    }
}
