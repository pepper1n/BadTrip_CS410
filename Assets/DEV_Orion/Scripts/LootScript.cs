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
        public GameObject EVMMMesh;
        public GameObject lootItem;

        void Start()
        {
            pickupRange = GetComponent<CapsuleCollider>();
            inputHandler = GameObject.Find("Player").GetComponent<InputHandler>();
            weaponEquip = GameObject.Find("Player").GetComponent<WeaponEquip>();
            float lootGen = Random.Range(0.0f, 1.0f);
            if (lootGen > 0.33f && .66f >= lootGen)
            {
                lootItem = weaponEquip.Sword;
                SwordMesh.SetActive(true);
            }
            else if (lootGen <= .33f)
            {
                lootItem = weaponEquip.Hammer;
                HammerMesh.SetActive(true);
            }
            else
            {
                lootItem = weaponEquip.EVMM;
                EVMMMesh.SetActive(true);
            }
        }

        void GiveWeapon()
        {
            if (inputHandler.pickupFlag && !weaponEquip.Weapon3)
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
