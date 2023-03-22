using UnityEngine;

public class RagdollEnabler : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent<Rigidbody>(out var rb))
                rb.isKinematic = true;

            if (child.TryGetComponent<Collider>(out var col))
                col.enabled = false;

            DisableChildRbs(child);
        }
    }

    public void Enable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent<Rigidbody>(out var rb))
                rb.isKinematic = false;

            if (child.TryGetComponent<Collider>(out var col))
                col.enabled = true;

            EnableChildRbs(child);
        }
    }

    public void EnableChildRbs(Transform tr)
    {
        for (int i = 0; i < tr.childCount; i++)
        {
            Transform child = tr.GetChild(i);

            if (child.TryGetComponent<Rigidbody>(out var rb))
                rb.isKinematic = false;

            if (child.TryGetComponent<Collider>(out var col))
                col.enabled = true;

            EnableChildRbs(child);
        }
    }

    public void DisableChildRbs(Transform tr)
    {
        for (int i = 0; i < tr.childCount; i++)
        {
            Transform child = tr.GetChild(i);

            if (child.TryGetComponent<Rigidbody>(out var rb))
                rb.isKinematic = true;

            if (child.TryGetComponent<Collider>(out var col))
                col.enabled = false;

            DisableChildRbs(child);
        }
    }
}

