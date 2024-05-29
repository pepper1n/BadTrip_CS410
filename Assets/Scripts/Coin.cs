using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Coin : MonoBehaviour
{
    private AudioSource coin;
    public PlayerLocomotion pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<PlayerLocomotion>();
        coin = GetComponent<AudioSource>();

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
            coin.Play();
            pl.gold++;
            // Destroy this game object
            Destroy(gameObject, coin.clip.length);
        }
    }
}
