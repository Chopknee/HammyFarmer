using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleComputeExecutor: MonoBehaviour {

    public ComputeShader compute;

    public RenderTexture outputTexture;

    public Texture2D inputMap;
    public Texture2D stampMap;

    public Vector2 stampPosition;
    public float stampRotation;
    public int kernel;
    // Start is called before the first frame update
    void Start () {

        //kernel = compute.FindKernel("CSMain");

        //outputTexture = new RenderTexture(inputMap.width, inputMap.height, 24);
        //outputTexture.enableRandomWrite = true;
        //outputTexture.Create();

        //RenderTexture.active = outputTexture;
        //Graphics.Blit(inputMap, outputTexture);
        //RenderTexture.active = null;

        //compute.SetTexture(kernel, "StampMap", stampMap);
        //compute.SetVector("position", new Vector4(stampPosition.x, stampPosition.y, 0, 0));
        //compute.SetVector("stampSize", new Vector2(stampMap.width, stampMap.height));
        //compute.SetTexture(kernel, "Result", outputTexture);
        //compute.SetFloat("rotation", Mathf.Deg2Rad * stampRotation);
        //compute.Dispatch(kernel, inputMap.width / 8, inputMap.height / 8, 1);

        //GetComponent<MeshRenderer>().material.SetTexture("Texture2D_4C8AF7C7", outputTexture);
        
    }

    // Update is called once per frame

    private void Update () {

        //stampPosition.x = ( ( Mathf.Cos(( Time.time / 10f ) * ( Mathf.PI * 2 )) + 1 ) / 2 ) * 1024;
        //stampPosition.y = ( ( Mathf.Cos(( Time.time / 7.5f ) * ( Mathf.PI * 2 )) + 1 ) / 2 ) * 1024;
        //stampRotation = ( ( Mathf.Cos(( Time.time / 5f ) * ( Mathf.PI * 2 )) + 1 ) / 2 ) * Mathf.PI * 2;

        //compute.SetVector("position", new Vector4(stampPosition.x, stampPosition.y, 0, 0));
        //compute.SetFloat("rotation", stampRotation);

        //compute.Dispatch(kernel, inputMap.width / 8, inputMap.height / 8, 1);


    }
}
