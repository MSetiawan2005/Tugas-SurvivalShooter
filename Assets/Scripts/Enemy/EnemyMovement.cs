using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Transform _player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private UnityEngine.AI.NavMeshAgent _nav;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    private void Update()
    {
        // TODO: Implement debounce for destination update
        if (_enemyHealth.currentHealth > 0 && _playerHealth.currentHealth > 0)
        {
            _nav.SetDestination(_player.position);
        }
        else
        {
            _nav.enabled = false;
        }
    }
}
