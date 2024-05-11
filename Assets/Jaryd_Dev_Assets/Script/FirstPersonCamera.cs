using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Camera firstPerson;
    public StateFlipping sf;
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Reference to the player object

 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;



    }

    // Update is called once per frame
    void Update()
    {
        bool state = sf.isTrippy;
        firstPerson.enabled = !state;

        // Get horizontal mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Apply rotation to camera or player object
        transform.Rotate(Vector3.up * mouseX);

    }
}
