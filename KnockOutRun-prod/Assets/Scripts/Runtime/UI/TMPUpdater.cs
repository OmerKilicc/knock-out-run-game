using Euphrates;
using TMPro;
using UnityEngine;

public class TMPUpdater : MonoBehaviour
{
    [SerializeReference] TextMeshProUGUI _uiText;
    [SerializeField] string _prefix = "";
    [SerializeReference] FloatSO _floatValue;
    [SerializeReference] IntSO _intValue;

    private void Start() => SetText(0f);

    private void OnEnable()
    {
        if (_floatValue != null)
        {
            _floatValue.OnChange += SetText;
            SetText();
            return;
        }

        if (_intValue != null)
            _intValue.OnChange += SetText;
    }

    private void OnDisable()
    {
        if (_floatValue != null)
        {
            _floatValue.OnChange -= SetText;
            return;
        }

        if (_intValue != null)
            _intValue.OnChange -= SetText;
    }


    public void SetText() => SetText(0f);

    void SetText(int change)
    {
        SetText((float) change);
    }

    void SetText(float change)
    {
        if (_floatValue != null)
        {
            _uiText.text = _prefix + _floatValue.Value.ToString("0.##");
            return;
        }

        if (_intValue != null)
        {
            _uiText.text = _prefix + _intValue.Value.ToString();
            return;
        }

        Debug.LogError("Can't set text! No variable so set.");
    }
}
