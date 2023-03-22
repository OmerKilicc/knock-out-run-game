using Euphrates;
using UnityEngine;

public class MusclePickup : MonoBehaviour, IPickup
{
    [SerializeField] float _muscleChange = 0f;
    public float MuscleChange => _muscleChange;

    [SerializeReference] FloatSO _muscleAmount;

    public void DoAction()
    {
        if (_muscleAmount == null)
            return;

        Tween.Lerp(_muscleAmount.Value, _muscleAmount.Value + _muscleChange, 0.5f, (object val) => _muscleAmount.Value = (float)val);
    }
}
