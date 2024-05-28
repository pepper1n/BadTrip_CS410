using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAttack : MonoBehaviour
{
    private Transform player;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float range = 5f;
    public float damage = 20f;
    public float attackDelay = 1f;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        attackTimer = 0;
    }

    void Update()
    {
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
        Vector3 directionToPlayer = player.position - transform.position;
        if (directionToPlayer.magnitude <= range && attackTimer <= 0)
        {
            Shoot();
            attackTimer = attackDelay;
        }
    }

    void Shoot()
    {
        Debug.Log("shoot!");
        attackTimer = attackDelay;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.SetActive(true);
        Vector3 shootingDirection = (player.transform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootingDirection * bulletSpeed;
    }
}
