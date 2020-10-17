using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject deadPrefab;
    public GameObject narrator;
    public Rigidbody2D rb;
    public float speed;
    public float jumpStrength;
    public AudioClip TestClip;

    enum MoveState
    {
        jumping,
        running,
        idle,
        stopped
    };
    private MoveState state;

    // Start is called before the first frame update
    void Start()
    {
        state = MoveState.stopped;
    }

    private void Update()
    {
        if (state != MoveState.jumping && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpStrength));
            state = MoveState.jumping;
        }
        if (state != MoveState.jumping && Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jumpStrength));
            state = MoveState.jumping;
        }
        if (state != MoveState.jumping && Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, jumpStrength));
            state = MoveState.jumping;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Die();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            narrator.GetComponent<NarratorEngine>().ResetLevel();
        }
    }

    void FixedUpdate()
    { 

        //Debug.Log(state);
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
    }

    private void SetState(MoveState state)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log(collision.gameObject.name);
        if (state == MoveState.jumping && collision.gameObject.tag == "Ground")
        {
            state = MoveState.idle;
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {

        if (state == MoveState.jumping && collision.gameObject.tag == "Ground")
        {
            Debug.LogError(collision.gameObject.name);
            state = MoveState.idle;
        }
    }*/

    public void Die()
    {
        Instantiate(deadPrefab, transform.position, Quaternion.identity);
        narrator.GetComponent<NarratorEngine>().AddBody(transform.position);
        narrator.GetComponent<NarratorEngine>().PlaySound(TestClip);
    }

    public void PauseMovement()
    {
        
    }
}
