using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSphere : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        if (_enemy == null)
        {
            Debug.LogError("Enemy is null in Attack Sphere!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            _enemy.currentState = Enemy.EnemyState.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _enemy.currentState = Enemy.EnemyState.Chase;
        }
    }
}
