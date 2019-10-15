using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrowth: MonoBehaviour {

    public GameObject plantPrefab;

    public Sprite cantGrowIcon;
    public Sprite canGrowIcon;

    [Range(0f, 1f)]
    public float minimumTilledness;
    [Range(0f, 1f)]
    public float minimumWetness;
    public float growCheckTime = 1;
    public float nextStateTime = 1;
    public float deathTime = 20;

    public LayerMask fieldMask;

    public SpriteRenderer sr;

    void Start () {

    }

    float t = 0;
    float ns = 0;
    public bool growing;
    public Color colorUnderMe;

    float dt = 0;
    void Update () {
        t += Time.deltaTime;
        if (t >= growCheckTime) {
            t = 0;
            //Checking if the seed can grow.
            if (Physics.Raycast(transform.position + ( Vector3.up * 5 ), Vector3.down, out RaycastHit hit, 50, fieldMask)) {
                //Only collides with the field.
                //Now checking the color under the seed. (probably gonna do an average under the seed at some point.
                colorUnderMe = hit.collider.gameObject.GetComponent<FarmFieldDeformation>().GetFieldValuesAt(hit.textureCoord);
                growing = colorUnderMe.g > minimumTilledness && colorUnderMe.b > minimumWetness;
                if (growing) {
                    if (sr != null) { sr.sprite = canGrowIcon; }
                } else {
                    if (sr != null) { sr.sprite = cantGrowIcon; }
                }
            }
        }
        sr.color = colorUnderMe;
        if (growing) {
            ns += Time.deltaTime;
            dt = 0;
        } else {
            dt += Time.deltaTime;
            ns = 0;
        }

        if (ns > nextStateTime) {
            //Spawn the raddish
            Instantiate(plantPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (dt > deathTime) {
            Destroy(gameObject);
        }
    }
}
