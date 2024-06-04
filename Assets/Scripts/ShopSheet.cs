using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using TMPro;

public class ShopSheet : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        //canvasGroup.alpha = 0;

    }

    // Update is called once per frame
    void Update()
    {
        buy();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    public void buy()
    {
        Debug.Log("I BOUGHT");
    }
}
