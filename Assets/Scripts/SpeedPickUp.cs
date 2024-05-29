using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class SpeedPickUp : MonoBehaviour
{
    private AudioSource woosh;
    public PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
        woosh = GetComponent<AudioSource>();

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
            woosh.Play();
            pm.speedBoost();
            // Destroy this game object
            Destroy(gameObject, woosh.clip.length);
        }
    }
}

