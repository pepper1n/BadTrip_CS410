using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using TMPro;

public class Shop : MonoBehaviour
{
    public float range = 3.0f;
    private GameObject player;
    private AudioSource[] audioSources;
    private GameObject music;
    private bool triggered = false;
    public bool isShopping = false;
    private float timer = 0;
    private float deathTimer = 0;
    private bool deathSound = false;
    private bool sliceSound = false;
    private bool pushed = false;
    public PlayerLocomotion pl;
    public PlayerManager pm;
    public ShopSheet ss;
    public FadeToBlack fade;
    private PlayerAttack playerAttackScript;
    bool dying = false;
    private GameObject deathCanvas;
    private CanvasGroup deathCanvasGroup;
    public GameObject shop;

    //Shop prices
    private float healthCost = 30f;
    private float speedCost = 60f;
    private float damageCost = 45f;
    private float attackSpeedCost = 40f;
    private float jumpCost = 10f;
    private float secretCost = 999f;
    private float enemyCost = 999f;
    private float allyCost = 999f;


    private TextMeshProUGUI wealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pl = player.GetComponent<PlayerLocomotion>();
        pm = player.GetComponent<PlayerManager>();
        music = GameObject.Find("MusicSource");
        fade = FindObjectOfType<FadeToBlack>();
        shop = GameObject.Find("Shop");
        shop.SetActive(true);
        setPrices();
        shop.SetActive(false);
        //ss = shop.GetComponent<ShopSheet>();
        //ss.canvasGroup.alpha = 0;
        audioSources = GetComponents<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //ss.canvasGroup.interactable = true;
                //ss.canvasGroup.alpha = 1;
                shop.SetActive(true);
                setPrices();
                wealth = GameObject.Find("ShopUI/Wealth/Amount").GetComponent<TextMeshProUGUI>();
                wealth.text = pl.gold.ToString();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isShopping = true;
                player.GetComponent<InputHandler>().enabled = false;

            }


            timer += Time.deltaTime;
        }else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (timer >= 5)
            {
                triggered = false;
            }
            timer = 0;
        }

        if(isShopping)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isShopping = false;
                //ss.canvasGroup.alpha = 0;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                player.GetComponent<InputHandler>().enabled = true;
                shop.SetActive(false);
            }

        }
        
        if (!triggered)
        {
            if (distance <= range)
            {
                if (Random.value < 0.5f)
                {
                    audioSources[0].Play();
                }else
                {
                    audioSources[1].Play();
                }
                triggered = true;
                // Add your logic here for when the player is within range
            }
        }

        if (dying)
        {
            
            triggered = true;
            deathTimer += Time.deltaTime;
        }

        if (deathTimer >= 1 && !pushed)
        {
            music.SetActive(false);
            Vector3 backwardDirection = -player.transform.forward;
            player.GetComponent<Rigidbody>().AddForce(backwardDirection * 100, ForceMode.Impulse);
            audioSources[5].Play();
            pushed = true;
        }

        if (deathTimer >= 2)
        {
            player.GetComponent<InputHandler>().enabled = false;
        }

        if (deathTimer >= 2.5)
        {
            Vector3 behindPosition = player.transform.position - player.transform.forward * 1;
            transform.position = behindPosition;
            transform.rotation = player.transform.rotation;
        }

        if (deathTimer >= 3 && !deathSound)
        {
            audioSources[3].Play();
            deathSound = true;
        }
        if (deathTimer >= 5.5 && !sliceSound)
        {
            float fadeDuration = 2.0f;
            float elapsedTime = 0f;
            audioSources[4].Play();
            sliceSound = true;
            pm.currentHealth = 0;
            fade.Fade();

        }

    }



    void setPrices()
    {
        TextMeshProUGUI healthPrice = GameObject.Find("Health/Buy/Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI speedPrice = GameObject.Find("Speed/Buy/Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI damagePrice = GameObject.Find("Damage/Buy/Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI secretPrice = GameObject.Find("Secret Sight/Buy/Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI enemyPrice = GameObject.Find("Enemy Sight/Buy/Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI allyPrice = GameObject.Find("Ally/Buy/Price").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI attackSpeedPrice = GameObject.Find("Attack Speed/Buy/Price").GetComponent<TextMeshProUGUI>();
        healthPrice.text = healthCost.ToString();
        damagePrice.text = damageCost.ToString();
        speedPrice.text = speedCost.ToString();
        secretPrice.text = secretCost.ToString();
        enemyPrice.text = enemyCost.ToString();
        allyPrice.text = allyCost.ToString();
        attackSpeedPrice.text = attackSpeedCost.ToString();



    }

    public void buyHealth()
    {
        if (pl.gold >= healthCost)
        {
            audioSources[2].Play();
            pm.shopHealth += 10;
            pm.currentHealth += 10;
            pl.gold -= healthCost;
            wealth.text = pl.gold.ToString();
        }
    }

    public void buySpeed()
    {   
        if(pl.gold >= speedCost)
        {
            audioSources[2].Play();
            pl.shopSpeed++;
            pl.gold -= speedCost;
            wealth.text = pl.gold.ToString();
        }

    }

    public void buyDamage()
    {
        if (pl.gold >= damageCost)
        {
            audioSources[2].Play();
            pl.shopDamage += 5;
            pl.gold -= damageCost;
            wealth.text = pl.gold.ToString();
        }


    }
    public void buySecretSight()
    {
        if (pl.gold >= secretCost)
        {
            audioSources[2].Play();
            pl.shopSpeed++;
            pl.gold -= secretCost;
            wealth.text = pl.gold.ToString();
        }


    }
    public void buyEnemySight()
    {
        if (pl.gold >= enemyCost)
        {
            audioSources[2].Play();
            pl.shopSpeed++;
            pl.gold -= enemyCost;
            wealth.text = pl.gold.ToString();
        }


    }
    public void buyAlly()
    {
        if (pl.gold >= allyCost)
        {
            audioSources[2].Play();
            pl.shopSpeed++;
            pl.gold -= allyCost;
            wealth.text = pl.gold.ToString();
        }


    }
    public void buyAttackSpeed()
    {
        if (pl.gold >= attackSpeedCost)
        {
            audioSources[2].Play();
            pl.shopSpeed++;
            pl.gold -= attackSpeedCost;
            wealth.text = pl.gold.ToString();
        }
    }

    public void buyJump()
    {
        if (pl.gold >= jumpCost)
        {
            audioSources[2].Play();
            pl.shopJump++;
            pl.gold -= jumpCost;
            wealth.text = pl.gold.ToString();
        }
    }

    public void dead()
    {
        dying = true;


    }
}
