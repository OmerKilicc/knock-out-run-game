using Euphrates;
using UnityEngine;

public class UIItemDisplayer : MonoBehaviour
{
    [SerializeField] float _duration = 0.5f;
    CanvasGroup _cg;

    private void Awake() => _cg = GetComponent<CanvasGroup>();

    public void Show() => Tween.Lerp(0, 1, _duration, (object val) => _cg.alpha = (float)val);
}
