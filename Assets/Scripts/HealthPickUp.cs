using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Health : MonoBehaviour
{
    private AudioSource health;
    public PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
        health = GetComponent<AudioSource>();

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

            }
            // Destroy this game object
            Destroy(gameObject, health.clip.length);
        }
    }
}
