using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    private Animator _anim;
    private CapsuleCollider _capsuleCollider;
    private AudioSource _enemyAudio;
    private ParticleSystem _hitParticles;
    private bool _isDead;
    private bool _isSinking;
    private static readonly int Dead = Animator.StringToHash("Dead");


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemyAudio = GetComponent<AudioSource>();
        _hitParticles = GetComponentInChildren<ParticleSystem>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }


    private void Update()
    {
        if (_isSinking) transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (_isDead) return;

        _enemyAudio.Play();

        currentHealth -= amount;

        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();

        if (currentHealth <= 0) Death();
    }


    private void Death()
    {
        _isDead = true;

        _capsuleCollider.isTrigger = true;

        _anim.SetTrigger(Dead);

        _enemyAudio.clip = deathClip;
        _enemyAudio.Play();
    }


    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        _isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}