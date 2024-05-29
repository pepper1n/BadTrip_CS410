using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image blackScreen;
    public float fadeDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade()
    {
        float elapsedTime = 0f;
        Color color = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreen.color = color;
        }

        color.a = 1f;
        blackScreen.color = color;
    }
}
