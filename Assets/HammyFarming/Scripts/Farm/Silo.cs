using UnityEngine;

public class Silo : MonoBehaviour {

    public float fillPerSecond = 0.1f;
    HammyFarming.Tools.Harvester.Collector harvester;


    private void Update () {
        if (harvester != null) {
            if (harvester.fill > 0) {
                float amount = fillPerSecond * Time.deltaTime;
                if (harvester.fill < amount) {
                    amount = harvester.fill;
                }
                harvester.fill -= amount;
                HammyFarming.Brian.Director.Instance.SiloFillLevel += amount;
            }
        }
    }

    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Harvester")) {
            //Offload the stuff.
            harvester = other.GetComponent<HammyFarming.Tools.Harvester.Collector>();
        }
    }

    private void OnTriggerExit ( Collider other ) {
        if (other.CompareTag("Harvester")) {
            harvester = null;
        }
    }
}
