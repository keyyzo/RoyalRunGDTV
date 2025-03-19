using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float effectCooldownLength = 1f;
    [SerializeField] float shakeModifier = 10.0f;



    float cooldownTimer = 0.0f;

    CinemachineImpulseSource cinemachineImpulseSource;


    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < effectCooldownLength)
            return;

        FireImpulse();
        CollisionFX(collision);
        cooldownTimer = 0.0f;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other)
    {
        
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        audioSource.Play();


    }
}
