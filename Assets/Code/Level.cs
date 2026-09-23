using UnityEngine;

public class Level : MonoBehaviour {
    public Transform outlineBP;
    public float outlineScale = 1;
    void Start() {
        Transform outline = Instantiate(outlineBP, transform, false);
        SpriteRenderer render = outline.GetComponent<SpriteRenderer>();
        outline.transform.localScale = Vector2.one + new Vector2(1 / transform.localScale.x * outlineScale, 1 / transform.localScale.y * outlineScale);
        outline.transform.localPosition = Vector2.zero;
        render.sortingOrder = 10;
        return;
        GetComponent<SpriteRenderer>().color = Config.theme;
    }
}
