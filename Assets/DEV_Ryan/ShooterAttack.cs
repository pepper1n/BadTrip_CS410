using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAttack : MonoBehaviour
{
    private Transform playerTarget;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public float range;
    public float damage;
    public float attackDelay;
    private float attackTimer;

    void Start()
    {
        playerTarget = GameObject.FindWithTag("playerTarget").transform;
        attackTimer = attackDelay;
    }

    void Update()
    {
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
        Vector3 directionToPlayer = playerTarget.position - transform.position;
        if (directionToPlayer.magnitude <= range && attackTimer <= 0)
        {
            Shoot();
            attackTimer = attackDelay;
        }
    }

    void Shoot()
    {
        attackTimer = attackDelay;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.SetActive(true);
        Vector3 shootingDirection = (playerTarget.position - bulletSpawnPoint.position).normalized;
        bullet.transform.rotation = Quaternion.LookRotation(shootingDirection);
        bullet.GetComponent<Rigidbody>().velocity = shootingDirection * bulletSpeed;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(damage);
        bulletScript.SetShooter(gameObject);
    }
}
