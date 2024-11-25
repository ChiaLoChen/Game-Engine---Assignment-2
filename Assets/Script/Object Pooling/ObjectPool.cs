using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;

    public IObjectPool<Bullet> Pool
    {
        get
        {
            if (_pool == null)
                _pool =
                    new ObjectPool<Bullet>(
                        CreatedPooledItem,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        true,
                        stackDefaultCapacity,
                        maxPoolSize);
            return _pool;
        }
    }

    private IObjectPool<Bullet> _pool;

    private Bullet CreatedPooledItem()
    {
        var go =
            GameObject.CreatePrimitive(PrimitiveType.Cube);
        Bullet bullet = go.AddComponent<Bullet>();
        go.name = "Bullet";
        bullet.Pool = Pool;
        return bullet;
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Spawn()
    {
        var amount = Random.Range(1, 10);
        for (int i = 0; i < amount; ++i)
        {
            var bullet = Pool.Get();
            bullet.transform.position =
                Random.insideUnitSphere * 10;
        }
    }
}
