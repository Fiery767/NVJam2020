using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpStrength;
    public AudioClip TestClip;

    private float WallJumpFacingThreshold = 0.9f;
    private float NormalGravity = 1f;
    private float WallingGravity = 0.5f;

    enum MoveState
    {
        jumping,
        running,
        walling,
        idle
    };

    [SerializeField] private MoveState m_state;

    // Start is called before the first frame update
    void Start()
    {
        m_state = MoveState.idle;
    }

    private void Update()
    {
        if (m_state != MoveState.jumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                var jumpForce = new Vector2(0, jumpStrength);
                if (m_state == MoveState.walling)
                {
                }
                jumpForce.Normalize();
                rb.AddForce(jumpForce.normalized * jumpStrength);
                SetState(MoveState.jumping);
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
        if (state == MoveState.walling)
        {
            rb.gravityScale = WallingGravity;
        }
        else
        {
            rb.gravityScale = NormalGravity;
        }

        switch (state)
        {
            case MoveState.jumping:
                break;
            case MoveState.walling:
                Debug.Log("WE ARE WALL");
                break;
        }

        m_state = state;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_state == MoveState.jumping && collision.gameObject.tag == "Ground")
        {
            //if (AllContactPointsHorizontal(contactPoints))
            //{
                //SetState(MoveState.walling);
            //}
            //else
            {
                SetState(MoveState.idle);
            }
        }
        else if (collision.gameObject.tag == "Death")
        {
            SetState(MoveState.idle);
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (m_state == MoveState.walling)
        {

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Ground")
        //{
        //    if (stayContactPoints.Count == 0 || !AllContactPointsHorizontal(stayContactPoints))
        //    {
        //        SetState(MoveState.jumping);
        //    }
        //}
    }

    public void Die()
    {
        GameManager.Instance.PlayerDie();
        GameManager.Instance.Narrator.PlaySound(TestClip);
    }

    public void PauseMovement()
    {
        
    }
}
