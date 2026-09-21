using UnityEngine;

public class Turret : MonoBehaviour {
    public Transform bullet;

    Transform barrel;
    Transform gun;

    AudioSource audio;

    public float cooldown = 1f;
    float timer = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (gun == null) SafeStart();
    }

    void SafeStart() {
        gun = transform.Find("gun");
        barrel = gun.Find("barrel");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (timer == -1f) {
            Vector2 dir = gun.right;
            Debug.DrawRay(gun.transform.position, dir);
            RaycastHit2D hit = Physics2D.Raycast(gun.transform.position, dir, 100, 1 << LayerMask.NameToLayer("Player"));
            if (hit) {
                Transform bulletObj = Instantiate(bullet);
                bulletObj.transform.rotation = gun.transform.rotation;
                bulletObj.transform.position = gun.transform.position;
                timer = 0;
                audio.Play();
            }
        }
        else {
            timer += Time.deltaTime;
            if (timer > cooldown) {
                timer = -1f;
            }
        }


    }

    void Fire() {

    }


}
