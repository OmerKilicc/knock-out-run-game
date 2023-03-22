using Euphrates;
using UnityEngine;

public class HitForceManager : MonoBehaviour
{
    [SerializeReference] FloatSO _strength;
    [SerializeReference] FloatSO _force;
    [SerializeReference] FloatSO _maxForce;
    [SerializeReference] FloatSO _forceAdd;
    [SerializeReference] EnemyChannelSO _hit;
    [SerializeReference] TriggerChannelSO _secondPhase;
    [SerializeReference] TriggerChannelSO _fail;
    [SerializeReference] TriggerChannelSO _end;

    void OnEnable()
    {
        _hit.AddListener(OnHit);
        _secondPhase.AddListener(StopTimer);
        _fail.AddListener(StopTimer);
        _end.AddListener(StopTimer);
        _strength.OnChange += StrChange;
    }

    void OnDisable()
    {
        _hit.RemoveListener(OnHit);
        _secondPhase.RemoveListener(StopTimer);
        _fail.RemoveListener(StopTimer);
        _end.RemoveListener(StopTimer);
        _strength.OnChange -= StrChange;
    }

    private void Start()
    {
        CreateReduceTimer();
        _force.Value = 0f;
    }

    void StrChange(float change)
    {
        if (change > 0)
            return;

        _force.Value = Mathf.Clamp(_force + change, 0f, _maxForce);
    }

    void CreateReduceTimer() => GameTimer.CreateTimer("Force Reduce", 3600, CreateReduceTimer, ReduceTick);

    void ReduceTick(TickInfo tick)
    {
        _force.Value -= tick.DeltaTime * 3f;
        _force.Value = Mathf.Clamp(_force, 0f, _maxForce);
    }

    void OnHit(Enemy enemy)
    {
        _force.Value += _forceAdd;
    }

    void StopTimer() => GameTimer.CancleTimer("Force Reduce");
}
