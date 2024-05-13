using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed = 5;
    public Vector3 direction;
    public float rotationSpeed = 5.0f;
    public float rotateRange = 40f;
    public float followRange = 20f;

    public ParticleSystem breath;
    public float breathTimer = 0;
    bool isBreathing = false;
    // Start is called before the first frame update
    void Start()
    {

        breath = GetComponentInChildren<ParticleSystem>();
        player = GameObject.FindWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        direction = (player.transform.position - transform.position);
        Move();
        if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            attack();
            breathTimer += Time.deltaTime;

        }
        if(isBreathing && breathTimer >= 2)
        {
            stopAttack();
            breathTimer = 0;
        }

    }

    void attack()
    {
        if (!isBreathing)
        {
            breath.Play();
            isBreathing = true;
        }

    }

    void stopAttack()
    {
        breath.Stop();
        isBreathing = false;

    }

    void Move()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < rotateRange)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;


            Quaternion targetRotation = Quaternion.LookRotation(direction);


            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < followRange && Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            direction.Normalize();
            transform.position += direction * enemySpeed * Time.deltaTime;
        }
    }
}
