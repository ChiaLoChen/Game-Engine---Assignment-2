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
        _rigidbody = GetComponent<Rigidbody>(); 
    }

    public void ResetBullet()
    {
        _timeAlive = 0f; // Reset time alive
        if (_rigidbody != null)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void LaunchBullet()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _timeAlive = 0f;
        if (_rigidbody != null)
        {
            _rigidbody.velocity = player.transform.forward * speed;
        }

        Invoke("ReturnToPool", lifetime);
    }

    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        ReturnToPool();
    }

    private void OnDisable()
    {
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
