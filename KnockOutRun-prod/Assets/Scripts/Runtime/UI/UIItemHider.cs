using Euphrates;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class UIItemHider : MonoBehaviour
{
    [SerializeField] float _duration = 0.5f;
    public UnityEvent OnEnd;
	CanvasGroup _cg;

    private void Awake() => _cg = GetComponent<CanvasGroup>();

    public void Hide() => Tween.Lerp(1, 0, _duration, (object val) => _cg.alpha = (float)val, () => OnEnd.Invoke());
}
