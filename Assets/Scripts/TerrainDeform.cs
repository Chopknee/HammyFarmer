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

    Average velocityAverage;

    public float minimumDeformationVelocity = 0.25f;
    float minDefVelSquared;

    [Range(-1f, 1f)]
    public float deformWeight = 1;
    [Range(-1f, 1f)]
    public float tillWeight = 1;
    [Range(-1f, 1f)]
    public float waterWeight = 1;
    public bool additiveOnly = true;

    public ParticleSystem[] deformParticles;
    bool systemsPlaying = false;

    void Start () {
        rb = GetComponent<Rigidbody>();
        fieldRollingAS = gameObject.AddComponent<AudioSource>();
        fieldRollingAS.clip = fieldSound;
        fieldRollingAS.playOnAwake = false;
        fieldRollingAS.loop = true;
        fieldRollingAS.volume = soundVolume;
        minDefVelSquared = minDefVelSquared * minDefVelSquared;
        velocityAverage = new Average(10);
        origDrag = rb.drag;
        
    }

    void Update () {
        if (isOnField) {
            if (rb.velocity.sqrMagnitude > minDefVelSquared) {
                //While the object is within a farm field trigger, run that field's deform function.
                Vector3 weights = new Vector3(deformWeight, tillWeight, waterWeight);
                ffd.Deform(gameObject, stampMap, Time.deltaTime, stampScale, weight, weights, additiveOnly, rb.velocity);
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
                fieldRollingAS.pitch = Mathf.Min(Mathf.Max(velocityAverage.GetNext(vel) * pitchSpeedMultiplier, pitchMin), pitchMax);
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
