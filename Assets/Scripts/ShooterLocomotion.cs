using UnityEngine;
using UnityEngine.AI;

public class ShooterLocomotion : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    
    public float followDistance = 5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToPlayer, Time.deltaTime * agent.angularSpeed);
        }

        Vector3 directionFromPlayer = transform.position - player.position;
        directionFromPlayer.y = 0;
        directionFromPlayer.Normalize();
        Vector3 followPosition = player.position + directionFromPlayer * followDistance;
        agent.SetDestination(followPosition);
    }
}

