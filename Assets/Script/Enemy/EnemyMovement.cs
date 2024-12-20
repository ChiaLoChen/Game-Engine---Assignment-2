using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class EnemyMovement : Observer
{
    // Import the OnEnemyDeath and OnPlayDialogue functions from the C++ DLL
    [DllImport("CourseProject_Plugins", CallingConvention = CallingConvention.Cdecl)]
    private static extern void SetDebugCallback(DebugCallback callback);
    [DllImport("CourseProject_Plugins", CallingConvention = CallingConvention.Cdecl)]
    private static extern void OnEnemyDeath();

    [DllImport("CourseProject_Plugins", CallingConvention = CallingConvention.Cdecl)]
    
    private static extern void OnPlayDialogue();

    public delegate void DebugCallback(string message);

    // The callback method that gets called by the C++ DLL
    private void DebugCallbackMethod(string message)
    {
        Debug.Log(message); // This will print the message to Unity's console
    }
    private ScoreUI scoreUI;
    bool _playerDead = false;

    [SerializeField]
    GameObject player;
    Rigidbody rb;
    public float speed = 2.5f;
    public int distance = 50;
    public int health = 5;
    public int maxScore = 3;

    bool run = false;

    Vector3 _playerPosition;

    PlayerManager _playerManager;
    public override void Notify(Subject subject)
    {
        _playerDead = subject.GetComponent<PlayerManager>().isDead;
        _playerPosition = subject.GetComponent<PlayerManager>().currentPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        scoreUI = FindObjectOfType<ScoreUI>();

        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager.attachObserver(this);
        SetDebugCallback(DebugCallbackMethod);
        OnPlayDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerDead && player != null)
        {
            if(Vector3.Magnitude(_playerPosition - transform.position) < distance)
            {
                run = true;
            }
        }
        if (health <= 0)
        {
            Die();
            OnEnemyDeath();
        }

        if (run)
        {
            gameObject.transform.LookAt(player.transform.position);
            rb.AddForce(-transform.forward.normalized * (speed * 10f * Time.deltaTime), ForceMode.Force);
        }
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    void Die()
    {
        scoreUI.OnEnemyDeath();
        scoreUI.SetDirty();
        Destroy(gameObject);
    }
/*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            rb.AddForce(transform.up.normalized * 5 * 200f, ForceMode.Force);
        }
    }
*/
}
