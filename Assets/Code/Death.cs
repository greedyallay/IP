using UnityEngine;

public class Death : MonoBehaviour {

    int res = 10;
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            Player.Kill();
        }
    }

    void fuckoff() {

        int x = (int)Mathf.Round((int)transform.position.x / res) * res;
        int y = (int)Mathf.Round((int)transform.position.y / res) * res;

        transform.position = new Vector2(x, y);
    }
}
