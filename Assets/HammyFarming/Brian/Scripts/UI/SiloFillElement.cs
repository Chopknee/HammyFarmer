using HammyFarming.Brian;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiloFillElement : MonoBehaviour {

    public Image FillImage;

    // Start is called before the first frame update
    void Start() {
        if (Director.Instance != null) {
            Director.Instance.OnSiloFillChanged += SiloFillChanged;
        }
    }

    void SiloFillChanged(float value) {
        FillImage.fillAmount = value / Director.Instance.SiloFillGoal;
    }
}
