using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInstance : MonoBehaviour {

    public static Dictionary<string, SingletonInstance> singletonInstances;

    private void Awake () {
        if (singletonInstances == null) {
            singletonInstances = new Dictionary<string, SingletonInstance>();
        }

        if (singletonInstances.ContainsKey(gameObject.name)) {
            Destroy(gameObject);//
        } else {
            singletonInstances.Add(gameObject.name, this);
            DontDestroyOnLoad(gameObject);
        }
    }
}
