using System.Collections.Generic;
using UnityEngine;

public class RandomGradient : MonoBehaviour
{
    [SerializeReference] TriggerChannelSO _setLevel;
    [SerializeReference] MeshRenderer _mesh;
    [SerializeField] List<Material> _gradients;

    private void OnEnable() => _setLevel.AddListener(SetGradient);

    private void OnDisable() => _setLevel.RemoveListener(SetGradient);

    void SetGradient()
    {
        Material sel = _gradients[Random.Range(0, _gradients.Count)];
        _mesh.material = sel;
    }
}
