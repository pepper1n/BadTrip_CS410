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
        public GameObject SwordMesh;
        public GameObject HammerMesh;
        public GameObject lootItem;

        void Start()
        {
            pickupRange = GetComponent<CapsuleCollider>();
            inputHandler = GameObject.Find("Player").GetComponent<InputHandler>();
            weaponEquip = GameObject.Find("Player").GetComponent<WeaponEquip>();
            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                lootItem = weaponEquip.Sword;
                SwordMesh.SetActive(true);
            }
            else
            {
                lootItem = weaponEquip.Hammer;
                HammerMesh.SetActive(true);
            }
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
