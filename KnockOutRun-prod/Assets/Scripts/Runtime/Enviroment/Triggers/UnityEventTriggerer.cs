using UnityEngine;
using UnityEngine.Events;

public class UnityEventTriggerer : MonoBehaviour
{
    [SerializeField] TriggerType _triggerType;
    [SerializeField] UnityEvent _trigger;

    private void Awake()
    {
        if (_trigger != null && _triggerType == TriggerType.Awake)
            _trigger.Invoke();
    }

    private void OnEnable()
    {
        if (_trigger != null && _triggerType == TriggerType.OnEnable)
            _trigger.Invoke();
    }

    private void OnDisable()
    {
        if (_trigger != null && _triggerType == TriggerType.OnDisable)
            _trigger.Invoke();
    }

    void Start()
    {
        if (_trigger != null && _triggerType == TriggerType.Start)
            _trigger.Invoke();
    }
}

enum TriggerType { Awake, OnEnable, OnDisable, Start }