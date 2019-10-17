using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFieldDeformation : MonoBehaviour {

    public LayerMask fieldMask;
    public ComputeShader compute;
    RenderTexture outputTexture;
    public Texture2D startMap;

    int kernel;
    int mapWidth = 0; int mapHeight = 0;

    int baseMapResolution = 2048;//This is the size I am basing all the scales on
    float mapScalar = 0;
    void Start() {
        mapWidth = startMap.width;
        mapHeight = startMap.height;
        mapScalar = (float) mapWidth / (float)baseMapResolution;
        kernel = compute.FindKernel("CSMain");

        outputTexture = new RenderTexture(mapWidth, mapHeight, 24);
        outputTexture.enableRandomWrite = true;
        outputTexture.filterMode = FilterMode.Bilinear;
        outputTexture.Create();

        RenderTexture.active = outputTexture;
        Graphics.Blit(startMap, outputTexture);
        RenderTexture.active = null;

        GetComponent<MeshRenderer>().material.SetTexture("Texture2D_5564C194", outputTexture);

        //tex = new Texture2D(1, 1, TextureFormat.RGB24, false, false);
        fieldMap = new Texture2D(mapWidth, mapHeight, TextureFormat.RGB24, false, false);
        StartCoroutine("DecodeScreen");
        chunks = mapWidth / chunkSize;
    }

    //I want to build the transformation matrix here for the stamp map.
    Matrix4x4 MakeTransformationMatrix(Vector2 position, Vector2 offset, float rotation, float scale) {
        //Vector3 vec = position + offset;
        Matrix4x4 pos = Matrix4x4.identity;
        
        pos *= Matrix4x4.TRS(offset, Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * rotation)), Vector3.one);
        pos *= Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1 / scale, 1 / scale, 1 / scale));
        pos *= Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);
        return pos;
    }

    public void Deform ( GameObject deformer, Texture2D stampMap, float deltaTime, float stampScale, float weight, Vector3 weights, bool additiveOnly, Vector3 vel ) {

        if (Physics.Raycast(deformer.transform.position + ( Vector3.up * 5 ), Vector3.down, out RaycastHit hit, 50, fieldMask)) {
            //Figure out the texture coords (relative position on the mesh) where the object is.
            Vector2 stampPosition = hit.textureCoord * mapWidth;

            Vector2 flatVelocity = new Vector2(vel.x, vel.z);

            //If the object is not really moving, then it should not deform.
            if (vel.sqrMagnitude > 0.25f) {

                float rot = Angle(flatVelocity);
                Vector3 transformedWeights = weights * vel.magnitude * weight;
                Vector2 stampSize = new Vector2(stampMap.width * mapScalar, stampMap.height * mapScalar);
                compute.SetTexture(kernel, "StampMap", stampMap);
                compute.SetTexture(kernel, "Result", outputTexture);
                compute.SetVector("stampSize", stampSize);
                compute.SetMatrix("transformationMatrix", MakeTransformationMatrix(-stampPosition, stampSize / 2, rot, stampScale));
                compute.SetFloat("deltaTime", deltaTime);
                compute.SetVector("weights", transformedWeights);
                compute.SetBool("additiveOnly", additiveOnly);
                compute.Dispatch(kernel, mapWidth / 8, mapHeight / 8, 1);
            }
        }
    }

    float Angle(Vector2 vec) {
        if (vec.x < 0) {
            return (Mathf.PI * 2f) - ( Mathf.Atan2(vec.x, vec.y) );
        } else {
            return -Mathf.Atan2(vec.x, vec.y);
        }
    }

    int chunkSize = 128;
    int x = 0;
    int y = 0;
    int chunks = 0;

    IEnumerator DecodeScreen () {
        while (true) {
            yield return new WaitForEndOfFrame();
            //Figure out where we are at in this thing.
            int posX = x * chunkSize;
            int posY = y * chunkSize;
            RenderTexture.active = outputTexture;
            fieldMap.ReadPixels(new Rect(posX, posY, chunkSize, chunkSize), posX, posY);
            //Apply isn't needed because I am using the field map to sample the texture in main cpu accessible memory
            //Apply essentially sends the texture data to the gpu, so it's pointless since the whole idea of this is to
            //  sample the output texture from the gpu.
            RenderTexture.active = null;
            x++;
            if (x >= chunks) {
                x = 0;
                y++;
                if (y >= chunks) {
                    y = 0;
                }
            }
        }
    }

    Texture2D fieldMap;

    public Color GetFieldValuesAt ( Vector2 texCoords ) {
        return fieldMap.GetPixel((int) ( texCoords.x * mapWidth ), (int) ( ( 1 - texCoords.y ) * mapHeight ));
    }

}
