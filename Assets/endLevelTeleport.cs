using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BT;
public class EndLevelTeleport : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitTillRestart());
        }
    }

    IEnumerator WaitTillRestart()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
/*public class EndLevelTeleport : MonoBehaviour
{
    private bool isReloading = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isReloading)
        {
            isReloading = true;
            StartCoroutine(WaitTillRestart());
        }
    }

    IEnumerator WaitTillRestart()
    {
        yield return new WaitForSeconds(0.5f);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

        yield return null;

        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.transform.position = PlayerManager.instance.startingPosition;
        }

        if (CameraHandler.singleton != null)
        {
            CameraHandler.singleton.ResetCamera();
        }

        isReloading = false;
    }
}*/
