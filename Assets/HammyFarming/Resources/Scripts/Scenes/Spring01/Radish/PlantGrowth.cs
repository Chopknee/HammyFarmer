﻿using HammyFarming.Brian.Utils.Timing;
using UnityEngine;

namespace HammyFarming.Scenes.Spring01.Radish {

    public class PlantGrowth: HammyFarming.Farm.Plants.PlantGrowth {

        public Vector3 startScale = Vector3.one;
        public Vector3 endScale = Vector3.one * 4;

        public override void Awake() {
            base.Awake();
        }

        public override void OnGrowing () {
            base.OnGrowing();
            transform.localScale = Vector3.Lerp(startScale, endScale, growPercent);
        }

        public override void OnGrowthStateChanged(bool state) {
            base.OnGrowthStateChanged(state);
        }

        public override void OnCollected () {
            base.OnCollected();
        }

        public override void OnValidate () {
            base.OnValidate();

            startScale = Vector3.Max(startScale, Vector3.one * 0.001f);
            endScale = Vector3.Max(startScale * 1.001f, endScale);
        }

    }
}
