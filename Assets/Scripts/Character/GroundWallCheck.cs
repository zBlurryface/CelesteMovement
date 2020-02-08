using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWallCheck : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;

    [Space]

    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]
    public float bottonSize;
    public float sideSize;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, bottonSize, groundLayer);
        onWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, sideSize, groundLayer)
            || Physics2D.OverlapBox((Vector2)transform.position + leftOffset, sideSize, groundLayer);

        onRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, sideSize, groundLayer);
        onLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, sideSize, groundLayer);
        */
        
        onGround = Physics2D.Raycast( transform.position, Vector2.down, bottonSize, groundLayer);
        onWall = Physics2D.Raycast( transform.position, Vector2.right, sideSize, groundLayer) 
                 || Physics2D.Raycast( transform.position, Vector2.left, sideSize, groundLayer);

        
        onRightWall = Physics2D.Raycast(transform.position, Vector2.right, sideSize, groundLayer);
        onLeftWall = Physics2D.Raycast(transform.position, Vector2.left, sideSize, groundLayer);

        
        
        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawRay((Vector2)transform.position, Vector3.down * bottonSize);
        Gizmos.DrawRay((Vector2)transform.position, Vector3.right * sideSize);
        Gizmos.DrawRay((Vector2)transform.position, Vector3.left * sideSize);
    }








}
