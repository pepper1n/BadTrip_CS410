using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class ChaserAttack : MonoBehaviour
{
    private Transform player;
    private PlayerManager playerManager;
    public float range = 0.5f;
    public float damage = 20f;
    public float attackDelay = 1f;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerManager = player.gameObject.GetComponent<PlayerManager>();
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
            playerManager.TakeDamage(damage);
            attackTimer = attackDelay;
        }
    }
}
