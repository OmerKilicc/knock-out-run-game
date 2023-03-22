using Euphrates;
using UnityEngine;

public class ProgressionUpdater : MonoBehaviour
{
    Transform _transform;

    [SerializeReference] FloatSO _levelLength;
    [SerializeReference] FloatSO _progression;

    [Header("Events"), Space]
    [SerializeReference] TriggerChannelSO _gameStart;
    [SerializeReference] TriggerChannelSO _gameEnd;
    [SerializeReference] TriggerChannelSO _gameFail;

    bool _update = false;

    private void OnEnable()
    {
        _transform = transform;

        _gameStart.AddListener(StartUpdating);
        _gameEnd.AddListener(StopUpdating);
        _gameFail.AddListener(StopUpdating);
    }

    private void OnDisable()
    {
        _gameStart.RemoveListener(StartUpdating);
        _gameEnd.RemoveListener(StopUpdating);
        _gameFail.RemoveListener(StopUpdating);
    }

    private void Update()
    {
        if (!_update)
            return;

        var cur = _transform.position.z;
        var prog = cur / _levelLength;

        prog = Mathf.Clamp01(prog);
        _progression.Value = prog;
    }

    void StartUpdating() => _update = true;
    void StopUpdating() => _update = false;
}
