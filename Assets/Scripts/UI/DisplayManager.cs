﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public Text displayText;
    public float displayTime;
    public float fadeTime;

    private IEnumerator fadeAlpha;

    private static DisplayManager displayManager;

    public static DisplayManager Instance()
    {
        if (!displayManager)
        {
            displayManager = FindObjectOfType(typeof(DisplayManager)) as DisplayManager;
            //if (!displayManager)
            //    Debug.Log("There need to be at least one active displayManager script on a GameObject in your scene.");
        }

        return displayManager;
    }

    void start()
    {
        //displayText = GameObject.Find("Display Text").GetComponent<Text>();
    }

    public void DisplayMessage(string message)
    {
        if (displayText != null) {
            displayText.text = message;
            SetAlpha();
        }
    }

    void SetAlpha()
    {
        if(fadeAlpha != null)
        {
            StopCoroutine(fadeAlpha);
        }
        fadeAlpha = FadeAlpha();
        StartCoroutine(fadeAlpha);
    }

    IEnumerator FadeAlpha()
    {
        Color resetColor = displayText.color;
        resetColor.a = 1;
        displayText.color = resetColor;

        yield return new WaitForSeconds(displayTime);

        while(displayText.color.a > 0)
        {
            Color displayColor = displayText.color;
            displayColor.a -= Time.deltaTime / fadeTime;
            displayText.color = displayColor;
            yield return null;
        }
        yield return null;
    }
}
