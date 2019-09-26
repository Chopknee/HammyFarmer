using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFieldDeformation : MonoBehaviour {

    public LayerMask fieldMask;
    public ComputeShader compute;
    public RenderTexture outputTexture;
    public Texture2D startMap;
    public Texture2D stampMap;

    public Vector2 stampPosition;
    public float stampRotation = 0;
    int kernel;
    int mapWidth = 0; int mapHeight = 0;
    public int alphaMultiplier = 1;

    bool hammyOnField = false;
    GameObject hammyBall;

    void Start() {
        mapWidth = startMap.width;
        mapHeight = startMap.height;

        kernel = compute.FindKernel("CSMain");

        outputTexture = new RenderTexture(mapWidth, mapHeight, 24);
        outputTexture.enableRandomWrite = true;
        outputTexture.Create();

        RenderTexture.active = outputTexture;
        Graphics.Blit(startMap, outputTexture);
        RenderTexture.active = null;

        compute.SetTexture(kernel, "StampMap", stampMap);
        compute.SetVector("position", new Vector4(stampPosition.x, stampPosition.y, 0, 0));
        compute.SetVector("stampSize", new Vector2(stampMap.width, stampMap.height));
        compute.SetTexture(kernel, "Result", outputTexture);
        compute.SetFloat("rotation", Mathf.Deg2Rad * stampRotation);
        compute.SetFloat("multiplier", alphaMultiplier);
        compute.SetFloat("deltaTime", 0);
        //compute.Dispatch(kernel, mapWidth / 8, mapHeight / 8, 1);

        GetComponent<MeshRenderer>().material.SetTexture("Texture2D_5564C194", outputTexture);

    }

    // Update is called once per frame
    void Update() {
        //Modify the texture here to represent hammy ball modifications.
        if (hammyOnField) {
            RaycastHit hit;
            if (Physics.Raycast(hammyBall.transform.position + ( Vector3.up * 5 ), Vector3.down, out hit, 50, fieldMask)) {

                //Figure out the absolute position of the hammy ball relative to the texture....
                stampPosition = hit.textureCoord * mapWidth;
                //Rotation can come later, right now, I wanna get this working!!!
                compute.SetVector("position", stampPosition);
                Rigidbody rb = hammyBall.GetComponent<Rigidbody>();

                Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.z).normalized;

                compute.SetFloat("deltaTime", Time.deltaTime);

                if (rb.velocity.magnitude > 0.25f) {
                    stampRotation = Angle(vel);
                    compute.SetFloat("rotation", Mathf.Deg2Rad * stampRotation);
                    compute.Dispatch(kernel, mapWidth / 8, mapHeight / 8, 1);
                }
                //Debug.Log(hit.textureCoord * mapWidth);
            }
        }
    }

    public void OnTriggerEnter ( Collider other ) {
        if (other.tag == "HammyBall") {
            hammyOnField = true;
            hammyBall = other.gameObject;
            hammyBall.GetComponent<Rigidbody>().drag = 1.5f;
        }
    }

    public void OnTriggerExit ( Collider other ) {
        if (other.tag == "HammyBall") {
            hammyOnField = false;
            hammyBall.GetComponent<Rigidbody>().drag = 0;
        }
    }

    float Angle(Vector2 vec) {
        if (vec.x < 0) {
            return 360 - ( Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg * -1 );
        } else {
            return Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
        }
    }
}
