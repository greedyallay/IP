using UnityEngine;

public class turretTrigger : MonoBehaviour {
    public Transform target;

    Turret turret;

    void Start() {
        turret = target.GetComponent<Turret>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        turret.Fire();
    }
}
