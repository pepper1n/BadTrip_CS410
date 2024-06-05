using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private TMP_Text textContent;
    public string moveTip;
    public string atkTip;
    public string interactTip;
    public string lookTip; 
    public float displayDuration = 5f;
    private float displayTimer;
    private Stack<string> tips;

    // Start is called before the first frame update
    void Start()
    {
        textContent = gameObject.GetComponent<TMP_Text>();
        textContent.text = "Good, you're awake. Time to get to work";
        tips = new Stack<string> (new[] {"Find the teleporter in this world and maybe you'll get out alive", atkTip, interactTip, moveTip, lookTip});
    }

    // Update is called once per frame
    void Update()
    {
        displayTimer += Time.deltaTime;

        // Check if the display duration has elapsed
        if (displayTimer >= displayDuration)
        {
            Debug.Log(tips.Count);
            if (tips.Count != 0 && tips != null) {
                textContent.text = tips.Pop();
            } else {
                textContent.text = "";
            }
            // Reset the timer for the next use
            displayTimer = 0f;
        }
    }
}
