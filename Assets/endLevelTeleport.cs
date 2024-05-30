using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BT;
public class EndLevelTeleport : MonoBehaviour
{
    public Transform spawnPoint;
    private bool isReloading = false;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
    }

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
        SceneManager.LoadScene(currentScene.buildIndex);

        yield return new WaitForSeconds(0.5f);

        if (PlayerManager.instance != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;
        }

        isReloading = false;
    }
}
