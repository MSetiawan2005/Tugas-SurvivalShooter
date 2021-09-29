using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    
    private readonly float _effectsDisplayTime = 0.2f;
    private AudioSource _gunAudio;
    private Light _gunLight;
    private LineRenderer _gunLine;
    private ParticleSystem _gunParticles;
    private int _shootableMask;
    private RaycastHit _shootHit;
    private Ray _shootRay;

    private float _timer;

    private void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");
        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _gunLight = GetComponent<Light>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (_timer >= timeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }

    public void Shoot()
    {
        _timer = 0f;

        _gunAudio.Play();

        _gunLight.enabled = true;

        _gunParticles.Stop();
        _gunParticles.Play();

        _gunLine.enabled = true;
        _gunLine.SetPosition(0, transform.position);

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
        {
            var enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
            }

            _gunLine.SetPosition(1, _shootHit.point);
        }
        else
        {
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
        }
    }
}