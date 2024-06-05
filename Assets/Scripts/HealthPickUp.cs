using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Health : MonoBehaviour
{
    private AudioSource health;
    public PlayerManager pm;
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
        health = GetComponent<AudioSource>();
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
            if(!health.isPlaying)
            {
                health.Play();
                pm.currentHealth += 20;
                pm.shopHealth += 20;

            }
            mesh.enabled = false;
            Destroy(gameObject, health.clip.length);
        }
    }
}
