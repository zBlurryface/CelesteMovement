using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    [Range(1,20)]
    public float speed = 5f;
    
    [Range(1,20)]
    public float jumpVelocity = 11.3f;

    [Range(1, 20)]
    public float wallSpeed = 3f;

    private Rigidbody2D rb;
    private GroundWallCheck collision;

    private Animator anim;
    
    // Start is called before the first frame update
    void Awake()
    {
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
            GetComponent<SpriteRenderer>().flipX = false;
        if (x < 0)
            GetComponent<SpriteRenderer>().flipX = true;


        if (Input.GetButtonDown("Jump"))
        {
            if(collision.onGround)
                Jump(Vector2.up);
        }

        if (!collision.onGround)
            anim.SetBool("isJumping", true);
        else if(collision.onGround)
            anim.SetBool("isJumping", false);

        if (collision.onWall && !collision.onGround)
            WallSlide();



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
        rb.velocity = new Vector2(rb.velocity.x, -wallSpeed);
    }

    private void WallJump()
    {

    }

}
