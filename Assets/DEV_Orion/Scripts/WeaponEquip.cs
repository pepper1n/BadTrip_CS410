using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class WeaponEquip : MonoBehaviour
    {
        public GameObject Sword;
        public GameObject Hammer;
        public GameObject EVMM;

        public GameObject Weapon1 = null;
        public GameObject Weapon2 = null;
        public GameObject Weapon3 = null;
        public GameObject activeWeapon = null;
        private GameObject previousWeapon;
        public AudioClip drawSwordSound;
        public AudioClip drawHammerSound;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void PickupWeapon(GameObject newWeapon)
        {
            if (newWeapon != null)
            {
                if (!Weapon1)
                {
                    Weapon1 = newWeapon;
                    SwitchWeapon(Weapon1);
                }
                else if (!Weapon2)
                {
                    Weapon2 = newWeapon;
                    SwitchWeapon(Weapon2);
                }
                else if (!Weapon3)
                {
                    Weapon3 = newWeapon;
                    SwitchWeapon(Weapon3);
                }
                else
                {
                    //TODO
                }
            }

        }

        public void SwitchWeapon(GameObject switchedWeapon)
        {
            if (!switchedWeapon)
            {
                Debug.Log("No weapon to equip");
            }
            else
            {
            if (activeWeapon)
                {
                    previousWeapon = activeWeapon;
                }
                activeWeapon = switchedWeapon;
                if (previousWeapon)
                {
                    previousWeapon.SetActive(false);
                }
                activeWeapon.SetActive(true);
            }
            
            if (switchedWeapon == Sword)
            {
                audioSource.clip = drawSwordSound;
                audioSource.Play();
            }
            else if (switchedWeapon == Hammer)
            {
                audioSource.clip = drawHammerSound;
                audioSource.Play();
            }
        }

        public void DeEquip()
        {
            activeWeapon.SetActive(false);
        }

    }
}
