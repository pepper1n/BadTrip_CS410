using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEstate : MonoBehaviour
{
    public float areaDuration;

    private StateFlipping stateScript;
    private GameObject player;
    private bool isTrippy;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        stateScript = player.GetComponent<StateFlipping>();
        Destroy(gameObject, areaDuration);
    }

    void Update()
    {
        isTrippy = stateScript.isTrippy;
        UpdateStructureStates();
    }

    void UpdateStructureStates()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("fleshStructure"))
            {
                child.gameObject.SetActive(!isTrippy);
            }
            else if (child.CompareTag("trippyStructure"))
            {
                child.gameObject.SetActive(isTrippy);
            }
        }
    }
}
