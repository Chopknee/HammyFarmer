using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChainMaker: MonoBehaviour {

    public Vector3 capsuleColliderCenter = new Vector3(0, 0, 0.3775f);
    public float capsuleColliderRadius = 0.255f;
    public float capsuleColliderHeight = 0.70f;
    public int capsuleColliderDirection = 2;
    public bool generateChainLinks = false;

    int ct = 0;

    void Start () {
        //Generate the chain
        //RecursiveAddition(transform.GetChild(0).gameObject, true);
    }

    private void Update () {
        if (generateChainLinks) {
            ct = 0;
            generateChainLinks = false;
            RecursiveAddition(gameObject, true);
        }
    }

    void RecursiveAddition(GameObject go, bool addLink) {
        AddRigidBody(go);
        AddCollider(go);
        if (addLink) {
            AddJoint(go, go.transform.parent.GetComponent<Rigidbody>());//Link to the previous object
        }

        ct++;
        if (ct > 50) { return; }//Hard limiter to prevent infinite recursion errors

        if (go.transform.childCount > 0) {
            RecursiveAddition(go.transform.GetChild(0).gameObject, true);
        }
    }

    void AddCollider(GameObject go) {
        if (go.GetComponent<CapsuleCollider>() != null) { DestroyImmediate(go.GetComponent<CapsuleCollider>()); }
        CapsuleCollider cc = go.AddComponent<CapsuleCollider>();
        cc.center = capsuleColliderCenter;
        cc.radius = capsuleColliderRadius;
        cc.height = capsuleColliderHeight;
        cc.direction = capsuleColliderDirection;
    }

    void AddRigidBody(GameObject go) {
        if (go.GetComponent<ConfigurableJoint>() != null) { DestroyImmediate(go.GetComponent<ConfigurableJoint>()); }
        if (go.GetComponent<Rigidbody>() != null) { DestroyImmediate(go.GetComponent<Rigidbody>()); }
        Rigidbody rb = go.AddComponent<Rigidbody>();
    }

    void AddJoint(GameObject go, Rigidbody connection) {
        ConfigurableJoint j = go.AddComponent<ConfigurableJoint>();
        j.xMotion = ConfigurableJointMotion.Locked;
        j.yMotion = ConfigurableJointMotion.Locked;
        j.zMotion = ConfigurableJointMotion.Locked;
        j.connectedBody = connection;
        JointDrive jd = j.angularXDrive;
        jd.positionSpring = 6.17f;
        jd.positionDamper = 7.09f;
        j.angularXDrive = jd;
        j.enableCollision = true;
        j.angularYMotion = ConfigurableJointMotion.Locked;
        //SoftJointLimit sjl = j.angularYLimit;
        //sjl.limit = 3.37174f;
        //j.angularYLimit = sjl;
    }
}
