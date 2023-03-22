using Euphrates;
using UnityEngine;

public class EnemyMuscleManager : MonoBehaviour
{
    [SerializeReference] SkinnedMeshRenderer _mesh;
    [SerializeReference] Enemy _enemy;

    [SerializeReference] FloatSO _maxMuscle;

    [Header("Scale"), Space]
    [SerializeReference] Transform _scaled;
    [SerializeReference] FloatSO _minScale;
    [SerializeReference] FloatSO _maxScale;

    private void OnEnable()
    {
        _enemy.OnStrengthSet += SetBody;
    }

    private void OnDisable()
    {
        _enemy.OnStrengthSet -= SetBody;
    }

    void SetBody()
    {
        SetMuscle();
        SetScale();
    }

    void SetMuscle()
    {
        float amount = Mathf.Clamp(_enemy.Strength, 0f, _maxMuscle);

        if (amount < 50f)
        {
            _mesh.SetBlendShapeWeight(0, 0);
            Tween.Lerp(_mesh.GetBlendShapeWeight(1), (50f - amount) * 2f, .5f,
                (object val) => { if (_mesh != null) _mesh.SetBlendShapeWeight(1, (float)val); });
            //_mesh.SetBlendShapeWeight(1, (50f - amount) * 2f);
            return;
        }

        float newAmt = amount - 50f;
        newAmt *= 2f;

        _mesh.SetBlendShapeWeight(1, 0);
        Tween.Lerp(_mesh.GetBlendShapeWeight(0), newAmt, .5f,
                (object val) => { if (_mesh != null) _mesh.SetBlendShapeWeight(0, (float)val); });
        _mesh.SetBlendShapeWeight(0, newAmt);
    }

    void SetScale()
    {
        float step = _enemy.Strength / _maxMuscle;
        float newScale = Mathf.Lerp(_minScale, _maxScale, step);

        void StepFunc(object val)
        {
            if (_scaled == null)
                return;

            Vector3 newScale = Vector3.one * (float)val;
            _scaled.localScale = newScale;
        }

        Tween.Lerp(_scaled.localScale.x, newScale, 0.5f, StepFunc);
    }
}
