using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using BT;

using Debug = UnityEngine.Debug;

public class StateFlipping : MonoBehaviour
{
    GameObject[] stateStructures;

    public bool isTrippy = true;
    public float trippyDuration = 5;
    public float fleshHitsNeeded = 5;
    public float timer = 0;
    public bool swapped = false;
    public bool canSwap = true;
    public float swapTimer = 0f;
    private HostileBehavior hb;

    Color trippyColor = new Color(1, 0, 1, 1);
    Color fleshColor = new Color(1, 0, 0, 1);

    private GameObject fleshAudio;
    private AudioSource[] trippyAudioSources;

    void Start()
    {
        hb = FindObjectOfType<HostileBehavior>();

    }
    void Update()
    {
        if (isTrippy)
        {
            timer += Time.deltaTime;
            if (timer >= trippyDuration)
            {
                timer = 0;
                //Swap();
            }
        }
        else
        {
            if (timer >= fleshHitsNeeded)
            {
                timer = 0;
                //Swap();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Swap();
        }
        if (swapped == true)
        {
            swapTimer += Time.deltaTime;
        }
        if (swapTimer >= 5)
        {
            unSwap();
        }
    }
    
    public void doSwap()
    {
        swapTimer = 0f;
        swapped = true;
        Swap();

    }
    public void unSwap()
    {
        Swap();
        swapTimer = 0f;
        swapped = false;

    }


    public void Swap()
    {
        isTrippy = !isTrippy;
        GameObject[] stateStructures = GameObject.FindGameObjectsWithTag("stateStructure");
        foreach (GameObject stateStructure in stateStructures)
        {
            if (stateStructure.name == "MusicSource")
            {
                musicSwap(stateStructure);
            }
            else 
            {
                Transform[] children = stateStructure.GetComponentsInChildren<Transform>(true);
                Transform activeChild = null;
                Transform inactiveChild = null;
                foreach (Transform child in children)
                {
                    if (child.CompareTag("trippyStructure") || child.CompareTag("fleshStructure"))
                    {
                        if (child.gameObject.activeSelf)
                        {
                            activeChild = child;
                        }
                        else
                        {
                            inactiveChild = child;
                        }
                    }
                }
                HostileBehavior hostileBehavior = stateStructure.GetComponent<HostileBehavior>();
                if (activeChild != null && inactiveChild != null)
                {
                    if (hostileBehavior != null)
                    {
                        Vector3 oldPosition = activeChild.position;
                        Quaternion oldRotation = activeChild.rotation;
                        inactiveChild.position = oldPosition;
                        inactiveChild.rotation = oldRotation;
                    }
                    activeChild.gameObject.SetActive(false);
                    inactiveChild.gameObject.SetActive(true);
                }
            }
    
        }

        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            light.color = isTrippy ? trippyColor : fleshColor;
        }
    }
    public void musicSwap(GameObject stateStructure)
    {
        fleshAudio = stateStructure.transform.GetChild(0).gameObject;
        trippyAudioSources = stateStructure.transform.GetChild(1).gameObject.GetComponentsInChildren<AudioSource>();

        bool isFighting = ActiveRoom.isFighting;

        if (isTrippy && !isFighting)
        {
            foreach (AudioSource trippyAudio in trippyAudioSources)
            {
                trippyAudio.volume = 0.5f;
            }
            fleshAudio.SetActive(false);

        }
        else if (!isTrippy && !isFighting)
        {
            foreach (AudioSource trippyAudio in trippyAudioSources)
            {
                trippyAudio.volume = 0f;
            }
            fleshAudio.SetActive(true);
        }
    }
}

