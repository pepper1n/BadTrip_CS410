using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Camera topDown;
    public StateFlipping sf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool state = sf.isTrippy;
        topDown.enabled = state;
        Debug.Log(!state);


    }
}
