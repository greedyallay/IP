using UnityEngine;

public class Camera : MonoBehaviour {
    public static Camera Instance;

    int camRes = 2;

    public Transform target;

    public Vector3 offset = new Vector3(0f, 0f, -1f);
    public float smoothTime = 0.25f;
    public bool doFollow = true;

    public bool allowZoom = true;
    public float zoom = 1f;
    public float baseOrthographicSize = 5f;
    public float minZoom = 0.5f;
    public float maxZoom = 3f;

    UnityEngine.Camera cam;
    Vector3 velocity = Vector3.zero;

    float shakeIntensity;

    void Awake() {
        Instance = this;
        cam = GetComponent<UnityEngine.Camera>();
    }

    void Start() {
        if (target != null) {
            Transform possibletarget;
            possibletarget = GameObject.Find("player").transform;
            if (possibletarget != null) target = possibletarget;
            transform.position = target.position + offset;
        }

        cam.orthographicSize = baseOrthographicSize * zoom;
    }

    void Update() {
        if (target == null) return;

        float scroll = Input.mouseScrollDelta.y;

        if (allowZoom) {
            if (scroll > 0f) {
                zoom /= 1.1f;
            }

            if (scroll < 0f) {
                zoom *= 1.1f;
            }

            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

            cam.orthographicSize = baseOrthographicSize * zoom;
        }

        if (shakeIntensity > 0f) {
            Vector3 shake = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0f
            );

            transform.position += shake;

            shakeIntensity -= Time.deltaTime;

            if (shakeIntensity < 0f) {
                shakeIntensity = 0f;
            }
        }
    }

    void LateUpdate() {
        if (target == null) return;
        if (!doFollow) return;
        if (Player.dead) return;

        Vector3 targetPos = target.position + offset;

        float pixelSize = 1f / camRes;

        targetPos.x = Mathf.Round(targetPos.x / pixelSize) * pixelSize;
        targetPos.y = Mathf.Round(targetPos.y / pixelSize) * pixelSize;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );
    }

    public void shake(float intensity) {
        shakeIntensity = intensity;
    }
}