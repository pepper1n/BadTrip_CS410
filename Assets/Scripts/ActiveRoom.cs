using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class ActiveRoom : MonoBehaviour
{
    public AudioClip fightAudio;
    public GameObject doors;
    public GameObject spawners;
    public GameObject shopPrefab;
    private GameObject ambientMusic;
    private AudioSource audioSource;
    private bool triggered;
    List<GameObject> enemies = new List<GameObject>();
    float delta;
    float openRoomTimer;
    private GameObject enemy;
    private bool isTrippy;
    public GameObject teleporter;
    public Transform shopSpawn;
    public GameObject shop;



    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.Find("SlenderShop");
        ambientMusic = GameObject.Find("MusicSource");
        audioSource = GetComponent<AudioSource>();
        delta = Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // when player enters
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;

            // stop current music and start fight music
            ambientMusic.SetActive(false);
            audioSource.clip = fightAudio;
            audioSource.Play();

            // close doors
            doors.SetActive(true);

            // spawn enemies
            spawners.SetActive(true);

            //shop.SetActive(false);
        }

        // add enemies to list 
        if (other.gameObject.name.Contains("Enemy") || other.gameObject.name.Contains("Common"))
        {
            Debug.Log(other.gameObject.name);
            enemy = other.gameObject.transform.parent.gameObject;

            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                Debug.Log(other.gameObject.name + "added to list. Total: " + enemies.Count);
            }
        }


    }

    void OnTriggerStay(Collider other)
    {
        // wait for list to be updated
        openRoomTimer += delta;
        if (openRoomTimer >= 3f)
        {
            checkEnemyDefeat();
        }        
    }

    void checkEnemyDefeat()
    {
        // all enemies killed after spawn
        if (enemies.Count <= 0 && triggered)
        {
            // reset ambient music
            audioSource.Stop();

            ambientMusic.SetActive(true);

            isTrippy = GameObject.Find("Player").GetComponent<StateFlipping>().isTrippy;
            if (isTrippy)
            {
                // trippy music on
                ambientMusic.transform.GetChild(1).gameObject.SetActive(true);
                // dark music off
                ambientMusic.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (!isTrippy)
            {
                // dark music on
                ambientMusic.transform.GetChild(0).gameObject.SetActive(true);
                // trippy music off
                ambientMusic.transform.GetChild(1).gameObject.SetActive(false);
            }

            if (teleporter != null)
            {
                teleporter.SetActive(true);
            }

            // open doors
            doors.SetActive(false);
            shop.transform.position = shopSpawn.transform.position + new Vector3(0, 0, 2);
            //spawn shop
            //shop.SetActive(true);
            /*GameObject shop = Instantiate(shopPrefab, shopSpawn.position, Quaternion.identity);
            shop.SetActive(true);*/
        }     
    }


    public void enemyKill(GameObject currentEnemy)
    {
        // removes enemy from list on death
        if(enemies.Contains(currentEnemy))
        {
            enemies.Remove(currentEnemy);
            Debug.Log("Enemy removed from list. Total: " + enemies.Count);
        }
    }
}


