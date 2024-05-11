using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sprite;
    public StateFlipping sf;
    public float moveSpeed = 5f;
    private Vector3 move;
    Rigidbody rb; 
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        bool state = sf.isTrippy;
        sprite.enabled = state;

        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");

        move.Normalize();

        rb.velocity = move * moveSpeed;

    }
}
