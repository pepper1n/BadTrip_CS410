using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class WeaponEquip : MonoBehaviour
    {
        public GameObject Weapon1 = null;
        public GameObject Weapon2 = null;
        public GameObject Weapon3 = null;
        private GameObject activeWeapon;
        private GameObject previousWeapon = null;

        public void PickupWeapon(GameObject newWeapon)
        {
            if (newWeapon != null)
            {
                if (Weapon1 == null)
                {
                    Weapon1 = newWeapon;
                    SwitchWeapon(Weapon1);
                }
                else if (Weapon2 == null)
                {
                    Weapon2 = newWeapon;
                    SwitchWeapon(Weapon2);
                }
                else if (Weapon3 == null)
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
            if (switchedWeapon == null)
            {
                Debug.Log("No weapon to equip");
            }
            else
            {
                previousWeapon = activeWeapon;
                activeWeapon = switchedWeapon;
                previousWeapon.SetActive(false);
                activeWeapon.SetActive(true);
            }
        }

        void DeEquip()
        {
            activeWeapon.SetActive(false);
        }

    }
}