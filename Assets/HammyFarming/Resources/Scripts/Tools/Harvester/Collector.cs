using UnityEngine;


namespace HammyFarming.Tools.Harvester {
    public class Collector: MonoBehaviour {

        public float MaxFill;

        float _fill;
        public float fill {
            get { return _fill; }
            set { _fill = value; }
        }

        public string PlantTag;

        public void Harvest ( HammyFarming.Farm.Plants.PlantGrowth plant ) {
            float nf = fill + plant.growPercent * HammyFarming.Brian.Director.Instance.FullGrowthScore;
            if (nf < MaxFill) {
                fill += plant.growPercent * HammyFarming.Brian.Director.Instance.FullGrowthScore;
                Destroy(plant.gameObject);
            }
        }

    }
}
