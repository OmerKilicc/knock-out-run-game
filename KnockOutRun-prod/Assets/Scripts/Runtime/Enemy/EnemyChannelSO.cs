using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Enemy Channel", menuName = "SO Channels/Enemy")]
class EnemyChannelSO : ScriptableObject 
{
    public event Action<Enemy> OnTrigger;

    public void AddListener(Action<Enemy> listener) => OnTrigger += listener;
    public void RemoveListener(Action<Enemy> listener) => OnTrigger -= listener;

    public void Invoke(Enemy data) => OnTrigger?.Invoke(data);
}
