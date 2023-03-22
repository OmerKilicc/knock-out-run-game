using Euphrates;
using UnityEngine;

public class PlayerAnimController : CharacterAnimationControls
{
    [SerializeReference] FloatSO _strength;

    [Header("Animation Names"), Space]
    [SerializeField] string _knockedOut;
    [SerializeField] string _stumble;
    [SerializeField] string _finishPunch;

    [Header("Animation Names"), Space]
    [SerializeReference] EnemyChannelSO _punchTrigger;
    [SerializeReference] EnemyChannelSO _enemyHit;
    [SerializeReference] TriggerChannelSO _failTrigger;
    [SerializeReference] TriggerChannelSO _startTrigger;
    [SerializeReference] TriggerChannelSO _finishPunchTrigger;

    bool _started = false;

    private void OnEnable()
    {
        _startTrigger.AddListener(OnStart);
        _strength.OnChange += OnDamage;
        _punchTrigger.OnTrigger += PunchEnemy;
        _finishPunchTrigger.AddListener(Finisher);
        _failTrigger.AddListener(Fail);
    }

    private void OnDisable()
    {
        _startTrigger.RemoveListener(OnStart);
        _strength.OnChange -= OnDamage;
        _punchTrigger.OnTrigger -= PunchEnemy;
        _finishPunchTrigger.RemoveListener(Finisher);
        _failTrigger.RemoveListener(Fail);
    }

    void OnStart() => _started = true;

    void OnDamage(float change)
    {
        if (!_started || change > 0f)
            return;

        if (_strength <= 0f)
        {
            return;
        }

        _animator.Play(_stumble);
    }

    Enemy _selEnemy = null;
    void PunchEnemy(Enemy enemy)
    {
        if (_selEnemy != null)
            EnemyHit();

        _selEnemy = enemy;
        base.Punch();
    }


    public void EnemyHit()
    {
        if (_selEnemy == null)
            return;

        _enemyHit?.Invoke(_selEnemy);
        _selEnemy = null;
    }

    void Finisher()
    {
        _animator.Play(_finishPunch);
    }
     
    void Fail()
    {
        _animator.enabled = false;
    }
}