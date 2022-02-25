using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool canJump;
    private bool polaritySwitched;



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

    IEnumerator PolaritySwitch()
    {
        Jump();

        yield return new WaitForSecondsRealtime(0.3f);

        Swap();

    }
}
