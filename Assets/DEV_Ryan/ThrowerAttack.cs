using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerAttack : MonoBehaviour
{
    private Transform playerTarget;
    private Transform player;
    private StateFlipping stateScript;
    private ThrowerLocomotion throwerLocomotion;
    private Vector3 directionToTarget;
    public GameObject throwablePrefab;
    public Transform throwableSpawnPoint;
    public float range;
    public float throwAngle;
    public float attackDelay;
    private float attackTimer;

    void Start()
    {
        playerTarget = GameObject.FindWithTag("playerTarget").transform;
        attackTimer = attackDelay;
        player = GameObject.FindWithTag("Player").transform;
        stateScript = player.GetComponent<StateFlipping>();
    }

    void Update()
    {
        throwerLocomotion = GetComponent<ThrowerLocomotion>();
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (stateScript.isTrippy)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerTarget.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                directionToTarget = hit.point - throwableSpawnPoint.position;
            }
            else
            {
                directionToTarget = playerTarget.position - throwableSpawnPoint.position;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(throwerLocomotion.target.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                directionToTarget = hit.point - throwableSpawnPoint.position;
            }
            else
            {
                directionToTarget = throwerLocomotion.target.position - throwableSpawnPoint.position;
            }
        }
        if (directionToTarget.magnitude <= range && attackTimer <= 0)
        {
            Throw();
            attackTimer = attackDelay;
        }
    }

    void Throw()
    {
        attackTimer = attackDelay;
        GameObject throwable = Instantiate(throwablePrefab, throwableSpawnPoint.position, Quaternion.identity);
        throwable.SetActive(true);

        float h = directionToTarget.y;
        directionToTarget.y = 0;
        float distance = directionToTarget.magnitude;
        float a = throwAngle * Mathf.Deg2Rad;
        directionToTarget.y = distance * Mathf.Tan(a);
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));

        Vector3 velocityVector = velocity * directionToTarget.normalized;
        Rigidbody rb = throwable.GetComponent<Rigidbody>();
        rb.velocity = velocityVector;
        Throwable throwableScript = throwable.GetComponent<Throwable>();
    }
}
