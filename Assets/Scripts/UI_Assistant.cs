using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text messageText;

    private void Awake() {

        messageText = transform.Find("Message").Find("messageText").GetComponent<Text>();
    }

    private void Start()
    {
        textWriter.AddWriter(messageText, "Display Message Here", 0.2f);
    }
}
