using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endLevelTeleport : MonoBehaviour
{
    private const string DEV_Orion = "DEV_Orion";
    public string sceneName = DEV_Orion;
    void Start()
    {
        //sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
            Destroy(gameObject);
        }
    }
}
