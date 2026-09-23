using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxSpeed = 10;
    public float floorAcceleration = 1;
    public float airAcceleration = 1;
    public float jumpHeight = 10;
    public float floorDamping = 1;
    public float airDamping = 1;

    bool firstDeathFrame = true;

    Rigidbody2D body;
    BoxCollider2D box;
    SpriteRenderer renderer;
    [HideInInspector] public static AudioSource audio;
    float deathTime;
    float airTime;
    bool hasJumped;


    public static Transform player;
    public static Player component;
    public static Vector2 spawn;
    public static bool dead;
    public float respawnTime;

    public AudioClip jumpSound;
    public AudioClip deathSound;

    public Transform deathEffect;

    public float coyoteWindow;
    //public Transform pointOfDeath;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (body == null) {
            SafeStart();
        }
    }

    void SafeStart() {
        body = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        box = transform.Find("collider").GetComponent<BoxCollider2D>();
        player = GameObject.Find("player").transform;
        component = player.GetComponent<Player>();
        spawn = transform.position;
    }


    // Update is called once per frame
    void Update() {
        if (dead) {
            deathTime += Time.deltaTime;
            body.simulated = false;

            if (deathTime > .2f) {
                renderer.color = new Color(0f, 0f, 1f, 0f);
            }

            if (firstDeathFrame) {
                firstDeathFrame = false;
                Instantiate(deathEffect).transform.position = transform.position;
            }


            if (deathTime > respawnTime) {
                dead = false;
                transform.position = spawn;
                body.simulated = true;
                body.linearVelocity = Vector2.zero;
                deathTime = 0;
                firstDeathFrame = true;
                renderer.color = new Color(0f, 0f, 1f, 1f);
            }
            return;
        }
        //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        int wall = againstWall();
        bool floor = onFloor();
        if (Input.GetKey("a") && wall != -1) { body.linearVelocityX -= (floor ? floorAcceleration : airAcceleration) * Time.deltaTime; }
        if (Input.GetKey("d") && wall != 1) { body.linearVelocityX += (floor ? floorAcceleration : airAcceleration) * Time.deltaTime; }
        if (Input.GetKeyDown("w")) {
            if (floor || airTime < coyoteWindow) {
                Jump();
            }

        }
        if (floor) {
            airTime = 0;
            hasJumped = false;
            body.linearVelocityX *= Mathf.Pow(floorDamping, Time.deltaTime);


        }
        else {
            body.linearVelocityX *= Mathf.Pow(airDamping, Time.deltaTime);
            airTime += Time.deltaTime;
        }
        body.freezeRotation = !floor;
        body.linearVelocityX = Mathf.Clamp(body.linearVelocityX, -maxSpeed, maxSpeed);


        //transform.rotation = Quaternion.Euler(0, 0, checkSlope());

    }

    bool onFloor() {
        float jumpRange = .02f;
        Vector2 origin = new Vector2(box.bounds.min.x, box.bounds.min.y - jumpRange);
        Vector2 dir = Vector2.right;
        Debug.DrawRay(origin, dir, new Color(255, 0f, 0f));
        return Physics2D.Raycast(origin, dir, box.size.x, 1 << LayerMask.NameToLayer("Stage"));
    }

    void Jump() {
        if (hasJumped) return;
        hasJumped = true;
        body.linearVelocityY = jumpHeight;
        audio.PlayOneShot(jumpSound);
    }

    int againstWall() {
        int shit = 0;
        float detectionRange = .02f;
        Vector2 leftPos = new Vector2(box.bounds.min.x - detectionRange, box.bounds.max.y);
        Vector2 rightPos = new Vector2(box.bounds.max.x + detectionRange, box.bounds.max.y);
        Vector2 dir = Vector2.down;
        Debug.DrawRay(rightPos, dir, new Color(255, 0f, 0f));
        Debug.DrawRay(leftPos, dir, new Color(255, 0f, 0f));
        if (Physics2D.Raycast(leftPos, dir, .02f, 1 << LayerMask.NameToLayer("Stage"))) {
            shit = -1;
        }
        else if (Physics2D.Raycast(rightPos, dir, .02f, 1 << LayerMask.NameToLayer("Stage"))) {
            shit = 1;

        }
        return shit;

    }

    float checkSlope() {
        return 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Vector2 p1 = Physics2D.Raycast(new Vector2(box.bounds.min.x, box.bounds.min.y), Vector2.down, 1f, 1 << LayerMask.NameToLayer("Stage")).point;
        Vector2 p2 = Physics2D.Raycast(new Vector2(box.bounds.max.x, box.bounds.min.y), Vector2.down, 1f, 1 << LayerMask.NameToLayer("Stage")).point;

        return Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;
    }

    public static void Kill() {
        print("you died");
        dead = true;
        audio.PlayOneShot(component.deathSound);
    }

    public static void CheckPoint(Vector2 pos = default) {
        print("kruispunt");
        spawn = pos; //player.transform.position
    }

}
