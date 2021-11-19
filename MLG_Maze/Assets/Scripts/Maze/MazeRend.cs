using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class MazeRend : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;
    [SerializeField]
    [Range(1, 50)]
    private int height = 10;
    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private Transform wallPrefab = null;
    [SerializeField]
    private Transform floorPrefab = null;
    [SerializeField]
    private Transform exit = null;
    [SerializeField]
    private Transform keyPrefab = null;
    [SerializeField]
    private Transform mobPrefab = null;
    [SerializeField]
    private Transform weaponPrefab = null;
    [SerializeField]
    private Transform ammoPrefab = null;

    //Some bad technique for probability ;)
    private int[] proba = {0,0,0,1,1,1,1,3,3};
    // 0 = keys, 1 = mobs, 3= ammo

    public NavMeshAgent navMeshAgent;
    public NavMeshSurface surface;

    private Stack<Vector3> Draw(WallState[,] maze)
    {
        //Create stack to stock deadEnd positions
        var DeadEndStack = new Stack<Vector3>();


        //Parse the board
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //actual cell
                var cell = maze[i, j];
                //transform board postion to world position
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                //if board cell is tagged with up wall, create a wall up of the cell
                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                //if board cell is tagged with left wall, create a wall left of the cell
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                //if board cell is tagged with right wall, create a wall right of the cell
                if (i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                //if board cell is tagged with down wall, create a wall down of the cell
                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var botWall = Instantiate(wallPrefab, transform) as Transform;
                        botWall.position = position + new Vector3(0, 0, -size / 2);
                        botWall.localScale = new Vector3(size, botWall.localScale.y, botWall.localScale.z);
                        botWall.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
                //If the cell got three wall is tagged deadend and be use to spawn items
                if (cell.HasFlag(WallState.DEADEND))
                {
                    DeadEndStack.Push(position);
                    
                    //Check the probability tab to choose what to spawn
                    var realPosition = position * 10;
                    int selected = proba[Random.Range(0, proba.Length)];

                    //Spawn a key
                    if (selected == 0)
                    {
                        var key = Instantiate(keyPrefab, realPosition, keyPrefab.transform.rotation) as Transform;                      
                    }
                    //Spawn a ennemy
                    if (selected == 1)
                    {
                        var mob = Instantiate(mobPrefab, realPosition, Quaternion.identity) as Transform;
                    }          
                    //Spawn an ammo box
                    if (selected == 3)
                    {
                        var ammo = Instantiate(ammoPrefab, realPosition, Quaternion.identity) as Transform;
                    }
                }
            }
        }

        return DeadEndStack;
    }


    //Found the farthest deadend to spawn the exit
    public void GetFarthestDeadEnd(Stack<Vector3> DeadEndStack)
    {
        Vector3 buff = new Vector3(0, 0, 0), spawn = new Vector3(0, 0, 0);
        float dist = 0f, res = 0f;
        Vector3 farthestDE = new Vector3(0, 0, 0);


        //Classic tri
        while (DeadEndStack.Count > 0)
        {
            buff = DeadEndStack.Pop();
            dist = Vector3.Distance(buff, spawn);

            if (dist > res)
            {
                res = dist;
                farthestDE = buff;
            }
        }
        //Instantiate exit
        Vector3 exitPos = new Vector3(farthestDE.x * 10, 0, farthestDE.z * 10);
        var ExitVar = Instantiate(exit, exitPos, exit.transform.rotation);
    }

    void Start()
    {
        var maze = MazeGen.Generate(width, height);
        MazeGen.GetDeadEnd(maze, width, height);
        var deadEndStack = Draw(maze);
        transform.localScale = new Vector3(10, 5, 10);
        transform.position = Vector3.zero;

        //InstantiateMapElements(deadEndStack);
        GetFarthestDeadEnd(deadEndStack);
        //Instantiate the floor
        var floor = Instantiate(floorPrefab, new Vector3(0, -2.5f, 0), Quaternion.identity);
        floor.transform.localScale = new Vector3(height + 2, 1, width + 2);

        surface.BuildNavMesh();
    }
}
