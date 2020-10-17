using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpStrength;
    enum moveState
    {
        jumping,
        running,
        idle
    };
    moveState state;

    // Start is called before the first frame update
    void Start()
    {
        state = moveState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(state != moveState.jumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpStrength));
            state = moveState.jumping;
        }
        if (state != moveState.jumping && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jumpStrength));
            state = moveState.jumping;
        }
        if (state != moveState.jumping && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, jumpStrength));
            state = moveState.jumping;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.tag == "ground")
        //{
        //
        //}
    }


}
