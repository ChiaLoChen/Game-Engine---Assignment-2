using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : Subject
{
    public int currentHealth { get { return health; } }
    public int currentScore { get { return score; } }

    public Vector3 currentPosition { get { return _position; } }

    public bool isDead { get; private set; }

    [SerializeField]
    private int health = 100; 
    private int score = 0;

    private Vector3 _position;
    private Vector3 _lastPosition = new Vector3(0,0,0);


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;

        
        
    }

    // Update is called once per frame
    void Update()
    {

        _position = transform.position;

        if(_lastPosition != _position)
        {
            NotifyObservers();
            _lastPosition = _position;
            
        }


        if (health == 0)
        {
            onDie();
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            onHealthChange(health - 1);
        }
    }

    public void onDie()
    {
        isDead = true;
        NotifyObservers();
    }

    public void scoreChange(int newScore)
    {
        score = newScore;
        NotifyObservers();
    }

    public void onHealthChange(int newHealth)
    {
        health = newHealth;
        NotifyObservers();
    }

    public void attachObserver(Observer observer)
    {
        Attach(observer);
    }
}
