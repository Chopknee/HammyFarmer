using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Farm {

    public class FarmFieldDeformation: MonoBehaviour {

        public LayerMask fieldMask;
        public ComputeShader compute;
        RenderTexture outputTexture;
        public Texture2D startMap;
        public Texture2D borderMap;

        public int meshResolution = 100;

        private float halfsize;
        
		private int inputTextureWidth = 0;
		private int inputTextureHeight = 0;

        int kernelIndex;

        private MeshFilter mf;

		private GameObject meshGameObject;

		int chunkSize = 128;
        int x = 0;
        int y = 0;
        int chunksRows = 0;
		int chunksColumns = 0;

        void Awake () {

			meshGameObject = transform.Find("Mesh").gameObject;
            mf = meshGameObject.GetComponent<MeshFilter>();
            mf.sharedMesh = HammyFarming.Farm.FieldMeshGeneration.Generate(meshResolution, meshResolution, false);

			meshGameObject.GetComponent<MeshCollider>().sharedMesh = mf.sharedMesh;

            halfsize = meshResolution / 2.0f;

            kernelIndex = compute.FindKernel("CSMain");

			//The true scalar of the map!
			inputTextureWidth = startMap.width;
			inputTextureHeight = startMap.height;

            outputTexture = new RenderTexture(inputTextureWidth, inputTextureHeight, 24);
            outputTexture.enableRandomWrite = true;
            outputTexture.filterMode = FilterMode.Bilinear;
            outputTexture.Create();

            RenderTexture.active = outputTexture;
            Graphics.Blit(startMap, outputTexture);
            RenderTexture.active = null;

			MeshRenderer renderer = transform.Find("Mesh").GetComponent<MeshRenderer>();
            renderer.material.SetTexture("_DeformTex", outputTexture);
			renderer.material.SetInt("_Resolution", meshResolution);

            //tex = new Texture2D(1, 1, TextureFormat.RGB24, false, false);
            fieldMap = new Texture2D(inputTextureWidth, inputTextureHeight, TextureFormat.RGB24, false, false);
            StartCoroutine("DecodeScreen");
            chunksRows = inputTextureWidth / chunkSize;
			chunksColumns = inputTextureHeight / chunkSize;
        }

        //I want to build the transformation matrix here for the stamp map.
        Matrix4x4 MakeTransformationMatrix ( Vector2 position, Vector2 offset, float rotation, Vector2 scale ) {
            //Vector3 vec = position + offset;
            Matrix4x4 pos = Matrix4x4.identity;

            pos *= Matrix4x4.TRS(offset, Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * rotation)), new Vector3(1.0f / scale.x, 1.0f / scale.y, 1.0f));
            // pos *= Matrix4x4.TRS(Vector3.zero, Quaternion.identity, );
            pos *= Matrix4x4.TRS(position, Quaternion.identity, Vector3.one);
            return pos;
        }

		float Angle ( Vector2 vec ) {
            if (vec.x < 0) {
                return ( Mathf.PI * 2f ) - ( Mathf.Atan2(vec.x, vec.y) );
            } else {
                return -Mathf.Atan2(vec.x, vec.y);
            }
        }

        public void Deform ( GameObject deformer, Texture2D stampMap, float deltaTime, float stampScale, float weight, Vector3 weights, bool additiveOnly, Vector3 vel ) {

            if (Physics.Raycast(deformer.transform.position + ( Vector3.up * 5 ), Vector3.down, out RaycastHit hit, 50, fieldMask)) {
                //Getting the texture-space position of the hit point.
				
                Vector2 flatVelocity = new Vector2(vel.x, vel.z);//Getting the horizontal velocity of the obejct in question?
                
                if (vel.sqrMagnitude > 0.25f) {//Preventing deformations on slow moving or still objects.
					//Debug.Log(stampPosition);
                    float rot = Angle(flatVelocity);
                    Vector3 transformedWeights = weights * vel.magnitude * weight;
					compute.SetVector("weights", transformedWeights);

                    Vector2 stampSize = new Vector2(stampMap.width, stampMap.height);
					compute.SetVector("stampSize", stampSize);

					Vector3 uniformScale = new Vector3(
						((float)outputTexture.width / stampMap.width) / transform.localScale.x,
						((float)outputTexture.height / stampMap.height) / transform.localScale.z, 
						1.0f) * stampScale;

					Vector2 pos = new Vector2(hit.textureCoord.x * inputTextureWidth, hit.textureCoord.y * inputTextureHeight);
					Matrix4x4 mat = MakeTransformationMatrix(
						pos,
						-new Vector2(stampMap.width, stampMap.height) * 0.5f,
						-rot,
						uniformScale
					);
					compute.SetMatrix("transformationMatrix", mat);

                    compute.SetTexture(kernelIndex, "StampMap", stampMap);
                    compute.SetTexture(kernelIndex, "Result", outputTexture);
                    compute.SetFloat("deltaTime", deltaTime);
                    compute.SetBool("additiveOnly", additiveOnly);
                    compute.Dispatch(kernelIndex, inputTextureWidth / 8, inputTextureHeight / 8, 1);
                }
            }
        }

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
                if (x >= chunksRows) {
                    x = 0;
                    y++;
                    if (y >= chunksColumns) {
                        y = 0;
                    }
                }
            }
        }

        Texture2D fieldMap;

        public Color GetFieldValuesAt ( Vector2 texCoords ) {
            return fieldMap.GetPixel((int) ( texCoords.x * inputTextureWidth ), (int) ( ( 1 - texCoords.y ) * inputTextureHeight ));
        }

        public void OnDrawGizmos () {
            Gizmos.color = Color.cyan;
            Vector3 pos = transform.position;
            Gizmos.DrawWireCube(pos, transform.localScale);
        }
    }
}
