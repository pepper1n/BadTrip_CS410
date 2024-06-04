using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using BT;

public class ThrowerLocomotion : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private ThrowerAttack throwerAttack;
    private StateFlipping stateScript;
    private float followDistance;
    public Transform target;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        stateScript = player.GetComponent<StateFlipping>();
        agent = GetComponent<NavMeshAgent>();
        throwerAttack = GetComponent<ThrowerAttack>();
        followDistance = throwerAttack.range * 0.8f;
    }

    void Update()
    {
        if (stateScript.isTrippy)
        {
            Chase(player);
        }
        else
        {
            HostileBehavior[] hostileBehaviors = FindObjectsOfType<HostileBehavior>();
            List<HostileBehavior> nonRagedEnemies = hostileBehaviors
                .Where(hb => hb.transform != transform.parent.transform && !hb.isRaged)
                .ToList();

            if (nonRagedEnemies.Count > 0)
            {
                nonRagedEnemies.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position)
                    .CompareTo(Vector3.Distance(transform.position, b.transform.position)));

                target = nonRagedEnemies[0].transform;
                GameObject fleshStructureChild = null;
                foreach (Transform child in target)
                {
                    if (child.CompareTag("fleshStructure"))
                    {
                        fleshStructureChild = child.gameObject;
                        target = fleshStructureChild.transform;
                        break;
                    }
                }
                if (fleshStructureChild != null)
                {
                    Chase(target);
                }
            }
            else
            {
                Chase(player);
            }
        }
    }

    void Chase(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0;
        if (directionToTarget != Vector3.zero)
        {
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, Time.deltaTime * agent.angularSpeed);
        }
        Vector3 directionFromTarget = transform.position - target.position;
        directionFromTarget.y = 0;
        directionFromTarget.Normalize();
        Vector3 followPosition = target.position + directionFromTarget * followDistance;
        agent.SetDestination(followPosition);
    }
}

