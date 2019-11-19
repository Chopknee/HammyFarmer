using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TerrainDeform: MonoBehaviour {

    List<FarmFieldDeformation> intersectedFields;

    [Header("Deformation Stamp")]
    [Tooltip("The base texture used to deform the field.")]
    public Texture2D stampMap;
    [Tooltip("How big the stamp is on the field.")]
    public float stampScale = 1;
    [Tooltip("How much to apply the stamp each pass in the shader.")]
    public float weight = 1;
    [Tooltip("How fast should the object be moving before deformations happen?")]
    public float minimumDeformationVelocity = 0.25f;
    [Tooltip("What is the weight of the 'deform' or red channel.")]
    [Range(-1f, 1f)]
    public float deformWeight = 1;
    [Tooltip("What is the weight of the 'tilledness' or green channel.")]
    [Range(-1f, 1f)]
    public float tillWeight = 1;
    [Tooltip("What is the weight of the 'wateredness' or blue channel.")]
    [Range(-1f, 1f)]
    public float waterWeight = 1;
    [Tooltip("Tells if the stamp's weights should be set in a range of -1 to 1, or 0 to 1. (Just play with this to see different results.)")]
    public bool additiveOnly = true;
    [Header("Deformation Sound")]
    [Tooltip("What sound should play when deforming the field?")]
    public AudioClip fieldSound;
    AudioSource fieldRollingAS;
    [Range(0, 1f)]
    public float soundVolume = 0.75f;
    [Range(0, 2f)]
    public float pitchSpeedMultiplier;
    [Tooltip("The minimum and maximum pitch the sound may play at.")]
    public Vector2 pitchRange;
    [Header("Misc Properties")]
    [Tooltip("Particle systems that should play when deforming.")]
    public ParticleSystem[] deformParticles;
    [Tooltip("What should the drag be set to when over the field?")]
    public float mudDragValue = 1.35f;

    bool systemsPlaying = false;
    float origDrag = 0;
    Rigidbody rb;
    float minDefVelSquared;
    Average velocityAverage;

    bool isOnField {
        get {
            return intersectedFields.Count > 0;
        }
    }

    void Awake () {
        rb = GetComponent<Rigidbody>();
        fieldRollingAS = gameObject.AddComponent<AudioSource>();
        fieldRollingAS.clip = fieldSound;
        fieldRollingAS.playOnAwake = false;
        fieldRollingAS.loop = true;
        fieldRollingAS.volume = soundVolume;
        minDefVelSquared = minDefVelSquared * minDefVelSquared;
        velocityAverage = new Average(10);
        origDrag = rb.drag;

        intersectedFields = new List<FarmFieldDeformation>();
        
    }

    void Update () {
        if (isOnField) {
            if (rb.velocity.sqrMagnitude > minDefVelSquared) {
                //While the object is within a farm field trigger, run that field's deform function.
                Vector3 weights = new Vector3(deformWeight, tillWeight, waterWeight);
                foreach (FarmFieldDeformation ffd in intersectedFields) {
                    ffd.Deform(gameObject, stampMap, Time.deltaTime, stampScale, weight, weights, additiveOnly, rb.velocity);
                }
                systemsPlaying = true;
            } else {
                systemsPlaying = false;
            }
        } else {
            systemsPlaying = false;
        }

        SetParticleSystems(systemsPlaying);
    }

    void SetParticleSystems(bool playing) {
        foreach (ParticleSystem ps in deformParticles) {
            if (systemsPlaying) {
                if (!ps.isPlaying) {
                    ps.Play();
                }
            } else {
                if (ps.isPlaying) {
                    ps.Stop();
                }
            }
        }
    }

    private void FixedUpdate () {
        float vel = rb.velocity.magnitude;

        if (fieldSound != null) {
            if (isOnField) {        
                if (vel > 0.2f && !fieldRollingAS.isPlaying) {
                    fieldRollingAS.Play();
                } else if (vel < 0.2f && fieldRollingAS.isPlaying) {
                    fieldRollingAS.Stop();
                }
                fieldRollingAS.pitch = Mathf.Clamp(velocityAverage.GetNext(vel) * pitchSpeedMultiplier, pitchRange.x, pitchRange.y);
            } else {
                if (fieldRollingAS.isPlaying) {
                    fieldRollingAS.Stop();
                }
            }
        }
    }

    private void OnTriggerEnter ( Collider other ) {
        //If we collide with a farm field trigger
        if (other.tag == "FarmField") {
            //Try to grab the farm field deformation component
            FarmFieldDeformation ffd = other.gameObject.GetComponent<FarmFieldDeformation>();
            if (ffd != null && !intersectedFields.Contains(ffd)) {

                //Only if the field is not already in the list
                if (!isOnField) { 
                    rb.drag = mudDragValue;
                }

                intersectedFields.Add(ffd);
            }
        }
    }

    private void OnTriggerExit ( Collider other ) {
        //If the object exits a farm field trigger
        if (other.tag == "FarmField" && isOnField) {

            FarmFieldDeformation ffd = other.gameObject.GetComponent<FarmFieldDeformation>();

            if (ffd != null && intersectedFields.Contains(ffd)) {
                intersectedFields.Remove(ffd);
            }

            if (!isOnField) {
                rb.drag = origDrag;
            }
        }
    }

    private void OnValidate () {
        pitchRange.x = Mathf.Max(0.001f, pitchRange.x);
        pitchRange.y = Mathf.Max(pitchRange.x + 0.001f, pitchRange.y);
        pitchRange.y = Mathf.Min(1f, pitchRange.y);
    }
}
