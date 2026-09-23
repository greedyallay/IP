using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
    public string nextLevel;
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            SceneManager.LoadSceneAsync(nextLevel);
            print("yay good job little bro");
        }
    }
}
