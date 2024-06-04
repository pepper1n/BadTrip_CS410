using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] pickups;

    public Transform enemySpawnPoint;

    public Vector3 spawnRotation;
    void Start()
    {
        Vector3 smaller = new Vector3(0.3f, 0.3f, 0.3f);
        spawnRotation = new Vector3(-90, 0, 0);
        Quaternion rotation = Quaternion.Euler(spawnRotation);
        for(int i = 0; i < 2; i++)
        {
            Debug.Log(pickups.Length);
            GameObject pickupsPrefab = pickups[Random.Range(0, pickups.Length)];

            GameObject pickup = Instantiate(pickupsPrefab, transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), rotation);

            pickup.SetActive(true);
            pickup.transform.localScale = smaller;

        }
        for (int i = 0; i < 2; i++)
        {
           

            GameObject pickupsPrefab = pickups[Random.Range(0, pickups.Length)];

            GameObject pickup = Instantiate(pickupsPrefab, transform.position + new Vector3(Random.Range(-14, 14), 0, Random.Range(-14, 14)), rotation);
            
            pickup.SetActive(true);
            pickup.transform.localScale = smaller;
        }
    }
}
