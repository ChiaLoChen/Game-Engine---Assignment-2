using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<Bullet> Pool { get; set; }
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * (Time.deltaTime * 5000f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Bullet"))
        {
            if (other.gameObject.GetComponent<EnemyMovement>() != null)
            {
                other.GetComponent<EnemyMovement>().TakeDamage(1);
            }
            Destroy(gameObject);
        }

    }

    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    private void ResetDrone()
    {
        
    }
}
