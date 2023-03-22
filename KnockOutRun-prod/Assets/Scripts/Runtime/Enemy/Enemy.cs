using Euphrates;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Strength")]
    public EnemyType EnemyPresetType = EnemyType.Random;

    [SerializeField] float _strengthChange;
    public float StrengthChange
    {
        get => _strengthChange;
        set
        {
            if (value >= 0)
                _strengthChange = Mathf.Clamp(value, 1f, float.MaxValue);
            else
                _strengthChange = value;
        }
    }

    public event Action OnStrengthSet;
    float _strength = 0f;
    public float Strength { get => _strength; }
    [Space]
    [SerializeReference] EnemyHolderSO _enemyHolder;
    [SerializeReference] UnityEventTriggerCollider _trigger;
    [SerializeReference] FloatSO _playerStrength;
    [SerializeReference] FloatSO _missingPoints;
    [SerializeReference] IntSO _coin;
    [SerializeReference] CharacterAnimationControls _anim;
    [SerializeReference] RagdollEnabler _ragdoll;
    [SerializeReference] Rigidbody _rb;
    [SerializeReference] Vector3SO _throw;
    public UnityEvent OnDefeat;

    [Header("Channels"), Space]
    [SerializeReference] EnemyChannelSO _playerPunch;
    [SerializeReference] EnemyChannelSO _enemyHit;
    [SerializeReference] TriggerChannelSO _playerFail;

    bool _finished = false;
    bool _fought = false;

    private void OnEnable()
    {
        _enemyHolder.AddEnemy(this);
        _trigger.OnEnter.AddListener(Fight);
        _enemyHit.AddListener(LoseFight);
        _playerFail.AddListener(OnFinish);
    }

    private void OnDisable()
    {
        _enemyHolder.RemoveEnemy(this);
        _trigger.OnEnter.RemoveListener(Fight);
        _enemyHit.RemoveListener(LoseFight);
        _playerFail.RemoveListener(OnFinish);
    }

    public void Fight(GameObject _)
    {
        if (_finished || _fought)
            return;

        _fought = true;

        float change = _playerStrength.Value - _strength;
        bool lost = change > 0;

        if (lost)
        {
            _playerPunch.Invoke(this);
            _playerStrength.Value += _strength;
            return;
        }

        _playerStrength.Value += change;
        _anim.PunchRand();
    }

    void LoseFight(Enemy enemy)
    {
        if (enemy != this)
            return;

        _anim.Disable();
        _ragdoll.Enable();

        _coin.Value++;

        _rb.AddForce(_throw.Value, ForceMode.Impulse);

        OnDefeat?.Invoke();
    }

    void OnFinish()
    {
        _finished = true;
    }

    public void SetStrength(GameObject _)
    {
        if (_finished)
            return;

        if (_strengthChange < 0)
        {
            _strength = _playerStrength - _strengthChange;
            OnStrengthSet?.Invoke();
            return;
        }

        if (_playerStrength > _strengthChange)
        {
            if (_missingPoints == 0)
            {
                _strength = _strengthChange;
                OnStrengthSet?.Invoke();
                return;
            }

            float availableAmt = _playerStrength - _strengthChange - 1f;

            float max = availableAmt > _missingPoints ? _missingPoints : availableAmt;

            float added = availableAmt == 0 ? 0 : UnityEngine.Random.Range(1, Mathf.RoundToInt(max));
            float totalChange = _strengthChange + added;

            _missingPoints.Value = Mathf.Clamp(_missingPoints - totalChange, 0f, float.MaxValue);


            _strength = _strengthChange + added;
            OnStrengthSet?.Invoke();
            return;
        }

        float missing = _strengthChange - _playerStrength - 1f;
        _strength = Mathf.Clamp(_playerStrength - 1f, 1f, float.MaxValue);
        _missingPoints.Value += missing;
        OnStrengthSet?.Invoke();
    }
}

public enum EnemyType { Random, GiveStrength, RemoveStrength }