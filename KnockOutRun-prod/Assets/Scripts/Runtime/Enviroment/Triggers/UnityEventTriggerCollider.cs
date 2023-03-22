using Euphrates;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTriggerCollider : MonoBehaviour
{
	public UnityEvent<GameObject> OnEnter;
	public UnityEvent<GameObject> OnExit;

	public LayerMaskSO _observedLayers;

    private void OnTriggerEnter(Collider other)
    {
        int pow = (int)Mathf.Pow(2, other.gameObject.layer);

        if ((_observedLayers.Value & pow) != pow)
            return;

        OnEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        int pow = (int)Mathf.Pow(2, other.gameObject.layer);

        if ((_observedLayers.Value & pow) != pow)
            return;

        OnExit?.Invoke(other.gameObject);
    }
}
