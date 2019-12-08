using UnityEngine;


namespace HammyFarming.Tools.Harvester {
    public class Collector: MonoBehaviour {

        public float MaxFill;

        public delegate void FillChanged( float level );
        public FillChanged OnFillChanged;

        float _fill;
        public float fill {
            get { return _fill; }
            set { _fill = value; OnFillChanged?.Invoke(_fill); }
        }

        public bool Harvest ( HammyFarming.Farm.Plants.PlantGrowth plant ) {
            float nf = fill + plant.growPercent * HammyFarming.Brian.Director.Instance.FullGrowthScore;
            if (nf < MaxFill) {
                fill += plant.growPercent * HammyFarming.Brian.Director.Instance.FullGrowthScore;
                Destroy(plant.gameObject);
                return true;
            }
            return false;
        }
    }
}
