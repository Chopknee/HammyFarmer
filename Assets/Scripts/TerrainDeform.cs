using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TerrainDeform: MonoBehaviour {

    bool isOnField = false;
    FarmFieldDeformation ffd;

    public Texture2D stampMap;
    public float stampScale = 1;
    public float weight = 1;

    public float mudDragValue = 1.35f;
    float origDrag = 0;

    Rigidbody rb;

    public AudioClip fieldSound;
    AudioSource fieldRollingAS;
    [Range(0, 1f)]
    public float soundVolume = 0.75f;
    [Range(0, 2f)]
    public float pitchSpeedMultiplier;

    float[] velocityCaptures = new float[10];
    int velocityCaptureIndex = 0;
    float averageVelocity = 0;

    void Start () {
        rb = GetComponent<Rigidbody>();
        fieldRollingAS = gameObject.AddComponent<AudioSource>();
        fieldRollingAS.clip = fieldSound;
        fieldRollingAS.playOnAwake = false;
        fieldRollingAS.loop = true;
        fieldRollingAS.volume = soundVolume;
    }

    void Update () {
        if (isOnField) {
            //While the object is within a farm field trigger, run that field's deform function.
            ffd.Deform(gameObject, stampMap, Time.deltaTime, stampScale, weight);

        }
    }

    private void FixedUpdate () {
        float vel = rb.velocity.magnitude;
        velocityCaptures[velocityCaptureIndex] = vel;
        velocityCaptureIndex++;
        velocityCaptureIndex = ( velocityCaptureIndex == velocityCaptures.Length ) ? 0 : velocityCaptureIndex;
        averageVelocity = 0;
        for (int i = 0; i < velocityCaptures.Length; i++) {
            averageVelocity += velocityCaptures[i];
        }
        averageVelocity = averageVelocity / velocityCaptures.Length;

        if (fieldSound != null) {
            if (isOnField) {        
                if (vel > 0.2f && !fieldRollingAS.isPlaying) {
                    fieldRollingAS.Play();
                } else if (vel < 0.2f && fieldRollingAS.isPlaying) {
                    fieldRollingAS.Stop();
                }
                fieldRollingAS.pitch = averageVelocity * pitchSpeedMultiplier;
            } else {
                if (fieldRollingAS.isPlaying) {
                    fieldRollingAS.Stop();
                }
            }
        }
    }

    private void OnTriggerEnter ( Collider other ) {
        //If the object enters a farm field trigger;
        if (other.tag == "FarmField") {
            ffd = other.gameObject.GetComponent<FarmFieldDeformation>();
            if (ffd != null) {
                isOnField = true;
                origDrag = rb.drag;
                rb.drag = mudDragValue;
            }

        }
    }

    private void OnTriggerExit ( Collider other ) {
        //If the object exits a farm field trigger
        if (other.tag == "FarmField" && isOnField) {
            isOnField = false;
            ffd = null;
            rb.drag = origDrag;
        }
    }
}
