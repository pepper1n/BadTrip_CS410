using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    private float range;
    //private int state = 0; //commenting out for now to avoid 'unused variable' console messages
    public float timer = 5;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float shootFrequency = 1f;
    public float shootingRange = 10f;
    public float followRange = 20f;
    private float shootTimer;
    private float stateTimer;
    public GameObject player;
    public Vector3 direction;
    public float rotationSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }


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
            if (shootTimer >= shootFrequency)
            {
                shootTimer = 0;
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                bullet.SetActive(true);
                Vector3 shootingDirection = (player.transform.position - transform.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = shootingDirection * bulletSpeed;
            }

        }
        
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < followRange)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;


            Quaternion targetRotation = Quaternion.LookRotation(direction);


            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
