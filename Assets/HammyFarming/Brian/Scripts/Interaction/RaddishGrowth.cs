using HammyFarming.Brian.Utils.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaddishGrowth : MonoBehaviour {

    public float growTime = 10;
    public float soilCheckTime = 1;

    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one * 4;

    public Timeout growTimer;

    public Timeout soilCheckTimer;

    public bool growing;
    public Color colorUnderMe;
    [Range(0f, 1f)]
    public float minimumTilledness;
    [Range(0f, 1f)]
    public float minimumWetness;
    public LayerMask fieldMask;

    MaterialPropertyBlock mat;

    public float growPercent {
        get {
            return growTimer.NormalizedTime;
        }
    }


    void Start() {
        growTimer = new Timeout(growTime);

        soilCheckTimer = new Timeout(soilCheckTime);
        soilCheckTimer.Start();

        mat = new MaterialPropertyBlock();
        GetComponent<Renderer>().SetPropertyBlock(mat, 1);
    }

    void Update() {
        transform.localScale = Vector3.Lerp(startScale, endScale, growTimer.NormalizedTime);

        if (growTimer.Tick(Time.deltaTime)) {
            //Debug.Log("Raddish has grown!");
            //counter.Reset();
            //counter.Start();
        }

        if (soilCheckTimer.Tick(Time.deltaTime)) {
            //Do the soil check
            soilCheckTimer.ReStart();

            if (Physics.Raycast(transform.position + ( Vector3.up * 5 ), Vector3.down, out RaycastHit hit, 50, fieldMask)) {
                //Only collides with the field.
                //Now checking the color under the seed. (probably gonna do an average under the seed at some point.
                colorUnderMe = hit.collider.gameObject.GetComponent<FarmFieldDeformation>().GetFieldValuesAt(hit.textureCoord);
                growing = colorUnderMe.g >= minimumTilledness && colorUnderMe.b >= minimumWetness;

                if (growing) {
                    growTimer.Start();
                    SetTopColor(new Color(0.31f, 0.75f, 0.38f));
                    //Debug.Log("Growing!");
                } else {
                    growTimer.Pause();
                    SetTopColor(new Color(0.75f, 0.57f, 0.31f));
                    //Debug.Log("Poor soil conditions, not growing!");
                }

            } else {
                growing = false;
                growTimer.Pause();
                colorUnderMe = Color.black;
                growTimer.Pause();
                SetTopColor(new Color(0.75f, 0.57f, 0.31f));
                //Debug.Log("Not over farm field, growth halted!");
            }
        }
    }

    void SetTopColor(Color col) {
        mat.SetColor("_BaseColor", col);
        GetComponent<Renderer>().SetPropertyBlock(mat, 1);
    }

    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Harvester")) {
            //Do the harvesting!!!
            other.GetComponent<Harvester>().Harvest(this);
        }
    }

    private void OnValidate () {
        startScale = Vector3.Max(startScale, Vector3.one * 0.001f);
        endScale = Vector3.Max(startScale * 1.001f, endScale);
        growTime = Mathf.Clamp(growTime, 0.001f, 3600);
    }
}
