using Euphrates;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BuildUpToVal : MonoBehaviour
{
    [SerializeField] float _duration = 0.5f;
	TextMeshProUGUI _text;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    public void Play(IntSO end) => Play((float)end.Value);

    public void Play(int end) => Play((float)end);

    public void Play(FloatSO end) => Play(end.Value);

    public void Play(float end) => Tween.Lerp(0f, end, _duration, (object val) => _text.text = ((float)val).ToString("0."));
}
