using UnityEngine;
using System.Reflection;
public class Trigger : MonoBehaviour {
    public Transform target;

    Turret code;

    void Start() {
        code = target.GetComponent<Turret>();
    }



    void OnTriggerEnter2D(Collider2D collision) {
        Activate(collision.transform);
        print("carpt");
    }

    void Activate(Transform target) {
        MonoBehaviour[] components = target.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour component in components) {
            if (component == null) continue;
            print("ja tog");
            print(component);

            MethodInfo method = component.GetType().GetMethod("Trigger",
                BindingFlags.Public | BindingFlags.Instance,
                null,
                System.Type.EmptyTypes,
                null);

            if (method == null) continue;
            if (method.ReturnType != typeof(void)) continue;

            method.Invoke(component, null);
        }
    }
}
