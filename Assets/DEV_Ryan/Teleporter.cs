using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public int targetScene;
    private GameObject spawn;
    private Spawn spawnScript;

    void Update()
    {
        spawn = GameObject.FindGameObjectWithTag("spawnPoint");
        spawnScript = spawn.GetComponent<Spawn>();
        targetScene = spawnScript.nextLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelExit")
        {
            SceneManager.LoadScene(targetScene);
            Debug.Log($"Loaded Scene: {targetScene}");
        }
    }
}
