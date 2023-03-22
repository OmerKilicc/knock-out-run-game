using Euphrates;
using UnityEngine;

public class SetEnemyThrow : MonoBehaviour
{
    [SerializeReference] Vector3SO _throw;

    [SerializeField] Vector3 _firstValue;
    [SerializeField] Vector3 _secondValue;
    [SerializeField] Vector3 _thirdValue;

    public void SetFirstThrow() => _throw.Value = _firstValue;
    public void SetSecondThrow() => _throw.Value = _secondValue;
    public void SetThirdThrow() => _throw.Value = _thirdValue;
}
