using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HammyFarming.Brian.Animation {

    public class TagHover: MonoBehaviour {

        public float heightOffset = 1;

        public Transform[] hoverTags;

        // Update is called once per frame
        void Update () {
            foreach (Transform ta in hoverTags) {
                ta.position = transform.position + Vector3.up * heightOffset;
            }
        }
    }
}
