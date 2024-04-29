using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public Light directionalLight;
    public Color newLightColor;
    public int state;

    private bool isLightChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            if (!isLightChanged)
            {
                // Change light color
                directionalLight.color = Color.red;
                isLightChanged = true;
            }
            else
            {
                // Reset light color to default or any other color
                directionalLight.color = Color.white; // Change this to reset to your desired default color
                isLightChanged = false;
            }
        }

    }
    public void OnButtonClick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            if (!isLightChanged)
            {
                // Change light color
                directionalLight.color = Color.red;
                isLightChanged = true;
            }
            else
            {
                // Reset light color to default or any other color
                directionalLight.color = Color.white; // Change this to reset to your desired default color
                isLightChanged = false;
            }
        }
    }
}
