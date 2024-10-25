using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleton<C> : MonoBehaviour where C : Component
{
    private static C _instance;
    
    public static C Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<C>();

                if(_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(C).Name;
                    _instance = obj.AddComponent<C>();
                }
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {

        if (_instance == null)
        {
            _instance = this as C;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
