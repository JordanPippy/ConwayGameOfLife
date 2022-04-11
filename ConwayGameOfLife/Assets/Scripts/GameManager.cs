using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Tile[,] map;
    private bool[,] states;

    public Tile tile;
    public int mapSize;
    public static bool started;
    private bool nextFrame;
    void Start()
    {
        Camera.main.transform.position = new Vector3(mapSize / 2.0f, mapSize / 2.0f, -10f);

        nextFrame = true;
        started = false;
        map = new Tile[mapSize, mapSize];
        states = new bool[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                map[i, j] = Instantiate(tile, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
            RunAlgorithm();
    }

    public void StartAlgorithm()
    {
            Debug.Log("Spacebar has been pressed.");
            StateInit();
            started = true;
    }

    public void RandomFill()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (Random.Range(0, 2) == 0)
                    map[i, j].Respawn();
                else
                    map[i, j].Die();
            }
        }
        StartAlgorithm();
    }

    void StateInit()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
                states[i, j] = map[i, j].isAlive;
        }
    }

    int GetNeighbours(int i, int j)
    {
        int neighbours = 0;
        if (i > 0)
        {
            if (states[i-1, j])
                neighbours++;
            if (j > 0)
            {
                if (states[i-1, j-1])
                    neighbours++;
            }
            if (j < mapSize - 1)
            {
                if (states[i-1, j+1])
                    neighbours++;
            }
        }

        if (i < mapSize - 1)
        {
            if (states[i+1, j])
                neighbours++;
            if (j > 0)
            {
                if (states[i+1, j-1])
                    neighbours++;
            }
            if (j < mapSize - 1)
            {
                if (states[i+1, j+1])
                    neighbours++;
            }
        }
        if (j > 0)
        {
            if (states[i, j-1])
                neighbours++;
        }
        if (j < mapSize - 1)
        {
            if (states[i, j+1])
                neighbours++;
        }
        return neighbours;
    }

    IEnumerator WaitForNextFrame()
    {
        yield return new WaitForSeconds(0.1f);
        nextFrame = true;
    }

    Tile[,] DeepCopy()
    {
        Tile[,] toRet = new Tile[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                toRet[i, j] = map[i, j];
            }
        }
        return toRet;
    }

    void RunAlgorithm()
    {

        if (nextFrame)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    int neighbours = GetNeighbours(i, j);
                    if (map[i, j].isAlive && (neighbours == 2 || neighbours == 3))
                    {
                        //survive
                    }
                    else if (!map[i, j].isAlive && neighbours == 3)
                    {
                        //comes back to life
                        map[i, j].Respawn();
                    }
                    else
                    {
                        map[i, j].Die();
                    }
                }
            }
            StateInit();
            nextFrame = false;
            StartCoroutine(WaitForNextFrame());
        }
    }
}
