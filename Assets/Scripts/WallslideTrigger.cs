using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallslideTrigger : MonoBehaviour
{
    [SerializeField] private GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (this.name == "LeftSide") { character.GetComponent<PlayerController>().WallTrigger(true); }
            if (this.name == "RightSide") { character.GetComponent<PlayerController>().WallTrigger(false); }
        }
    }
}
