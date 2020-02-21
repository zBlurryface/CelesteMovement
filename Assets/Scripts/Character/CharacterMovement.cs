using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    
    [Range(1,20)]
    public float speed = 5f;
    
    [Range(1,20)]
    public float jumpVelocity = 11.3f;
    public float trampolimJumpVelocity = 20f;

    
    [Range(1, 20)]
    public float wallSpeed = 3f;
    public float wallJumpLerp = 10f;

    public float dashSpeed = 20f;
    public float dashTimer = 0.3f;
    public float groundTimer = 0.15f;
    public float dashLinearDrag = 5f;


    public bool canWalk;
    public bool isJumping;
    public bool wallJumped;
    public bool canDash;
    public bool isDashing;

    public Vector2 respawnPoint;

    
    public bool trampolimJump;

    public GameObject resetPuloGO;
    
    private Rigidbody2D rb;
    private GroundWallCheck collision;

    private Animator anim;
    private SpriteRenderer _spriteRenderer;
    private BetterJump _betterJump;


    // Start is called before the first frame update
    void Awake()
    {
        _betterJump = GetComponent<BetterJump>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb         = GetComponent<Rigidbody2D>();
        collision  = GetComponent<GroundWallCheck>();
        anim       = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y);
        
        MoveCharacter(direction);

        if (rb.velocity.x != 0)
            anim.SetBool("isWalking", true);
        else if (rb.velocity.x <= 0)
            anim.SetBool("isWalking", false);

        if(x > 0)
            _spriteRenderer.flipX = false;
        if (x < 0)
            _spriteRenderer.flipX = true;


        if (Input.GetButtonDown("Jump"))
        {
            if(collision.onGround)
                Jump(Vector2.up);
            if (collision.onWall && !collision.onGround)
                WallJump();
        }

        if (!collision.onGround) {
            anim.SetBool("isJumping", true);
        }
        else if (collision.onGround) {
            anim.SetBool("isJumping", false);
            wallJumped = false;
            canDash = true;

        }

        if (collision.onWall && !collision.onGround && rb.velocity.y < 0)
            if(x != 0)
                WallSlide();
        
        
        if (Input.GetButtonDown("Dash")) {
            if (canDash) {
                Dash(xRaw, yRaw);
                canDash = false;
                if(yRaw > 0)
                    rb.drag = dashLinearDrag;
            }
        }
        if (collision.onWall && Input.GetButton("Hold"))
        {
            rb.gravityScale = 0;
            if(x > .2f || x < -.2f)
               rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .8f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        } else
        {
            if(!isDashing)
                rb.gravityScale = 1;
        } 




    }
    
    void MoveCharacter(Vector2 direction)
    {
        if(!wallJumped)
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        else {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(direction.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }
    
    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpVelocity;
        //trampolimJump = false;

    }

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity = new Vector2(rb.velocity.x, -wallSpeed);
    }

    private void WallJump()
    {
        Vector2 wallDir = collision.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f));
        wallJumped = true;
    }

    private void WallGrab() {
        rb.simulated = false;
    }

    private void Dash(float x, float y) {
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashTimer());
    }
    
    IEnumerator DashTimer()
    {
        //StartCoroutine(GroundDashTimer());

        rb.gravityScale = 0;
        _betterJump.enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(dashTimer);

        rb.gravityScale = 1;
        rb.drag = 0;
        _betterJump.enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDashTimer()
    {
        yield return new WaitForSeconds(groundTimer);
        if (collision.onGround)
            canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.CompareTag("Reset Pulo")) {
            canDash = true;
            resetPuloGO.SetActive(false);
        }

        if (colisor.gameObject.CompareTag("Checkpoint"))
            respawnPoint = colisor.transform.position;

        if (colisor.gameObject.CompareTag("Death")) {
            transform.position = respawnPoint;
            resetPuloGO.SetActive(true);
        }
            


        if (colisor.gameObject.CompareTag("Trampolim")) {
            trampolimJump = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * trampolimJumpVelocity;
        }
        
        if(colisor.gameObject.CompareTag("Win"))
            SceneManager.LoadScene(2);

    }
    
}
