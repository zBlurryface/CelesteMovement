using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Range(1,20)]
    public float speed = 10f;
    
    
    [Range(1,20)]
    public float jumpVelocity = 2.5f;

    [Range(1,10)]
    public float jumpMultiplier = 2.0f;
    
    [Range(1,10)]
    public float fallMultiplier = 2.5f;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y);
        
        MoveCharacter(direction);
        
        if (Input.GetButtonDown("Jump"))
            Jump(Vector2.up);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
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
    
}
