using UnityEngine;
using UnityEngine.AI;

public class ChaserLocomotion : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private ChaserAttack chaserAttack;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        chaserAttack = GetComponent<ChaserAttack>();
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

        if (directionToPlayer.magnitude < (chaserAttack.range * 0.9f))
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }
}
