using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls; //up, down, right, left
    public GameObject[] doors;

    public void UpdateRoom(bool[] status) 
    {
        for (int i = 0; i < status.Length; i++) 
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
