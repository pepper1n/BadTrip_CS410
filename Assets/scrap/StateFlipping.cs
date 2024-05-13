using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class StateFlipping : MonoBehaviour
{
    GameObject[] stateStructures;

    public bool isTrippy = true;

    Color trippyColor = new Color(1, 0, 1, 1);
    Color fleshColor = new Color(1, 0, 0, 1);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isTrippy = !isTrippy;
            GameObject[] stateStructures = GameObject.FindGameObjectsWithTag("stateStructure");
            foreach (GameObject stateStructure in stateStructures)
            {
                Transform[] children = stateStructure.GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {
                    if (child.CompareTag("trippyStructure") || child.CompareTag("fleshStructure"))
                    {
                        child.gameObject.SetActive(!child.gameObject.activeSelf);
                    }
                }
            }
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                light.color = isTrippy ? trippyColor : fleshColor;
            }
        }
    }

    void ToggleActiveState(GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}

