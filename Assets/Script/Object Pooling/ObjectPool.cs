using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int maxPoolSize = 10;      
    public int stackDefaultCapacity = 10;  
    private IObjectPool<Bullet> _pool;

    public IObjectPool<Bullet> Pool
    {
        get
        {
            if (_pool == null)
                _pool = new ObjectPool<Bullet>(
                    CreatedPooledItem,        
                    OnTakeFromPool,          
                    OnReturnedToPool,         
                    OnDestroyPoolObject,      
                    true,                    
                    stackDefaultCapacity,     
                    maxPoolSize            
                );
            return _pool;
        }
    }

    private Bullet CreatedPooledItem()
    {
        GameObject bulletObject = Instantiate(bulletPrefab);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Pool = Pool; 
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true); 
        bullet.ResetBullet();            
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject); 
    }

    public void SpawnAndShoot()
    {
        int amount = Random.Range(1, 5);
        for (int i = 0; i < amount; ++i)
        {
            Bullet bullet = Pool.Get();
            bullet.transform.position = transform.position;
            bullet.LaunchBullet();
        }
    }

}
