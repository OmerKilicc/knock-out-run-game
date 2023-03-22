using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    Transform _transform;

    [SerializeField] float _rotateSpeed = 5f;
    [SerializeField] Vector3 _axis = Vector3.up;

    void Awake() => _transform = transform;

    void FixedUpdate() => _transform.rotation *= Quaternion.Euler(_rotateSpeed * Time.fixedDeltaTime * _axis);
}
