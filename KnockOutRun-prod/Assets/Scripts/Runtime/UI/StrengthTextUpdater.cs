using Euphrates;
using TMPro;
using UnityEngine;

public class StrengthTextUpdater : MonoBehaviour
{
    [SerializeReference] TextMeshProUGUI _text;
    [SerializeReference] FloatSO _strength;

    private void OnEnable()
    {
        _strength.OnChange += UpdateText;
    }

    private void OnDisable()
    {
        _strength.OnChange -= UpdateText;
    }

    private void Start()
    {
        UpdateText(0);
    }

    void UpdateText(float change)
    {
        Tween.Lerp(_strength - change, _strength, 1f, (object val) => SetText((float)val));
    }

    void SetText(float val) => _text.text = val.ToString("0");
}
