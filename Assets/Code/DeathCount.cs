using TMPro;
using UnityEngine;

public class DeathCount : MonoBehaviour {
    int deathCount;
    TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update() {
        if (deathCount != Main.deathCount) {
            deathCount = Main.deathCount;

            text.text = deathCount + (deathCount == 1 ? " DEATH" : " DEATHS");
        }
    }
}
