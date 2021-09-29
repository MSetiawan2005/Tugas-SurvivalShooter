using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    private Animator anim;
    private CapsuleCollider capsuleCollider;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private bool isDead;
    private bool isSinking;
    private static readonly int Dead = Animator.StringToHash("Dead");


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }


    private void Update()
    {
        if (isSinking) transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0) Death();
    }


    private void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger(Dead);

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}