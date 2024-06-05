using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BT
{
    public class HUDScript : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        public GameObject Reticle;
        private Shop shop;
        public Slider healthBar;
        public Slider trippyBar;
        public Slider fleshBar;

        private GameObject player;
        private InputHandler IH;
        private PlayerManager playerManager;
        private StateFlipping stateScript;

        void Start()
        {
            shop = FindObjectOfType<Shop>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            player = GameObject.Find("Player");
            stateScript = player.GetComponent<StateFlipping>();
            IH = player.GetComponent<InputHandler>();
            playerManager = player.GetComponent<PlayerManager>();
            Reticle.SetActive(false);
        }

        void Update()
        {
            if(shop.isShopping == true)
            {
                canvasGroup.alpha = 0;
            }

            if(shop.isShopping == false)
            {
                canvasGroup.alpha = 1;
            }
            healthBar.maxValue = playerManager.maxHealth;
            healthBar.value = playerManager.currentHealth;

            if (IH.targetFlag)
            {
                Reticle.SetActive(true);
            }
            else
            {
                Reticle.SetActive(false);
            }

            if (stateScript.isTrippy)
            {
                trippyBar.maxValue = stateScript.trippyDuration;
                trippyBar.value = stateScript.timer;
            }
            else
            {
                fleshBar.maxValue = stateScript.fleshHitsNeeded;
                fleshBar.value = stateScript.timer;
            }
        }
    }
}
