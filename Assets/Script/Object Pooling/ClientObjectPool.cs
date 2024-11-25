using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientObjectPool : MonoBehaviour
{
    private ObjectPool _pool;

    void Start()
    {
        _pool = gameObject.AddComponent<ObjectPool>();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Spawn Drones"))
            _pool.Spawn();
    }
}
