using Euphrates;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionDisplayer : MonoBehaviour
{
    [SerializeReference] FloatSO _progression;
    [SerializeReference] Slider _slider;

    private void OnEnable()
    {
        _progression.OnChange += OnChange;
    }

    private void OnDisable()
    {
        _progression.OnChange -= OnChange;
    }

    void OnChange(float change)
    {
        _slider.value = _progression.Value;
    }
}
