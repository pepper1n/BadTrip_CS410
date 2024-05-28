using UnityEngine;
using UnityEngine.AI;

public class ChaserLocomotion : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Calculate direction to the player while maintaining the same y-coordinate for the enemy
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // Keep the y coordinate the same to prevent looking up/down

        // Rotate the enemy to face the player
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToPlayer, Time.deltaTime * agent.angularSpeed);
        }

        // Move towards the player
        agent.SetDestination(player.position);
    }
}
