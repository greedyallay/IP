using UnityEngine;

public class turretTrigger : MonoBehaviour {
    public Transform target;

    MonoBehaviour code;

    void Start() {
        code = target.GetComponent<Turret>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        code.Trigger();
    }
}
