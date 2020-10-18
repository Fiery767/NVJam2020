using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public AudioClip audioClip;
    private bool isPlayed = false;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isPlayed && collision.GetComponent<PlayerController>())
        {
            isPlayed = true;
            GameManager.Instance.Narrator.PlaySound(audioClip);
        }
    }
}
