using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class DamagePickUp : MonoBehaviour
{
    private AudioSource woosh;
    public PlayerLocomotion pm;
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerLocomotion>();
        woosh = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with has the tag "Player"
        if (other.name == "Player")
        {
            if (!woosh.isPlaying)
            {
                woosh.Play();
                pm.shopDamage += 5;

            }
            mesh.enabled = false;
            // Destroy this game object
            Destroy(gameObject, woosh.clip.length);
        }
    }
}
