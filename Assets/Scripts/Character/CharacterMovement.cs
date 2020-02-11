using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    [Range(1,20)]
    public float speed = 5f;
    
    [Range(1,20)]
    public float jumpVelocity = 11.3f;
    public bool isJumping;

    [Range(1, 20)]
    public float wallSpeed = 3f;

    private Rigidbody2D rb;
    private GroundWallCheck collision;

    private Animator anim;
    private SpriteRenderer _spriteRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb         = GetComponent<Rigidbody2D>();
        collision  = GetComponent<GroundWallCheck>();
        anim       = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
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
        }

        if (!collision.onGround) {
            anim.SetBool("isJumping", true);
        }
        else if (collision.onGround) {
            anim.SetBool("isJumping", false);
        }

        if (collision.onWall && !collision.onGround && rb.velocity.y < 0)
            if(x != 0)
                WallSlide();
        

        if (collision.onWall && Input.GetKey(KeyCode.B))
        {
            rb.gravityScale = 0;
            if(x > .2f || x < -.2f)
               rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        } else
        {
            rb.gravityScale = 1;
        }
        
        
    }
    
    void MoveCharacter(Vector2 direction)
    {
        rb.velocity = (new Vector2(direction.x * speed, rb.velocity.y));
    }
    
    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpVelocity;
        
    }

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity = new Vector2(rb.velocity.x, -wallSpeed);
    }

    private void WallJump()
    {
        if (collision.onWall) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            Jump(Vector2.up);
            
        }
    }

    private void WallGrab() {
        rb.simulated = false;
    }

    private void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.CompareTag("Reset Pulo"))
        {
            speed *= 2;
            Destroy(colisor.gameObject);
        }

    }
}
