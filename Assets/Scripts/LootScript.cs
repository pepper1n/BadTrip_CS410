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
        }

        void OnTriggerStay(Collider player)
        {
            if (inputHandler.pickupFlag)
            {
                weaponEquip.PickupWeapon(lootItem);
            }
        }
    }
}
