using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshBaker: MonoBehaviour {

    SkinnedMeshRenderer meshRenderer;
    MeshCollider mc;

    void Start () {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        Mesh m = new Mesh();
        meshRenderer.BakeMesh(m);
        mc = gameObject.AddComponent<MeshCollider>();
        mc.sharedMesh = m;
    }

    void UpdateMesh() {
        gameObject.GetComponent<MeshCollider>().sharedMesh = null;
        Mesh m = new Mesh();
        meshRenderer.BakeMesh(m);
        gameObject.GetComponent<MeshCollider>().sharedMesh = m;
    }
}
