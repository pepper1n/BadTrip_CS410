using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Coin : MonoBehaviour
{
    private AudioSource coin;
    public PlayerLocomotion pl;
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<PlayerLocomotion>();
        coin = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if (!coin.isPlaying)
            {
                coin.Play();
                pl.gold += 50;
            }
            mesh.enabled = false;
            Destroy(gameObject, coin.clip.length);
        }
    }
}
