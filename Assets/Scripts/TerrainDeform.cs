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
    [Range(0f, 0.9f)]
    public float pitchMin = 0.1f;
    [Range(0.1f, 1f)]
    public float pitchMax = 0.5f;
    float[] velocityCaptures = new float[10];
    int velocityCaptureIndex = 0;
    float averageVelocity = 0;

    [Range(-1f, 1f)]
    public float deformWeight = 1;
    [Range(-1f, 1f)]
    public float tillWeight = 1;
    [Range(-1f, 1f)]
    public float waterWeight = 1;
    public bool additiveOnly = true;

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
            ffd.Deform(gameObject, stampMap, Time.deltaTime, stampScale, weight, deformWeight, tillWeight, waterWeight, additiveOnly, rb.velocity);

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
                fieldRollingAS.pitch = Mathf.Min(Mathf.Max(averageVelocity * pitchSpeedMultiplier, pitchMin), pitchMax);
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
