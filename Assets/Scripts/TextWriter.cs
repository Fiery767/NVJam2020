using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{

    private Text uiText;
    private string textMessage;
    private float timePerChar;
    private float timer;
    private int textIndex;
    public void AddWriter(Text uiText, string textMessage, float timePerChar)
    {
        this.uiText = uiText;
        this.textMessage = textMessage;
        this.timePerChar = timePerChar;
        this.textIndex = 0;
    }

    private void Update()
    {

        if (uiText != null)
        {
            // decrement the timer to move on to the next character
            timer -= Time.deltaTime;
            if (timer <= 0.0f )
            {
                // display the next character in the string 
                timer += timePerChar;
                textIndex++;
                uiText.text = textMessage.Substring(0, textIndex);

                // return if we've ran out of characters to display
                if (textIndex >= textMessage.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }


    }

}
