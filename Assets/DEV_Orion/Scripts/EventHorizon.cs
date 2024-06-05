using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class EventHorizon : MonoBehaviour
    {
        public GameObject player;
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public Camera camera;
        private InputHandler inputHandler;
        public AudioSource cast;

        public float damage = 10f;
        public float attackDelay = .5f;
        public float attackTimer = .5f;

        //private Shop shopScript;

        //private AudioSource audioSource;
        //public AudioClip chainsawSounds;
        // Start is called before the first frame update
        void Start()
        {
            inputHandler = player.GetComponent<InputHandler>();
            //audioSource = GetComponent<AudioSource>();
            //shopScript = FindObjectOfType<Shop>();
        }

        // Update is called once per frame
        void Update()
        {
            if (attackTimer < attackDelay)
            {
                attackTimer += Time.deltaTime;
            }
            if (inputHandler.punchFlag)
            {
                Shoot();
            }
        }


        private void Shoot()
        {
            if (attackTimer >= attackDelay)
            {
                if(!cast.isPlaying)
                {
                    cast.Play();
                }
                int layerMask = 1 << 10;
                layerMask = ~layerMask;
                Vector3 ScreenCentreCoordinates = new Vector3(0.5f, 0.5f, 0f);
                //Ray ray = camera.ViewportPointToRay(ScreenCentreCoordinates);
                Ray ray = new Ray(camera.transform.position, camera.transform.forward);
                RaycastHit hit;
                Vector3 projectileDestination;

                if (Physics.Raycast(ray, out hit, 2000f, layerMask))
                {
                    projectileDestination = hit.point;
                }
                else
                {
                    projectileDestination = ray.GetPoint(1000f);
                }
                Debug.Log(projectileDestination);
                Debug.DrawLine(camera.transform.position, projectileDestination, Color.red, 10f, false);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
                bullet.SetActive(true);
                //bullet.transform.rotation = Quaternion.LookRotation(Vector3.Normalize(projectileDestination));
                bullet.transform.LookAt(projectileDestination);
                bullet.GetComponent<EHProjectile>().Travel();
                attackTimer = 0f;
                Destroy(bullet, 1.0f);

            }
        }
    }
}
