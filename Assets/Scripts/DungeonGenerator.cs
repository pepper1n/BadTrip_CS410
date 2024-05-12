using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule 
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        //public bool[] doorsExist = new bool[4];
        public bool obligatory;
        public bool priority;
        public int spawnProbability(int x, int y)
        {
            // 0: no spawn, 1: can spawn, 2: priority to spawn, 3: must spawn
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return (obligatory) ? 3 : (priority) ? 2 : 1;
            }
            return 0;
        }

        /*public int matchDoors(bool[] status)
        {
            if (status == doorsExist) 
            {
                return 1;
            }
            return 0;
        }*/
    }


    public Vector2Int size;
    public int startPos = 0;
    public Vector2 offset;
    public Rule[] rooms;
    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int prob = rooms[k].spawnProbability(i, j);
                        //int matchDoors = rooms[k].matchDoors(currentCell.status);

                        // obligatory
                        if(prob == 3 /*&& matchDoors == 1*/)
                        {
                            availableRooms.Add(k);
                            break;
                        } 

                        // priority
                        else if (prob == 2 /*&& matchDoors == 1*/)
                        {
                            availableRooms.Add(k);
                            availableRooms.Add(k);
                            availableRooms.Add(k);
                        }

                        // chance to spawn
                        else if (prob == 1 /*&& matchDoors == 1*/)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if(randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            randomRoom = 0;
                        }
                    }


                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += " " + i + "-" + j;

                }
            }
        }

    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k<1000)
        {
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            List<int> neighbors = new List<int>();
            List<int> direction = CheckNeighbors(currentCell, neighbors);

            // no direction left to go w/ current cell
            if (neighbors.Count == 0)
            {
                // end of path; done generating
                if (path.Count == 0)
                {
                    break;
                }

                // return through path to previous visited cell
                else
                {
                    currentCell = path.Pop();
                }
            }


            else
            {
                path.Push(currentCell);

                // pick cell to continue path from neighbors
                /* for (int i = 0; i < 4; i++){               
                    if (currentCell.status[i] == false)
                    {
                        neighbors.removeAt(direction);
                    }
                } */
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // figure out which direction new cell is in 
                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell, List<int> neighbors)
    {
        List<int> direction = new List<int>();

        //check up neighbor
        if (cell - size.x >= 0 && !board[(cell-size.x)].visited)
        {
            neighbors.Add((cell - size.x));
            // location in dir array tell which direction neighbor is in,
            // value in array is index of neighbor cell added to neighbor
            // direction[0] = neighbors.Count - 1;
        }

        //check down neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
            // direction[1] = neighbors.Count - 1;
        }

        //check right neighbor
        if ((cell+1) % size.x != 0 && !board[(cell +1)].visited)
        {
            neighbors.Add((cell +1));
            // direction[2] = neighbors.Count - 1;
        }

        //check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell -1));
            // direction[3] = neighbors.Count - 1;
        }

        return direction;
    }
}
