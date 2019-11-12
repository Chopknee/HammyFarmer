using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Utils {

    public class InstantiateOnDestroy: MonoBehaviour {

        public List<GameObject> prefabsToSpawn;

        private void OnDestroy () {
            foreach (GameObject go in prefabsToSpawn) {
                Instantiate(go, transform.position, Quaternion.identity, null);
            }
        }
    }
}
