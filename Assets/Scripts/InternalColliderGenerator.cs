using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalColliderGenerator : MonoBehaviour {

    public float uniformScale = 1;
    public int colliderLayer;
    public int spherePoints = 10;
    public float colliderScale = 0.15f;
    public Mesh colliderMesh;
    // Start is called before the first frame update
    void Start() {
        float scl = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
        transform.localScale = Vector3.one * scl;//If the scale components are equal then this will be the same.
        //Generate all the points around the sphere that are used for this.
        //The layer should not interfere with anything else in the level.
        foreach (Vector3 point in fibonacci_sphere(spherePoints, scl * uniformScale)) {
            GameObject go = new GameObject("Point");
            go.transform.position = point + transform.position;
            MeshCollider mc = go.AddComponent<MeshCollider>();
            mc.sharedMesh = colliderMesh;
            mc.convex = true;
            go.transform.localScale = Vector3.one * colliderScale;
            go.transform.SetParent(transform);
            go.transform.LookAt(transform.position);
            go.layer = colliderLayer;

            
        }
    }

    // Update is called once per frame
    void Update() {

    }

    //Generates a set of points evenly distributed around a sphere
    public Vector3[] fibonacci_sphere(int sampleCount, float radius) {
        Vector3[] points = new Vector3[sampleCount];
        float offset = 2f / sampleCount;
        float increment = Mathf.PI * (3f - Mathf.Sqrt(5f));
        for (int i = 0; i < sampleCount; i++) {
            float y = ((i * offset) - 1) + (offset / 2f);
            float r = Mathf.Sqrt(1 - Mathf.Pow(y, 2));
            float phi = ((i + 1) % sampleCount) * increment;
            float x = Mathf.Cos(phi) * r;
            float z = Mathf.Sin(phi) * r;
            points[i] = new Vector3(x, y, z) * uniformScale;
        }
        return points;
    }
}
