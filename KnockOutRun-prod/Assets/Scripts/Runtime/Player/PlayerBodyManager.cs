using Euphrates;
using UnityEngine;

public class PlayerBodyManager : MonoBehaviour
{
    [SerializeReference] SkinnedMeshRenderer _mesh;
    [SerializeReference] Transform _scaled;

    [Header("Muscle"), Space]
    [SerializeReference] FloatSO _strength;
    [SerializeReference] FloatSO _maxMuscle;

    [Header("Scale"), Space]
    [SerializeReference] FloatSO _minScale;
    [SerializeReference] FloatSO _maxScale;

    [Header("Triggers"), Space]
    [SerializeReference] TriggerChannelSO _start;

    void OnEnable()
    {
        _strength.OnChange += SetBody;
        _start.AddListener(SetBody);
    }

    void OnDisable()
    {
        _strength.OnChange -= SetBody;
        _start.RemoveListener(SetBody);
    }

    void SetBody(float _)
    {
        SetMuscle();
        SetScale();
    }
    void SetBody() => SetBody(0f);

    int _tween = -1;
    void SetMuscle()
    {
        float amount = Mathf.Clamp(_strength.Value, 0f, _maxMuscle);

        if (_tween != -1)
            Tween.StopTween(_tween);

        if (amount < 50f)
        {
            _mesh.SetBlendShapeWeight(0, 0);
            _tween = Tween.Lerp(_mesh.GetBlendShapeWeight(1), (50f - amount) * 2f, .5f,
                (object val) => _mesh.SetBlendShapeWeight(1, (float)val), () => _tween = -1);
            return;
        }

        float newAmt = amount - 50f;
        newAmt *= 2f;

        _mesh.SetBlendShapeWeight(1, 0);
        _tween = Tween.Lerp(_mesh.GetBlendShapeWeight(0), newAmt, .5f,
                (object val) => _mesh.SetBlendShapeWeight(0, (float)val), () => _tween = -1);
        _mesh.SetBlendShapeWeight(0, newAmt);
    }

    void SetScale()
    {
        float step = _strength / _maxMuscle;
        float newScale = Mathf.Lerp(_minScale, _maxScale, step);

        void StepFunc(object val)
        {
            Vector3 newScale = Vector3.one * (float)val;
            _scaled.localScale = newScale;
        }

        Tween.Lerp(_scaled.localScale.x, newScale, 0.5f, StepFunc);
    }
}
