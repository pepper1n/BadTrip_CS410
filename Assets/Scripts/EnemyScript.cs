using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Material material1;
    public Material material2;
    public float enemySpeed = 5;

    private float range;
    private int state = 0;
    public float timer = 5;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float shootFrequency = 1f;
    public float shootingRange = 10f;
    private float shootTimer;
    private float stateTimer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        range = 1.0f;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state == 0)
            {
                state = 1;
            }else
            {
                state = 0;
            }
        }
        if (state == 0)
        {
            this.GetComponent<MeshRenderer>().material = material1;

        }
        if(state == 1)
        {
            this.GetComponent<MeshRenderer>().material = material2;

        }
        Move();
        Shoot();
        
    }

    void Shoot()
    {
        if (state == 0)
        {
            shootTimer += Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) < shootingRange)
            {
                if (shootTimer >= shootFrequency)
                {
                    shootTimer = 0;
                    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                    Vector3 shootingDirection = (player.transform.position - transform.position).normalized;
                    bullet.GetComponent<Rigidbody>().velocity = shootingDirection * bulletSpeed;
                }

            }
        }
    }

    void Move()
    {
        if (state == 1)
        {
            Vector3 direction = (player.transform.position - transform.position);
            direction.Normalize();
            transform.position += direction * enemySpeed * Time.deltaTime;
        }
    }
}
