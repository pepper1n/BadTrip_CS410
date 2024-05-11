using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class StateFlipping : MonoBehaviour
{
    public Material trippyFloor;
    public Material trippyWall;
    public Material fleshFloor;
    public Material fleshWall;

    public bool isTrippy = true;

    Color trippyColor = new Color(1, 0, 1, 1);
    Color fleshColor = new Color(1, 0, 0, 1);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isTrippy = !isTrippy;
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            foreach (Renderer rend in renderers)
            {
                MeshRenderer meshRenderer = rend.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    if (meshRenderer.material.name == "M_TrippyFloor (Instance)" || meshRenderer.material.name == "M_FleshFloor (Instance)")
                    {
                        meshRenderer.material = isTrippy ? trippyFloor : fleshFloor;
                    }
                    if (meshRenderer.material.name == "M_TrippyWall (Instance)" || meshRenderer.material.name == "M_FleshWall (Instance)")
                    {
                        meshRenderer.material = isTrippy ? trippyWall : fleshWall;
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
}

