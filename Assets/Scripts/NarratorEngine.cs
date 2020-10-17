using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorEngine : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Clip;
    List<Vector3> BodyPos = new List<Vector3>();
    bool Reset = false;
    int CurrentBody = 0;
    public float Speed;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Reset)
        {
            if (CurrentBody > BodyPos.Count - 1)
            {
                Reset = false;
                CurrentBody = 0;
            }
            else
            {
                if (Mathf.Abs(transform.position.x - BodyPos[CurrentBody].x) >= .05 && Mathf.Abs(transform.position.y - BodyPos[CurrentBody].y) >= .05)
                {
                    Timer += Time.deltaTime;
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, BodyPos[CurrentBody].x, Timer*(Speed/100)), Mathf.Lerp(transform.position.y, BodyPos[CurrentBody].y, Timer * (Speed / 100)), transform.position.x);
                }
                else
                {
                    CurrentBody++;
                    Timer = 0;
                }
            }
        }
    }

    public void PlaySound(AudioClip MyClip)
    {
        Source.enabled = true;
        if (Source.isActiveAndEnabled)
        {
            Source.clip = MyClip;
            Source.Play();
        }
    }

    public void AddBody(Vector3 v)
    {
        BodyPos.Add(v);
    }

    public void ResetLevel()
    {
        foreach (Vector3 v in BodyPos)
        {
            Debug.Log(v);
        }
        Reset = true;
    }
}
