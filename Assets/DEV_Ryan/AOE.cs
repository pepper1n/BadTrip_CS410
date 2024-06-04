using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BT;

public class AOE : MonoBehaviour
{
    public float slowDuration;
    public float slowIntensity;
    public float rageDuration;
    public float rageIntensity;

    private GameObject player;
    private PlayerLocomotion playerLocomotion;
    private StateFlipping stateScript;
    private bool isTrippy;

    private HashSet<GameObject> objectsInTrigger = new HashSet<GameObject>();

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerLocomotion = player.GetComponent<PlayerLocomotion>();
        stateScript = player.GetComponent<StateFlipping>();
    }

    void Update()
    {
        isTrippy = stateScript.isTrippy;
    }

    void OnTriggerEnter(Collider other)
    {
        objectsInTrigger.Add(other.gameObject);

        if (other.gameObject == player && isTrippy)
        {
            playerLocomotion.ApplySlow(slowIntensity);
            playerLocomotion.CancelSlowRemoval();
        }

        else if (!isTrippy)
        {
            HostileBehavior hostile = other.GetComponentInParent<HostileBehavior>();
            if (hostile != null)
            {
                hostile.ApplyRage(rageIntensity);
                hostile.CancelRageRemoval();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        objectsInTrigger.Remove(other.gameObject);

        if (other.gameObject == player)
        {
            playerLocomotion.DelaySlowRemoval(slowDuration, slowIntensity);
        }
        else
        {
            HostileBehavior hostile = other.GetComponentInParent<HostileBehavior>();
            if (hostile != null)
            {
                hostile.DelayRageRemoval(rageDuration, rageIntensity);
            }
        }
    }

    void OnDisable()
    {
        foreach (GameObject obj in objectsInTrigger)
        {
            Collider other = obj.GetComponent<Collider>();

            if (other != null)
            {
                if (other.gameObject == player)
                {
                    playerLocomotion.DelaySlowRemoval(slowDuration, slowIntensity);
                }
                else
                {
                    HostileBehavior hostile = other.GetComponentInParent<HostileBehavior>();
                    if (hostile != null)
                    {
                        hostile.DelayRageRemoval(rageDuration, rageIntensity);
                    }
                }
            }
        }

        objectsInTrigger.Clear();
    }
}
