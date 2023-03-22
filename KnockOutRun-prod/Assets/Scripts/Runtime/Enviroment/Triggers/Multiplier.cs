using Euphrates;
using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeReference] MeshRenderer _mesh;
    [SerializeReference] TextMeshProUGUI _text;
    [SerializeReference] FloatSO _strength;
    [SerializeReference] LayerMaskSO _champLayer;
    [SerializeReference] TriggerChannelSO _endGameTrigger;
    [SerializeReference] FloatSO _gameMultiplier;

    void OnEnable() => _endGameTrigger.AddListener(OnEnd);

    void OnDisable() => _endGameTrigger.RemoveListener(OnEnd);

    float _multiplier = 1f;
    bool _isEnabled = true;

    public void Init(float multiplier)
    {
        _multiplier = multiplier;
        _text.text = $"x{_multiplier.ToString("0.##")}";
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!_isEnabled)
            return;

        int pow = (int)Mathf.Pow(2, collision.gameObject.layer);

        if ((_champLayer.Value & pow) != pow)
            return;

        Champ champ = collision.gameObject.transform.GetFirstParentsComponent<Champ>();
        if (champ == null)
            return;

        champ.Stop();

        if (_gameMultiplier < _multiplier)
            _gameMultiplier.Value = _multiplier;

        void Rise(object val)
        {
            transform.position = new Vector3(transform.position.x, (float)val, transform.position.z);
        }

        Tween.Lerp(transform.position.y, transform.position.y + 5f, 1f, Rise);

        _endGameTrigger.Invoke();
    }

    void OnEnd()
    {
        _isEnabled = false;
    }
}
