using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Euphrates;

public class CharacterAnimationControls : MonoBehaviour
{
    [SerializeReference] protected Animator _animator;
    [SerializeField] string _punchAnimTrigger;
    [SerializeField] List<string> _punchAnims = new List<string>();

    CinemachineImpulseSource _impulse;

    private void Awake() => _impulse = GetComponent<CinemachineImpulseSource>();

    public void Disable() => _animator.enabled = false;

    public void Punch()
    {
        if (_animator == null || string.IsNullOrEmpty(_punchAnimTrigger))
            return;

        // Get a random animation name from the list and pass it to the animator.
        _animator.SetTrigger(_punchAnimTrigger);

        // Generate the punch hit shake.
        _impulse.GenerateImpulse(Vector3.forward);

        // Play the sound.
        SoundManager.Play("hit", 0, true, 1f);
    }

    public void PunchRand()
    {
        if (_animator == null || _punchAnims.Count < 1)
            return;

        // Get a random animation name from the list and pass it to the animator.
        _animator.Play(_punchAnims.GetRandomItem());

        // Generate the punch hit shake.
        _impulse.GenerateImpulse(Vector3.forward);

        // Play the sound.
        SoundManager.Play("hit", 0, true, 1f);
    }
}
