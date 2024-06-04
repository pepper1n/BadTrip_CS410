using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using TMPro;



public class CharacterSheet : MonoBehaviour
{
    private GameObject UI;
    private GameObject player;
    private bool sheetOpen = false;

    public PlayerLocomotion pl;
    public PlayerManager pm;

    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pl = player.GetComponent<PlayerLocomotion>();
        pm = player.GetComponent<PlayerManager>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

    }

    // Update is called once per frame
    void Update()
    {
        updateCharacterSheet();
        if (Input.GetKeyDown(KeyCode.C))
        {
            openCharacterSheet();
            sheetOpen = true;
        }

        if((sheetOpen && Input.GetKeyDown(KeyCode.Escape)) )
        {
            closeCharacterSheet();
            sheetOpen = false;
        }


    }

    void updateCharacterSheet()
    {
        TextMeshProUGUI wealth = GameObject.Find("Wealth/Amount").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI healthUpgrade = GameObject.Find("Health/Upgrade Amount").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI speedUpgrade = GameObject.Find("Speed/Upgrade Amount").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI damageUpgrade = GameObject.Find("Damage/Upgrade Amount").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI jumpUpgrade = GameObject.Find("Jump/Upgrade Amount").GetComponent<TextMeshProUGUI>();
        wealth.text = pl.gold.ToString();
        healthUpgrade.text = "+ " + pm.shopHealth.ToString();
        damageUpgrade.text = "+ " + pl.shopDamage.ToString();
        speedUpgrade.text = "+ " + pl.shopSpeed.ToString();
        jumpUpgrade.text = "+ " + pl.shopJump.ToString();

    }
    void openCharacterSheet()
    {
     
        canvasGroup.alpha = 1;

        //wealth = GameObject.Find("Wealth/Amount").GetComponent<TextMeshProUGUI>();
        //wealth.text = pl.gold.ToString();
    }

    void closeCharacterSheet()
    {
        canvasGroup.alpha = 0;
        //UI.SetActive(false);

    }


}
