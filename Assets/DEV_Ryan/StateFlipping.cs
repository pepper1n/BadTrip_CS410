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
    public float fleshDuration = 5;
    public float timer = 0;

    Color trippyColor = new Color(1, 0, 1, 1);
    Color fleshColor = new Color(1, 0, 0, 1);

    void Update()
    {
        timer += Time.deltaTime;
        if (isTrippy)
        {
            if (timer >= trippyDuration)
            {
                timer -= trippyDuration;
                Swap();
            }
        }
        else
        {
            if (timer >= fleshDuration)
            {
                timer -= fleshDuration;
                Swap();
            }
        }
    }

    void Swap()
    {
        isTrippy = !isTrippy;
        GameObject[] stateStructures = GameObject.FindGameObjectsWithTag("stateStructure");
        foreach (GameObject stateStructure in stateStructures)
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
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            light.color = isTrippy ? trippyColor : fleshColor;
        }
    }
}

