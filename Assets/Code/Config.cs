using UnityEngine;

public class Config : MonoBehaviour {
    public Color setTheme;

    public static Color theme { get; set; }

    void Awake() {
        theme = setTheme;
    }
}
