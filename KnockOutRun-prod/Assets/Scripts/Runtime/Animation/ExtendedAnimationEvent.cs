using UnityEngine;
using UnityEngine.Events;

public class ExtendedAnimationEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _trigger1;
    [SerializeField] UnityEvent _trigger2;
    [SerializeField] UnityEvent _trigger3;
    [SerializeField] UnityEvent _trigger4;
    [SerializeField] UnityEvent _trigger5;

    public void InvokeFirst() => _trigger1.Invoke();
    public void InvokeSecond() => _trigger2.Invoke();
    public void InvokeThird() => _trigger3.Invoke();
    public void InvokeFourth() => _trigger4.Invoke();
    public void InvokeFifth() => _trigger5.Invoke();
}
