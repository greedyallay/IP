using UnityEngine;

public class Death : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            Player.Kill();
        }
    }
}
