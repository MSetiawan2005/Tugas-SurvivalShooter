using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    private Animator _anim;
    private GameObject _player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private bool _playerInRange;
    private float _timer;
    
    private static readonly int PlayerDead = Animator.StringToHash("PlayerDead");


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _anim = GetComponent<Animator>();
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeBetweenAttacks && _playerInRange && _enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (_playerHealth.currentHealth <= 0)
        {
            _anim.SetTrigger(PlayerDead);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player && other.isTrigger == false)
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerInRange = false;
        }
    }


    private void Attack()
    {
        _timer = 0f;

        if (_playerHealth.currentHealth > 0)
        {
            _playerHealth.TakeDamage(attackDamage);
        }
    }
}