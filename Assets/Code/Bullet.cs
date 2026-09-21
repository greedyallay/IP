using UnityEngine;

public class Move : MonoBehaviour {
    public float speed;

    void Update() {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnCollisionEnter() {
        //Destroy(gameObject);
    }

    void OnTriggerEnter2D() {
        //Destroy(gameObject);
    }
}