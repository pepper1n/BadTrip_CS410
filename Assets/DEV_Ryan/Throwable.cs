using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Throwable : MonoBehaviour
{
    private GameObject thrower;
    public GameObject puddlePrefab;

    void Start()
    {
        Destroy(gameObject, 5f);
        Physics.IgnoreCollision(GetComponent<Collider>(), thrower.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Floor") || other.gameObject.name.Contains("floor"))
        {
            Destroy(gameObject);
            Instantiate(puddlePrefab, transform.position, Quaternion.identity);
        }
    }
}
