using Euphrates;
using UnityEngine;

public class TotalPoints : MonoBehaviour
{
    [SerializeReference] FloatSO _strength;
    [SerializeReference] FloatSO _multiplier;
    [SerializeReference] BuildUpToVal _buildUp;

    public void SetText() => _buildUp.Play(_strength * _multiplier);
}
