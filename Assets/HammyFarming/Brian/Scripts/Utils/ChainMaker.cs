using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChainMaker: MonoBehaviour {
    [Header("Collider Settings")]
    public Vector3 capsuleColliderCenter = new Vector3(0, 0, 0.3775f);
    public float capsuleColliderRadius = 0.255f;
    public float capsuleColliderHeight = 0.70f;
    public int capsuleColliderDirection = 2;
    public PhysicMaterial linkPhysicsMaterial;


    [Header("Joint Settings")]

    public Vector3 jointAxis = new Vector3(0, 1, 0);

    public float motionSpring = 100;
    public float motionDamper = 1;

    public float twistSpring = 100;
    public float twistDamper = 1;

    public float swingSpring = 10;
    public float swingDamper = 1;

    [Header("Rigidbody Settings")]
    public float linkMass = 1;
    [Header("Danger Zone (Actually modifies gameobjects!)")]
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
        cc.material = linkPhysicsMaterial;
    }

    void AddRigidBody(GameObject go) {
        if (go.GetComponent<ConfigurableJoint>() != null) { DestroyImmediate(go.GetComponent<ConfigurableJoint>()); }
        if (go.GetComponent<Rigidbody>() != null) { DestroyImmediate(go.GetComponent<Rigidbody>()); }
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.mass = linkMass;
    }

    void AddJoint(GameObject go, Rigidbody connection) {
        ConfigurableJoint j = go.AddComponent<ConfigurableJoint>();

        j.axis = jointAxis;

        j.xMotion = ConfigurableJointMotion.Free;
        j.yMotion = ConfigurableJointMotion.Free;
        j.zMotion = ConfigurableJointMotion.Free;

        //The motion constraint setting
        JointDrive motionDrive = new JointDrive();
        motionDrive.positionSpring = motionSpring;
        motionDrive.positionDamper = motionDamper;
        motionDrive.maximumForce = float.MaxValue;
        j.xDrive = motionDrive;
        j.yDrive = motionDrive;
        j.zDrive = motionDrive;

        j.connectedBody = connection;

        j.angularXMotion = ConfigurableJointMotion.Free;
        j.angularYMotion = ConfigurableJointMotion.Free;
        j.angularZMotion = ConfigurableJointMotion.Free;

        JointDrive twistDrive = new JointDrive();
        twistDrive.positionSpring = twistSpring;
        twistDrive.positionDamper = twistDamper;
        twistDrive.maximumForce = float.MaxValue;
        j.angularXDrive = twistDrive;

        JointDrive swingDrive = new JointDrive();
        swingDrive.positionSpring = swingSpring;
        swingDrive.positionDamper = swingDamper;
        swingDrive.maximumForce = float.MaxValue;
        j.angularYZDrive = swingDrive;

        j.enableCollision = true;
    }
}
