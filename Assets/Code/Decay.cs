using UnityEngine;

public class Decay : MonoBehaviour {
    public float lifeTime;
    float timer;

    void Update() {
        timer += Time.deltaTime;
        if (timer > lifeTime) {
            Destroy(gameObject);
        }
    }
}
