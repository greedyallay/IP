using UnityEngine;

public class Main : MonoBehaviour {
    public static Config Config { get; set; }
    void Start() {
        GameObject configObj = GameObject.Find("Config");
        if (configObj != null) Config = configObj.GetComponent<Config>(); //not my problem if you just so hapen to have an object with the Name Config lmao
    }
}
