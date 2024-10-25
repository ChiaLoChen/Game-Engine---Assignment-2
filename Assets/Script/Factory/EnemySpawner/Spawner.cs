using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public void spawn(Vector3 pos, ScoreUI scoreManage)
    {
        GameObject newEntity = Instantiate(createEntity(), pos, Quaternion.identity);
    }

    public abstract GameObject createEntity();

}

