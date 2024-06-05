using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image blackScreen;
    public float fadeDuration = 1f;

    public void Fade()
    {
        
        Color color = blackScreen.color;
        blackScreen.color = color;
        color.a = 1f;
        blackScreen.color = color;

    }

    public void Unfade()
    {
        Color color = blackScreen.color;
        blackScreen.color = color;
        color.a = 0f;
        blackScreen.color = color;

    }
}
