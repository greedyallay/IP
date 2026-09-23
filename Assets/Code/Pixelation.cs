using UnityEngine;

public class Pixelation : MonoBehaviour {
    public RenderTexture renderTexture;

    public int width = 320;
    public int height = 180;

    void Start() {
        renderTexture.width = width;
        renderTexture.height = height;

        renderTexture.Release();
        renderTexture.Create();
    }
}