using UnityEngine;

public class Camera : MonoBehaviour {
    public static Camera Instance;

    public float camRes = 2;

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

    // Smooth camera position.
    Vector3 position;

    float shakeIntensity;

    void Awake() {
        Instance = this;
        cam = GetComponent<UnityEngine.Camera>();
    }

    void Start() {
        if (target != null) {
            Transform possibletarget;
            possibletarget = GameObject.Find("player").transform;

            if (possibletarget != null)
                target = possibletarget;

            position = target.position + offset;
            transform.position = position;
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

        // Smooth position stays fractional.
        position = Vector3.SmoothDamp(
            position,
            targetPos,
            ref velocity,
            smoothTime
        );

        // Make a separate snapped position for rendering.
        Vector3 renderPosition = position;

        float pixelSize = 1f / camRes;

        renderPosition.x = Mathf.Round(renderPosition.x / pixelSize) * pixelSize;
        renderPosition.y = Mathf.Round(renderPosition.y / pixelSize) * pixelSize;

        transform.position = renderPosition;
    }

    public void shake(float intensity) {
        shakeIntensity = intensity;
    }
}
