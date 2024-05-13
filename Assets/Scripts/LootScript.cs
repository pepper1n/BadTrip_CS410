using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class LootScript : MonoBehaviour
    {
        public InputHandler inputHandler;
        public WeaponEquip weaponEquip;
        public CapsuleCollider pickupRange;
        public GameObject lootItem;

        void Start()
        {
            pickupRange = GetComponent<CapsuleCollider>();
            inputHandler = GameObject.Find("Player").GetComponent<InputHandler>();
            weaponEquip = GameObject.Find("Player").GetComponent<WeaponEquip>();
            lootItem = weaponEquip.Sword;
        }

        void GiveWeapon()
        {
            if (inputHandler.pickupFlag && weaponEquip.Weapon3 == null)
            {
                weaponEquip.PickupWeapon(lootItem);
                Destroy(gameObject);
            }
        }

        void OnTriggerStay(Collider player)
        {
            GiveWeapon();
        }
    }
}
