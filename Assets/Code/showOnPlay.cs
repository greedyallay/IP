using UnityEngine;

public class showOnPlay : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
