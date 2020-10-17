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
        if (state != MoveState.jumping)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(new Vector2(0, jumpStrength));
                state = MoveState.jumping;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Die();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.ResetLevel();
        }
    }

    void FixedUpdate()
    { 

        //Debug.Log(state);
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
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
        else if (collision.gameObject.tag == "Death")
        {
            state = MoveState.idle;
            Die();
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
        GameManager.Instance.PlayerDie();
        GameManager.Instance.Narrator.PlaySound(TestClip);
    }

    public void PauseMovement()
    {
        
    }
}
