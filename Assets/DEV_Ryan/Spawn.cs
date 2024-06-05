using UnityEngine;

public class Spawn : MonoBehaviour
{
    GameObject player;
    Teleporter teleporter;
    public int nextLevel;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 newPosition = transform.position + new Vector3(0, 1, 0);
            teleporter = player.GetComponent<Teleporter>();
            player.transform.position = newPosition;
            teleporter.targetScene = nextLevel;
        }
        else
        {
            Debug.LogWarning("No GameObject with the tag 'Player' found in the scene.");
        }
    }
}
