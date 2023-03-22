using UnityEngine;
using UnityEngine.Events;

public class ChannelListener : MonoBehaviour
{
    [SerializeReference] TriggerChannelSO _listened;
    [SerializeReference] EnemyChannelSO _listenedEnemy;
    public UnityEvent OnTrigger;

    private void OnEnable()
    {
        if (_listened != null)
            _listened.AddListener(Invoke);

        if (_listenedEnemy != null)
            _listenedEnemy.AddListener(Invoke);
    }

    private void OnDisable()
    {
        if (_listened != null)
            _listened.RemoveListener(Invoke);

        if (_listenedEnemy != null)
            _listenedEnemy.RemoveListener(Invoke);
    }

    public void Invoke(Enemy _) => Invoke();

    public void Invoke() => OnTrigger.Invoke();
}
