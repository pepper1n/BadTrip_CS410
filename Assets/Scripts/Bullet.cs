using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public StateFlipping sf;
    //public GameObject player;
    public Vector3 direction;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        bool state = sf.isTrippy;
        if (state)
        {
            timer += Time.deltaTime;
        }
        
        if(timer >= 2f)
        {
            Destroy(this.gameObject, 0f);
        }
      
        if(this.GetComponent<Rigidbody>().velocity != (this.GetComponent<Rigidbody>().velocity * 0))
        {
            direction = this.GetComponent<Rigidbody>().velocity;
        }

        if (!state)
        {
            this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity * 0;
        }
        if(state)
        {
            this.GetComponent<Rigidbody>().velocity = direction;
        }
        

    }

    void swap()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
