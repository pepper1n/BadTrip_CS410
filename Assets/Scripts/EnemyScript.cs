using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Material material1;
    public Material material2;
    public float enemySpeed = 5;


    private int state = 0;
    public float timer = 5;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float shootFrequency = 1f;
    public float shootingRange = 10f;
    private float shootTimer;
    private float stateTimer;
    private GameObject player;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        direction = (player.transform.position - transform.position);
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
     
        Move();

        
    }

    void Shoot()
    {
     /*   if (state == 0)
        {
            shootTimer += Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) < shootingRange)
            {
                if (shootTimer >= shootFrequency)
                {
                    shootTimer = 0;
                    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                    bullet.SetActive(true);
                    Vector3 shootingDirection = (player.transform.position - transform.position).normalized;
                    bullet.GetComponent<Rigidbody>().velocity = shootingDirection * bulletSpeed;
                }

            }
        }*/
    }

    void Move()
    {
      
        direction.Normalize();
        transform.position += direction * enemySpeed * Time.deltaTime;

    }
}
