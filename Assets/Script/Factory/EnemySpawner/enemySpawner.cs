using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading;

public class enemySpawner : Spawner
{
    [SerializeField]
    List<Transform> _spawns = new List<Transform>();

    [SerializeField]
    GameObject _enemyPrefab;
    ScoreUI scoreManager;

    int randomNum;

    public bool started = false;

    PlayerManager _playerManager;

    
    void Start()
    {
        //scoreManager = GetComponent<ScoreUI>();

        scoreManager = FindObjectOfType<ScoreUI>();

        //check for children that are spawn locations then add them to the list
        Thread.Sleep(1000);
        GameObject[] enemySpawn = GameObject.FindGameObjectsWithTag("SpawnLocation");
        for (int i = 0; i < enemySpawn.Length; i++)
        {
            _spawns.Add(enemySpawn[i].transform);
        }
        //for(int i = 0; i < this.gameObject.transform.childCount; i++)
        //{
        //    Transform currentObject = this.gameObject.transform.GetChild(i);
        //    if (currentObject.tag == "SpawnLocation")
        //    {
        //        _spawns.Add(currentObject);
        //    }
        //}

    }

    void Update()
    {

        if (started)
        {
            //if current time is divisible by 10, summon an enemy

            //Spawns like 5 enemies every batch, unintended but maybe keep

            double time = Math.Round(timer.Instance.getTime(), 2);

            if (time % 10 == 0 && timer.Instance.canCount)
            {
                spawnEnemyInPosition(_spawns);
            }
        }

        
    }


    //creates a random number then spawns an enemy at the transform in the index of that random number
    void spawnEnemyInPosition(List<Transform> tArray)
    {
        randomNum = Random.Range(0, _spawns.Count);
        spawn(tArray[randomNum].position, scoreManager);
        
        //Debug.Log("enemy spawned");
    }


    public override GameObject createEntity()
    {
        return _enemyPrefab;
    }
}
