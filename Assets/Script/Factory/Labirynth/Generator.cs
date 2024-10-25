using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Generator : singleton<Generator>
{
	public GameObject wall;
	public GameObject floor;
    public GameObject enemySpawn;
    public int width = 10;
    public int height = 10;
    public int gapsize = 2;
    public float wallProbability = 0.3f;
    private List<Vector2Int> directions;

    void Start()
    {
        GenerateLab();
        
        //create 10 enemySpawn and randomly distribute them through out the map
        for (int i = 0; i < 10; i++)
        {
            CreateElement(Random.Range(1, height), Random.Range(1, width), "enemySpawn", Quaternion.identity, 0);
        }
        //Create an outside barrier wall surrounding the entire maze no matter the width and height
        CreateOutsideWall();
    }
    private void GenerateLab()
    {
        //randomly generate the wall on the map
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Random.value < wallProbability)
                {
                    CreateElement(i, j, "Wall", Quaternion.identity, 0);
                }
            }
        }
        //fully generate floor within the width and height
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                CreateElement(i, j, "Floor", Quaternion.identity, 0);
            }
        }

    }
    
    private void CreateElement(int x, int y, string type, Quaternion rotation, int closer)
    {
        //create specific prefab based on the type
        LabrynthCreation element = new Wall();
        Vector3 position = new Vector3(x * gapsize, 0, y * gapsize);
        if (type == "Wall")
        {
            position.y = 2; 
            element.Create(position, wall, Quaternion.identity);
        }
        else if(type == "Floor")
        {
            element.Create(position, floor, Quaternion.identity);
        }
        else if(type == "enemySpawn")
        {
            element.Create(position, enemySpawn, Quaternion.identity);
        }
        else if (type == "OutsideWall")
        {
            position.y = 2;
            position.x += closer;
            position.z += closer;
            element.Create(position, wall, rotation);
        }
    }
    private void CreateOutsideWall()
    {
        //create top and bottom wall in each column
        for (int i = -1; i <= width; i++)
        {
            CreateElement(i, -1, "OutsideWall", Quaternion.identity, 5); // Bottom wall
            CreateElement(i, height, "OutsideWall", Quaternion.identity, -5); // Top wall
        }
        //create left and right wall in each row
        for (int j = -1; j <= height; j++)
        {
            CreateElement(-1, j, "OutsideWall", Quaternion.Euler(0, 90, 0), 5); // Left wall
            CreateElement(width, j, "OutsideWall", Quaternion.Euler(0, 90, 0), -5); // Right wall
        }
    }
}