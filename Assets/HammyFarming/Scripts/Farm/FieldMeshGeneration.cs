
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Farm {
    public class FieldMeshGeneration {

        public static Mesh Generate(int width, int height, bool useFlatShading) {
            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            int[,] vertexIndicesMap = new int[width, height];
            int meshVertexIndex = 0;

            Vector3[] vertices = new Vector3[width * height];
            List<int> triangles = new List<int>();
            Vector2[] uvs = new Vector2[vertices.Length];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    vertexIndicesMap[x, y] = meshVertexIndex;
                    meshVertexIndex++;
                }
            }

			float aW = width - 1;
			float aH = height - 1;

            int i = 0;
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {

                    int vertIndex = vertexIndicesMap[x, y];
                    Vector2 percent = new Vector2((float) x / (float)aW, (float) y / (float) aH);
                    Vector3 vertexPosition = new Vector3(percent.x, percent.y);
                    vertices[i] = new Vector3(percent.x, 0, percent.y);
                    uvs[i] = new Vector2(percent.x, percent.y);
                    //vertices.Add(new Vector3(percent.x, 0, percent.y));

                    if (x < width-1 && y < height - 1) {
                        int a = vertexIndicesMap[x, y];
                        int b = vertexIndicesMap[x + 1, y];
                        int c = vertexIndicesMap[x, y + 1];
                        int d = vertexIndicesMap[x + 1, y + 1];
                        triangles.Add(c);triangles.Add(d);triangles.Add(a);
                        //triangles.Add(a);triangles.Add(d);triangles.Add(c);
                        triangles.Add(b);triangles.Add(a);triangles.Add(d);
                        //triangles.Add(d); triangles.Add(a); triangles.Add(b);
                    }
                    i++;
                }
            }

            //mesh.vertices = vertices;
            mesh.vertices = vertices;
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs;

            //The last thing is calculating the normals??
            mesh.RecalculateNormals();

            return mesh;
        }

    }
}