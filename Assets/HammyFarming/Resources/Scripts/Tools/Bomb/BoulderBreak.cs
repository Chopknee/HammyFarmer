using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBreak: IDestructable {

    public GameObject destructionPrefab;

    public override void Break () {

        Instantiate(destructionPrefab, transform.position, Quaternion.identity, null);

        Destroy(gameObject);

    }
}
