using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    // Reference to Bullet Prefab (Assign this in the Unity Inspector)
    public GameObject bulletPrefab;

    // Pool settings
    public int maxPoolSize = 10;           // Maximum size of the pool
    public int stackDefaultCapacity = 10;  // Initial capacity of the pool

    // Object pool for Bullet objects
    private IObjectPool<Bullet> _pool;

    public IObjectPool<Bullet> Pool
    {
        get
        {
            // Initialize the pool if it hasn't been created yet
            if (_pool == null)
                _pool = new ObjectPool<Bullet>(
                    CreatedPooledItem,        // Function to create bullets
                    OnTakeFromPool,           // Function to activate bullets when taken from pool
                    OnReturnedToPool,         // Function to deactivate bullets when returned to pool
                    OnDestroyPoolObject,      // Function to destroy bullets when no longer needed
                    true,                      // Allow shrinking the pool if necessary
                    stackDefaultCapacity,     // Pool's initial size
                    maxPoolSize               // Max pool size
                );
            return _pool;
        }
    }

    // Function to create a new bullet (called when pool needs more items)
    private Bullet CreatedPooledItem()
    {
        // Instantiate a new bullet from the prefab
        GameObject bulletObject = Instantiate(bulletPrefab);
        Bullet bullet = bulletObject.GetComponent<Bullet>();  // Get the Bullet component
        bullet.Pool = Pool; // Assign the pool to the bullet
        bullet.gameObject.SetActive(false); // Start the bullet as inactive
        return bullet;
    }

    // Function to handle when a bullet is taken from the pool
    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);  // Activate the bullet
        bullet.ResetBullet();               // Reset any state like position or velocity
    }

    // Function to handle when a bullet is returned to the pool
    private void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Deactivate the bullet
    }

    // Function to destroy the bullet when it's no longer needed
    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject); // Destroy the bullet gameObject
    }

    // Method to spawn and shoot bullets
    public void SpawnAndShoot()
    {
        int amount = Random.Range(1, 5);  // Random number of bullets to spawn
        for (int i = 0; i < amount; ++i)
        {
            Bullet bullet = Pool.Get();  // Get a bullet from the pool
            bullet.transform.position = transform.position;  // Set spawn position (can be player's position)
            bullet.LaunchBullet();        // Launch the bullet forward
        }
    }

}
