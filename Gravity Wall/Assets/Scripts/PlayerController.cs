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

    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    [SerializeField]
    private AudioSource jumpSFX;

    [SerializeField]
    private AudioSource dropSFX;

    [SerializeField]
    private AudioSource deathSFX;

    [SerializeField]
    private AudioSource laserDeathSFX;

    [SerializeField]
    private AudioSource gravityUpSFX;

    [SerializeField]
    private AudioSource gravityDownSFX;

    [SerializeField]
    private AudioSource runningSFX;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
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

            jumpSFX.Play();
            
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
            //gravitySFX.Play();
            StartCoroutine(PolaritySwitch());
        }

        AnimationStates();
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
            gravityUpSFX.Play();
        }
        else
        {
            polaritySwitched = false;
            gravityDownSFX.Play();
        }

    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    private void AnimationStates()
    {
        //running or idle
        if (moveInput != 0f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        //Jumping
        if (rb.velocity.y > 0.01f)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(DeathSequence());
            deathSFX.Play();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            StartCoroutine(DeathSequence());
            laserDeathSFX.Play();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dropSFX.Play();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && rb.velocity.x != 0 && !runningSFX.isPlaying && isGrounded)
        {
            runningSFX.Play();
        }
        else if (rb.velocity.x == 0 || rb.velocity.y > 1)
        {
            runningSFX.Stop();
        }
    }

    IEnumerator PolaritySwitch()
    {
        Jump();

        yield return new WaitForSecondsRealtime(0.1f);

        Swap();

    }

    IEnumerator DeathSequence()
    {
        anim.SetTrigger("isDead");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 0;

    }


}
