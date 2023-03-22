using Euphrates;
using UnityEngine;
using UnityEngine.Events;

public class HitRegister : MonoBehaviour
{
    [SerializeReference] LayerMaskSO _observedLayers;
    public UnityEvent OnTrigger;

    void OnCollisionEnter(Collision collision)
    {
        int pow = (int)Mathf.Pow(2, collision.gameObject.layer);

        if ((_observedLayers.Value & pow) != pow)
            return;

        OnTrigger.Invoke();
    }
}
