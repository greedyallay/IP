using UnityEngine;

public class Pixelation : MonoBehaviour {
    public RenderTexture renderTexture;

    public int height = 180;

    void Start() {
        float aspect = (float)Screen.width / Screen.height;
        int width = Mathf.RoundToInt(height * aspect);

        renderTexture.Release();
        renderTexture.width = width;
        renderTexture.height = height;
        renderTexture.Create();
    }
}