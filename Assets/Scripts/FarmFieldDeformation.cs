using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFieldDeformation : MonoBehaviour {

    public LayerMask fieldMask;
    public ComputeShader compute;
    public RenderTexture outputTexture;
    public Texture2D startMap;

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

        GetComponent<MeshRenderer>().material.SetTexture("Texture2D_5564C194", outputTexture);
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

    public void Deform ( GameObject deformer, Texture2D stampMap, float deltaTime, float stampScale, float weight, float deformWeight, float tillWeight, float waterWeight, bool additiveOnly, Vector3 vel ) {

        if (Physics.Raycast(deformer.transform.position + ( Vector3.up * 5 ), Vector3.down, out RaycastHit hit, 50, fieldMask)) {
            //Figure out the texture coords (relative position on the mesh) where the object is.
            Vector2 stampPosition = hit.textureCoord * mapWidth;
            //Get some pre-requisite information about the object.
            Rigidbody rb = deformer.GetComponent<Rigidbody>();

            Vector2 flatVelocity = new Vector2(vel.x, vel.z);

            //If the object is not really moving, then it should not deform.
            if (flatVelocity.magnitude > 0.25f) {

                float rot = Angle(flatVelocity);

                Vector2 stampSize = new Vector2(stampMap.width, stampMap.height);
                compute.SetVector("velocity", vel);
                compute.SetTexture(kernel, "StampMap", stampMap);
                compute.SetTexture(kernel, "Result", outputTexture);
                compute.SetVector("stampSize", stampSize);
                compute.SetMatrix("transformationMatrix", MakeTransformationMatrix(-stampPosition, stampSize / 2, rot, stampScale));
                compute.SetFloat("deltaTime", deltaTime);
                compute.SetFloat("weight", weight);
                compute.SetFloat("deformWeight", deformWeight);
                compute.SetFloat("tillWeight", tillWeight);
                compute.SetFloat("waterWeight", waterWeight);
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


}
