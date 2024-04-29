/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 0;
    public List<Transform> waypoints;
    private int waypointIndex;
    private float range;
    public int state = 0;
    public float timer = 5;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float shootFrequency = 1f;
    public float shootingRange = 10f;
    private float shootTimer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        range = 1.0f;

        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        
    }

    void Shoot()
    {
        shootTimer += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.transform.position) < shootingRange)
        {
            if(shootTimer >= shootFrequency)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                Vector3 shootingDirection = (player.transform.position - transform.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = shootingDirection * bulletSpeed;
            }

        }
    }

    void Move()
    {
        transform.LookAt(waypoints[waypointIndex]);
        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, waypoints[waypointIndex].position) < range)
        {
            waypointIndex++;
            if(waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
        }
    }
}*/
