using HammyFarming.Brian.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ImageCycle: MonoBehaviour {

    public float framesPerSecond;
    public bool playOnAwake;

    public Sprite[] cycleImages;
    int imageIndex = 0;

    Timeout frameTimeout;

    Image img;

    void Awake () {
        frameTimeout = new Timeout(1f / framesPerSecond, playOnAwake);
        img = GetComponent<Image>();
        if (img == null) {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {
        if (frameTimeout.Tick(Time.deltaTime)) {
            imageIndex++;
            if (imageIndex >= cycleImages.Length) {
                imageIndex = 0;
            }
            img.sprite = cycleImages[imageIndex];
            frameTimeout.ReStart();
        }
    }
}
