using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyObserver> observers = new List<EnemyObserver>();
    // Start is called before the first frame updat
    public void AddObserver(EnemyObserver observer)
    {
        observers.Add(observer);
    }

    // Method to remove an observer
    public void RemoveObserver(EnemyObserver observer)
    {
        observers.Remove(observer);
    }
}
