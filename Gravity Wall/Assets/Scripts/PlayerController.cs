using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float jumpForce = 20;
    public float moveInput;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;
    private bool canJump;
    private bool polaritySwitched;
    private bool facingRight = true;



    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        polaritySwitched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w")) && canJump == true)
        {
            if(polaritySwitched == false)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                rb.velocity = Vector2.down * jumpForce;
            }
                
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PolaritySwitch());
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0)
        {
            ChangeDirection();
        }
        else if(facingRight == true && moveInput < 0)
        {
            ChangeDirection();
        }

    }

    public void Jump()
    {
        rb.gravityScale *= -1;
    }

    public void Swap()
    {
        Vector3 temp = transform.localScale;
        temp.y *= -1;
        transform.localScale = temp;

        if(polaritySwitched == false)
        {
            polaritySwitched = true;
        }
        else
        {
            polaritySwitched = false;
        }
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    IEnumerator PolaritySwitch()
    {
        Jump();

        yield return new WaitForSecondsRealtime(0.1f);

        Swap();

    }
}
