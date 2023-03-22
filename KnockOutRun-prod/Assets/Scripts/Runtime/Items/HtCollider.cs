using System.Collections.Generic;
using UnityEngine;
using Euphrates;

public class HtCollider : MonoBehaviour
{
    [SerializeReference] EnemyChannelSO _enemyHit;

    HashSet<Enemy> _hitEnemies = new HashSet<Enemy>();

    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.transform.GetFirstParentsComponent<Enemy>();
        if (enemy == null)
            return;

        if (_hitEnemies.Contains(enemy))
            return;

        _enemyHit.Invoke(enemy);
        _hitEnemies.Add(enemy);

        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        rb.AddForceAtPosition(transform.up * 10f, collision.GetContact(0).point, ForceMode.Impulse);
    }
}
