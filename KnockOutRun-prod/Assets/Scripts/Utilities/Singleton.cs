using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this as T;
    }
}