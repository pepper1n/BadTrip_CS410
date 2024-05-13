using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    public GameObject currentEnemy;
    public StateFlipping sf;
    public GameObject prefab1;
    public GameObject prefab2;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject == prefab2)
        {
            Debug.Log("test");
        }
        bool state = sf.isTrippy;
        if(!state && gameObject == prefab1)
        {
            Destroy(currentEnemy);
            currentEnemy = Instantiate(prefab2, transform.position, transform.rotation);
            currentEnemy.SetActive(true);

        }
        else if (state && gameObject == prefab2)
        {
            Destroy(currentEnemy);
            currentEnemy = Instantiate(prefab1, transform.position, transform.rotation);
            currentEnemy.SetActive(true);


        }
        
    }
}
