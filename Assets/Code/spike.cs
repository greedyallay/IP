using UnityEngine;

public class Spike : MonoBehaviour {
    public int count;
    void Start() {
        for (int i = 0; i < count; i++) {
            Destroy(Instantiate(transform).GetComponent<Spike>());
            transform.position += transform.right * 1;
        }
    }

}
