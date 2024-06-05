using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        Color color = image.color;
        color.a = 0;
        image.color = color;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
