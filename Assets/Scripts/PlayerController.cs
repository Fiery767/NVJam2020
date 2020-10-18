using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float velSpeed;
    public float HorJumpStrength;
    public AudioClip TestClip;
    public float MaxVel;

    private float WallJumpFacingThreshold = 0.9f;
    private float NormalGravity = 1f;
    private float WallingGravity = 0.75f;
    private float JumpDirX = 0;
    private bool grounded = false;
    private Animator anim;
    private bool facing;
    private SpriteRenderer sr;
    private float jumpStrength = 70;


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
        facing = true;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        m_state = MoveState.idle;
    }

    private void Update()
    {
        if(facing) { sr.flipX = false; }
        else { sr.flipX = true; }
        if(Mathf.Abs(rb.velocity.x)>MaxVel)
        {
            rb.velocity = new Vector2((Mathf.Abs(rb.velocity.x)/rb.velocity.x) * MaxVel, rb.velocity.y);
        }
        if (m_state != MoveState.jumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (m_state != MoveState.walling)
                {
                    JumpDirX = 0;
                }
                else
                {
                    jumpStrength = jumpStrength * 1.5f;
                }
                var jumpForce = new Vector2(JumpDirX * HorJumpStrength, 1);
                rb.AddForce(jumpForce.normalized * jumpStrength);
                SetState(MoveState.jumping);
                jumpStrength = 70;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Die();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.ResetLevel();
        }
    }

    void FixedUpdate()
    {
        bool isActuallyGrounded = grounded && !(m_state == MoveState.walling) && !(m_state == MoveState.jumping);
        Debug.Log(m_state);
        //Debug.Log(grounded);
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x - velSpeed * Time.fixedDeltaTime, rb.velocity.y);
            facing = false;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && grounded)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
            facing = false;
            if (isActuallyGrounded)
            {
                SetState(MoveState.running);
            }
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !grounded)
        {
            facing = true;
            rb.velocity = new Vector2(rb.velocity.x + velSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && grounded)
        {
            facing = true;
            transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
            if (isActuallyGrounded)
            {
                SetState(MoveState.running);
            }
        }
        else if (grounded && m_state == MoveState.running)
        {
            SetState(MoveState.idle);
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
                anim.SetBool("Idle", false);
                anim.SetBool("Wall", false);
                anim.SetBool("Run", false);
                anim.SetBool("Jump", true);
                break;
            case MoveState.walling:
                anim.SetBool("Idle", false);
                anim.SetBool("Run", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Wall", true);
                break;
            case MoveState.running:
                anim.SetBool("Idle", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Wall", false);
                anim.SetBool("Run", true);
                break;
            case MoveState.idle:
                anim.SetBool("Run", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Wall", false);
                anim.SetBool("Idle", true);
                break;
        }

        m_state = state;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_state == MoveState.jumping && collision.gameObject.tag == "Ground")
        {
            SetState(MoveState.idle);
        }
        else if (collision.gameObject.tag == "Death")
        {
            SetState(MoveState.idle);
            Die();
        }
        if(collision.gameObject.tag == "Ground")
        {
           rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        if (m_state == MoveState.walling)
        {
            SetState(MoveState.idle);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        grounded = true;
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

    public void WallTrigger(bool side)
    {
        if (m_state != MoveState.idle)
        {
            SetState(MoveState.walling);
            if (side) { JumpDirX = 1; facing = true; }
            else { JumpDirX = -1; facing = false; }
        }
    }
}
