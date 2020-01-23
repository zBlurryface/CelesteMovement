using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    [Range(1,20)]
    public float speed = 10f;
    
    [Range(1,20)]
    public float jumpVelocity = 2.5f;

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
