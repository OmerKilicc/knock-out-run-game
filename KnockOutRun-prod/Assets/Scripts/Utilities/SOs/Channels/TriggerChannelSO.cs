using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Trigger Channel", menuName = "SO Channels/Trigger")]
public class TriggerChannelSO : ScriptableObject
{
	[SerializeField] UnityEvent OnTrigger;

	public bool Silent { get; set; } = false;

    public void AddListener(UnityAction listener) => OnTrigger.AddListener(listener);
	public void RemoveListener(UnityAction listener) => OnTrigger.RemoveListener(listener);

    public void Invoke()
    {
        if (Silent)
            return;

        OnTrigger?.Invoke();
    }
}
