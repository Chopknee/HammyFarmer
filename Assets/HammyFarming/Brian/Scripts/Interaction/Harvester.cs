using HammyFarming.Brian;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour {

    public float MaxFill;

    float _fill;
    public float fill {
        get {
            return _fill;
        }
        set {
            _fill = value;
            FillMesh.SetBlendShapeWeight(0, ( fill / MaxFill ) * 100f);
        }
    }


    public SkinnedMeshRenderer FillMesh;

    public string PlantTag;

    public void Harvest(RaddishGrowth plant) {
        float nf = fill + plant.growPercent * Director.Instance.FullGrowthScore;
        if (nf < MaxFill) {
            fill += plant.growPercent * Director.Instance.FullGrowthScore;
            Destroy(plant.gameObject);
        }
    }


    //private void OnTriggerEnter ( Collider other ) {
    //    if (Director.Instance != null) {
    //        //Looking for the daikons!!!
    //        if (other.CompareTag(PlantTag)) {
    //            Debug.Log("RADDDISSSH");
    //            if (other.GetComponent<RaddishGrowth>() != null) {
    //                Debug.Log("Collected");
    //                //This is a daikon, fill the thing!
    //                Destroy(other);
    //                fill += other.GetComponent<RaddishGrowth>().growPercent * Director.Instance.FullGrowthScore;

    //                FillMesh.SetBlendShapeWeight(0, ( fill / MaxFill ) * 100f);
    //            }
    //        }
    //    }
    //}
}
