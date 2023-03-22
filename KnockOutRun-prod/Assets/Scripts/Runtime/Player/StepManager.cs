using UnityEngine;

public class StepManager : MonoBehaviour
{
	public void TriggerSound()
    {
        SoundManager.Play("step", 1, true, 1f, 0.1f, .5f);
    }
}
