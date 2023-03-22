using Cinemachine;
using Euphrates;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SetVirtualCamFromSO : MonoBehaviour
{
    CinemachineVirtualCamera _vcam;

    [SerializeReference] TransformSO _follow;
    [SerializeReference] TransformSO _look;

    private void Awake() => _vcam = GetComponent<CinemachineVirtualCamera>();

    public void Set()
    {
        if (_follow != null)
            _vcam.Follow = _follow.Value;

        if (_look != null)
            _vcam.LookAt = _look.Value;
    }
}
