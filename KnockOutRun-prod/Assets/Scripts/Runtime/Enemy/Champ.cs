using Euphrates;
using UnityEngine;

public class Champ : MonoBehaviour
{
    [SerializeReference] FloatSO _strength;
    [SerializeReference] FloatSO _force;
    [SerializeReference] FloatSO _maxVelocity;

    public void Throw()
    {
        float vel = Mathf.Clamp((_strength * .5f) + (_force * .5f), 0f, _maxVelocity);
        SetChildRbVelocity(transform, new Vector3(0f, 20f, vel));
        SoundManager.Play("hit", 0, true, 1f);
    }

    public void Stop() => SetChildRbVelocity(transform, Vector3.up * 10f);

    void SetChildRbVelocity(Transform parent, Vector3 velocity)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            SetChildRbVelocity(child, velocity);

            if (!child.TryGetComponent<Rigidbody>(out var rb))
                continue;

            rb.velocity = velocity;
        }
    }
}
