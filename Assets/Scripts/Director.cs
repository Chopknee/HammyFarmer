using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {

    public static Director Instance;

    public delegate void SiloFillChanged ( float value );
    public SiloFillChanged OnSiloFillChanged;

    public void Start () {
        Instance = this;
    }

    float _siloFill;
    public float SiloFillLevel {
        get {
            return _siloFill;
        }
        set {
            _siloFill = value;
            OnSiloFillChanged?.Invoke(value);
        }
    }

    public float SiloFillGoal = 100;
    public float FullGrowthScore = 5;

    void Awake() {
        //Prevent multiple director instances from existing.
        if (Instance != null) {
            Destroy(this);
            return;
        }
    }
}
