using Euphrates;
using UnityEngine;

public class FailDetector : MonoBehaviour
{
    [SerializeReference] TriggerChannelSO _fail;
    [SerializeReference] TriggerChannelSO _started;
    [SerializeReference] FloatSO _strength;
    [SerializeReference] CharacterController _controller;
    [SerializeField] float _coyoteTime = 0.5f;

    public bool _start = false;
    public bool _failed = false;
    public bool _startedFall = false;

    void OnEnable()
    {
        _started.AddListener(GameStart);
        _strength.OnChange += StrengthChange;
    }

    void OnDisable()
    {
        _strength.OnChange -= StrengthChange;
        _started.RemoveListener(GameStart);
    }

    void FixedUpdate()
    {
        if (!_start || _failed)
            return;

        // If falling
        if (_controller.velocity.y < -2f)
        {
            // Do nothing if we already started falling
            if (_startedFall)
                return;

            // Start coyote timer
            //_fail.Invoke();
            _startedFall = true;
            GameTimer.CreateTimer("Coyote Time", _coyoteTime, _fail.Invoke);

            return;
        }

        // If not falling

        // Do nothing if we didn't start falling
        if (!_startedFall)
            return;

        _startedFall = false;
        GameTimer.CancleTimer("Coyote Time");
    }

    void GameStart() => _start = true;

    private void StrengthChange(float change)
    {
        if (_failed || _strength.Value > 0f)
            return;

        Fail();
    }

    void Fail()
    {
        _failed = true;
        _fail.Invoke();
        _fail.Silent = true;
    }
}
