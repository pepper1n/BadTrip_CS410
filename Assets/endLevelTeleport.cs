using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTeleport : MonoBehaviour
{
    private GameObject spawnPoint;
    private GameObject player;

    private bool isReloading = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Restarting");
        SceneManager.LoadScene(currentScene.buildIndex);

        yield return null;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player");
        spawnPoint = GameObject.FindWithTag("spawnPoint");
        TeleportPlayer();
    }

    void TeleportPlayer()
    {
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;

        isReloading = false;
    }
}
