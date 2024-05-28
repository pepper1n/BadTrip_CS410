using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    private GameObject shooter;
    private float damage;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shooter)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    public void SetShooter(GameObject shooterObject)
    {
        shooter = shooterObject;
    }
}
