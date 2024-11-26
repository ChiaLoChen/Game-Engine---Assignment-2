using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<Bullet> Pool { get; set; }
    Rigidbody rb;
    public float speed = 20f;
    public float lifetime = 5f;

    private Rigidbody _rigidbody;
    private float _timeAlive;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    // Reset bullet properties before returning it to the pool
    public void ResetBullet()
    {
        _timeAlive = 0f; // Reset time alive
        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector3.zero; // Stop any previous velocity
        }
    }

    // Launch the bullet forward
    public void LaunchBullet()
    {
        _timeAlive = 0f;
        if (_rigidbody != null)
        {
            // Shoot the bullet forward in the current forward direction
            _rigidbody.velocity = transform.forward * speed;
        }

        // Destroy the bullet after its lifetime
        Invoke("ReturnToPool", lifetime);
    }

    // Return bullet to the pool after its lifetime
    private void ReturnToPool()
    {
        Pool.Release(this); // Return the bullet to the pool after it is destroyed or goes out of bounds
    }

    private void OnCollisionEnter(Collision other)
    {
        // Optionally handle bullet collisions, e.g., hit targets
        ReturnToPool(); // Return the bullet to the pool after collision
    }

    private void OnDisable()
    {
        // Cancel the delayed call to return to pool if the bullet is disabled prematurely
        CancelInvoke("ReturnToPool");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Bullet"))
        {
            if (other.gameObject.GetComponent<EnemyMovement>() != null)
            {
                other.GetComponent<EnemyMovement>().TakeDamage(1);
            }
            ReturnToPool();
        }

    }
}
