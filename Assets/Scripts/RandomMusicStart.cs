using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicStart : MonoBehaviour
{
        public AudioSource source;
     
        void Start()
        {
            source.time = Random.Range(0f, source.clip.length);
            source.Play();
        }


    // Update is called once per frame
    void Update()
    {
        
    }
}
