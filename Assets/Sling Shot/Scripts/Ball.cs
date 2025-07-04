using UnityEngine;
using DG.Tweening; // DOTween namespace

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float rotationSpeed = 10f; // Speed of the rotation
    [SerializeField] private float destroyAfterSeconds = 1.5f; // Duration before the ball is destroyed

    [SerializeField] private GameObject boomerangPrefab; // Prefab reference for Boomerang
    [SerializeField] private ParticleSystem snowmanParticleEffect; // Particle effect to play on Snowman deactivation
    [SerializeField] private Vector3 particleEffectOffset = new Vector3(0, 2f, 0); // Offset for the particle effect

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    public void Push(Vector3 force)
    {
        _rb.isKinematic = false;
        _rb.AddForce(_rb.mass * force, ForceMode.Impulse);

        // Apply rotation to the ball
        ApplyRotation(force);

        // Destroy the ball after a few seconds
        Destroy(gameObject, destroyAfterSeconds);
    }

    private void ApplyRotation(Vector3 force)
    {
        // Calculate rotation based on force
        float rotationAmount = force.magnitude * rotationSpeed;
        _rb.angularVelocity = new Vector3(0, rotationAmount, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if this object matches the Boomerang prefab
        if (gameObject == boomerangPrefab)
        {
            // Check if the collided object has the "Snowman" tag
            if (collision.gameObject.CompareTag("Snowman"))
            {
                // Play the particle effect above the Snowman's position
                if (snowmanParticleEffect != null)
                {
                    Vector3 effectPosition = collision.transform.position + particleEffectOffset;
                    ParticleSystem instantiatedEffect = Instantiate(snowmanParticleEffect, effectPosition, Quaternion.identity);

                    // Set the particle system's start color alpha to be tweened
                    var mainModule = instantiatedEffect.main;
                    Color startColor = mainModule.startColor.color;

                    // Use DOTween to fade out the alpha of the particle effect
                    DOTween.To(() => startColor.a, x => {
                        Color newColor = startColor;
                        newColor.a = x;
                        mainModule.startColor = newColor;
                    }, 0f, 3f).OnComplete(() => Destroy(instantiatedEffect.gameObject));
                }

                // Deactivate the Snowman object
                collision.gameObject.SetActive(false);
            }
        }
    }
}
