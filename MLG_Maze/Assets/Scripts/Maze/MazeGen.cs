using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;



[Flags]
public enum WallState
{
    LEFT = 1, //1
    RIGHT = 2,//10
    UP = 4,//100
    DOWN = 8,//1000

    DEADEND = 16,//10000
    VISITED = 128,//1000000
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbor
{
    public Position Position;
    public WallState SharedWall;
}


public static class MazeGen
{

    //Return the opposite wall for a given wall
    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT:
                return WallState.LEFT;
            case WallState.LEFT:
                return WallState.RIGHT;
            case WallState.UP:
                return WallState.DOWN;
            case WallState.DOWN:
                return WallState.UP;

            default: return WallState.LEFT;
        }
    }

    //backtracking algorithm to "build" the maze
    private static WallState[,] BackTrack(WallState[,] maze, int width, int height)
    {
        //Random seed to choose a random direction during process
        int seed = Random.Range(1, 100000);
        var rdm = new System.Random(seed);
        var positionStack = new Stack<Position>();
         

        var position = new Position { X = rdm.Next(0, width), Y = rdm.Next(0, height)};

        //Tag a cell as already visited
        maze[position.X, position.Y] |= WallState.VISITED;
        positionStack.Push(position);

        
        //Parse all the cell, use pop to go to next cell, can go only to unvisited neighbors
        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbors = GetUnvisitedNeighbors(current, maze, width, height);

            if(neighbors.Count > 0)
            {
                positionStack.Push(current);

                //Choose a random neighbors
                var randIndex = rdm.Next(0, neighbors.Count);
                var randomNeighbor = neighbors[randIndex];

                var nPosition = randomNeighbor.Position;

                //Check shared walls
                maze[current.X, current.Y] &= ~randomNeighbor.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbor.SharedWall);

                //Visited flag
                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;
                //go next
                positionStack.Push(nPosition);
            }
        }

        return maze;
    }
    //Check if visited flagged
    private static List<Neighbor> GetUnvisitedNeighbors(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbor>();

        //left
        if(p.X > 0)
        {
            if(!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbor
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }
        //bot
        if (p.Y > 0)
        {
            if (!maze[p.X, p.Y-1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbor
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }
        //up
        if (p.Y < height -1)
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbor
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP
                });
            }
        }
        //right
        if (p.X < width - 1)
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbor
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }

        return list;
    }

    //Init the maze
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState init = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN; ;

        for (int i =0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                maze[i, j] = init;
            }
        }

        return BackTrack(maze, width, height);
    }
    //found all the deadend by checking if 3 walls 
    public static void GetDeadEnd(WallState[,] maze, int height, int width)
    {          
        for (int i =0; i < width; i++)
        {
            for(int j =0; j < height; j++)
            {
                if (maze[i, j].HasFlag(WallState.LEFT)&& maze[i, j].HasFlag(WallState.RIGHT)&&maze[i, j].HasFlag(WallState.UP))
                {
                    maze[i,j] |= WallState.DEADEND;
                }
                if (maze[i, j].HasFlag(WallState.LEFT) && maze[i, j].HasFlag(WallState.RIGHT) && maze[i, j].HasFlag(WallState.DOWN))
                {
                    maze[i, j] |= WallState.DEADEND;
                }
                if (maze[i, j].HasFlag(WallState.LEFT) && maze[i, j].HasFlag(WallState.DOWN) && maze[i, j].HasFlag(WallState.UP))
                {
                    maze[i, j] |= WallState.DEADEND;
                }
                if (maze[i, j].HasFlag(WallState.RIGHT) && maze[i, j].HasFlag(WallState.DOWN) && maze[i, j].HasFlag(WallState.UP))
                {
                    maze[i, j] |= WallState.DEADEND;
                }
                if (maze[i, j].HasFlag(WallState.UP) && maze[i, j].HasFlag(WallState.DOWN) && maze[i, j].HasFlag(WallState.LEFT))
                {
                    maze[i, j] |= WallState.DEADEND;
                }
                //Debug.Log(maze[i, j].HasFlag(WallState.DEADEND) + "(" + i + "," + j + ")");
            }
        }
    }

    
}
