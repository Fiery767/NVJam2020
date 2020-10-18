using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorEngine : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Clip;
    List<GameObject> DeadBodies = new List<GameObject>();
    bool Reset = false;
    int CurrentBody = 0;
    public float Speed;
    public float Timer;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPlayer(PlayerController controler)
    {
        player = controler;
    }

    // Update is called once per frame
    void Update()
    {
        if(Reset)
        {
            if (CurrentBody > DeadBodies.Count - 1)
            {
                Reset = false;
                CurrentBody = 0;
                GameManager.Instance.ClearAllBodies();
            }
            else
            {
                if (Vector3.Distance(transform.position, DeadBodies[CurrentBody].transform.position) > 0.0001f)
                {
                    Timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, DeadBodies[CurrentBody].transform.position, Timer * (Speed / 100f));
                }
                else
                {
                    DeadBodies[CurrentBody].gameObject.SetActive(false);
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
            if(Source.isPlaying)
            {
                Source.Stop();
            }
            Source.clip = MyClip;
            Source.Play();
        }
    }

    public void ClearBodies()
    {
        Reset = true;
        DeadBodies = GameManager.Instance.GetDeadBodies();
    }
}
