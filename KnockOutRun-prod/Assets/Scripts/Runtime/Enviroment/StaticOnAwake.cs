using UnityEngine;

public class StaticOnAwake : MonoBehaviour
{
    private void Awake()
    {
        gameObject.isStatic = true;
    }
}
