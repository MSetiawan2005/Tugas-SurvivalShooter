using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private Animator _anim;

    private bool _damaged;

    private PlayerShooting _playerShooting;
    private bool _isDead;
    private AudioSource _playerAudio;
    private PlayerMovement _playerMovement;
    private static readonly int Die = Animator.StringToHash("Die");


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
    }


    private void Update()
    {
        if (_damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        _damaged = false;
    }


    public void TakeDamage(int amount)
    {
        _damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        _playerAudio.Play();

        if (currentHealth <= 0 && !_isDead)
        {
            Death();
        }
    }


    private void Death()
    {
        _isDead = true;

        _playerShooting.DisableEffects();

        _anim.SetTrigger(Die);

        _playerAudio.clip = deathClip;
        _playerAudio.Play();

        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
    }
}