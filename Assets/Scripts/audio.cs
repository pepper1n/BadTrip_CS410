using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class audio : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // If the audio is playing, stop it; otherwise, play it
            if (audioSource.mute == false)
            {
                audioSource.mute = true;
            }
            else
            {
                audioSource.mute = false; 
            }
        }
    }
}
