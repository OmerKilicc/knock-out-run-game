using Euphrates;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class EnemyStrengthIndicator : MonoBehaviour
{
    [SerializeReference] Enemy _enemy;
    [SerializeReference] TextMeshProUGUI _text;

    [Header("Animation"), Space]
    [SerializeField] float _duration = .5f;
    [SerializeReference] AnimationCurveSO _alphaAnim;
    [SerializeReference] AnimationCurveSO _scaleAnim;

    CanvasGroup _cg;

    private void Awake() => _cg = GetComponent<CanvasGroup>();

    void OnEnable()
    {
        _enemy.OnDefeat.AddListener(Defeat);
        _enemy.OnStrengthSet += Show;
    }

    void OnDisable()
    {
        _enemy.OnDefeat.RemoveListener(Defeat);
        _enemy.OnStrengthSet -= Show;
    }

    void Defeat() => Tween.Lerp(1, 0, _duration, (object val) => _cg.alpha = (float)val);

    void Show()
    {
        if (_cg == null)
            return;

        _text.text = _enemy.Strength.ToString();
        Tween.Lerp(0, 1, _duration, (object val) => { if (_cg != null) _cg.alpha = (float)val; });
    }
}
