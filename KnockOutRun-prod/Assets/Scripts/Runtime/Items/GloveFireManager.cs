using Euphrates;
using UnityEngine;

public class GloveFireManager : MonoBehaviour
{
    [Header("References")]
    [SerializeReference] ParticleSystem _particles;
    [SerializeReference] FloatSO _force;
    [SerializeReference] FloatSO _maxForce;

    [Header("Values"), Space]
    [SerializeField] float _treshold;
    [SerializeField] float _minScale;
    [SerializeField] float _maxScale;
    [SerializeField] float _minEmission;
    [SerializeField] float _maxEmission;

    void Start() => SetParticleIntensity(0);

    void OnEnable() => _force.OnChange += SetParticleIntensity;

    void OnDisable() => _force.OnChange -= SetParticleIntensity;

    void SetParticleIntensity(float _)
    {
        if (_force < _treshold)
        {
            _particles.gameObject.SetActive(false);
            return;
        }

        if (!_particles.gameObject.activeSelf)
            _particles.gameObject.SetActive(true);

        float nForce = _force - _treshold;
        float nMaxForce = _maxForce - _treshold;

        float step = nForce / nMaxForce;

        float scaleVal = Mathf.Lerp(_minScale, _maxScale, step);
        float emissionVal = Mathf.Lerp(_minEmission, _maxEmission, step);

        _particles.transform.localScale = Vector3.one * scaleVal;
        var emission = _particles.emission;
        emission.rateOverTime = emissionVal;
    }
}
