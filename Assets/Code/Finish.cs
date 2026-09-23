using UnityEngine;

public class Finish : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            print("yay good job little bro");
        }
    }
}
