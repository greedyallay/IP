using UnityEngine;

public class fuckOff : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("g")) {
            Destroy(gameObject);
        }
    }
}
