using UnityEngine;
public class Spin : MonoBehaviour {
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.rotation *= Quaternion.Euler(0f, 0f, speed * Time.deltaTime);
    }
}
